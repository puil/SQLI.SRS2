using Prism.Ioc;
using Prism.Regions;
using SQLI.SRS2.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;

namespace SQLI.SRS2.Core.Regions
{
    public class DependentViewRegionBehavior : RegionBehavior
    {
        public const string BehaviorKey = "DependentViewRegionBehavior";
        
        private readonly IContainerExtension container;
        private readonly Dictionary<object, List<DependentViewInfo>> dependentViewCache = new Dictionary<object, List<DependentViewInfo>>();

        public DependentViewRegionBehavior(IContainerExtension container)
        {
            this.container = container;
        }

        protected override void OnAttach()
        {
            Region.ActiveViews.CollectionChanged += ActiveViews_CollectionChanged;
        }

        private void ActiveViews_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var newView in e.NewItems)
                {
                    var dependentViews = new List<DependentViewInfo>();

                    if (dependentViewCache.ContainsKey(newView))
                    {
                        dependentViews = dependentViewCache[newView];
                    }
                    else
                    {
                        var atts = GetCustomAttributes<DependentViewAttribute>(newView.GetType());
                        foreach (var att in atts)
                        {
                            var info = CreateDependentViewInfo(att);

                            if (info.View is ISupportDataContext infoDC && newView is ISupportDataContext viewDC)
                                infoDC.DataContext = viewDC.DataContext;

                            dependentViews.Add(info);
                        }

                        dependentViewCache.Add(newView, dependentViews);
                    }

                    dependentViews.ForEach(item => Region.RegionManager.Regions[item.Region].Add(item.View));
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var oldView in e.OldItems)
                {
                    if (dependentViewCache.ContainsKey(oldView))
                    {
                        var dependentViews = dependentViewCache[oldView];
                        dependentViews.ForEach(item => Region.RegionManager.Regions[item.Region].Remove(item.View));

                        if (!ShouldKeepAlive(oldView))
                            dependentViewCache.Remove(oldView);
                    }
                }
            }
        }

        private static bool ShouldKeepAlive(object oldView)
        {
            var regionLifetime = GetViewOrDataContextLifetime(oldView);
            return regionLifetime == null || regionLifetime.KeepAlive;
        }

        private static IRegionMemberLifetime GetViewOrDataContextLifetime(object view)
        {
            if (view is IRegionMemberLifetime regionLifetime)
                return regionLifetime;

            if (view is FrameworkElement fe)
                return fe.DataContext as IRegionMemberLifetime;

            return null;
        }

        private DependentViewInfo CreateDependentViewInfo(DependentViewAttribute attribute)
        {
            return new DependentViewInfo
            {
                Region = attribute.Region,
                View = container.Resolve(attribute.Type)
            };
        }

        private static IEnumerable<T> GetCustomAttributes<T>(Type type)
        {
            return type.GetCustomAttributes(typeof(T), true).OfType<T>();
        }
    }
}
