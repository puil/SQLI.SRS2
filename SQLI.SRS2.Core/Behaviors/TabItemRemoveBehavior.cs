using Infragistics.Windows.Controls;
using Infragistics.Windows.Controls.Events;
using Microsoft.Xaml.Behaviors;
using Prism.Regions;
using System.Windows;

namespace SQLI.SRS2.Core.Behaviors
{
    public class TabItemRemoveBehavior : Behavior<XamTabControl>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AddHandler(TabItemEx.ClosingEvent, new RoutedEventHandler(TabItem_Closing));
            AssociatedObject.AddHandler(TabItemEx.ClosedEvent, new RoutedEventHandler(TabItem_Closed));
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.RemoveHandler(TabItemEx.ClosingEvent, new RoutedEventHandler(TabItem_Closing));
            AssociatedObject.RemoveHandler(TabItemEx.ClosedEvent, new RoutedEventHandler(TabItem_Closed));
        }

        void TabItem_Closing(object sender, RoutedEventArgs e)
        {
            IRegion region = RegionManager.GetObservableRegion(AssociatedObject).Value;
            if (region == null)
                return;

            var args = (TabClosingEventArgs)e;

            args.Cancel = !CanRemoveItem(GetItemFromTabItem(args.OriginalSource), region);
        }

        void TabItem_Closed(object sender, RoutedEventArgs e)
        {
            IRegion region = RegionManager.GetObservableRegion(AssociatedObject).Value;
            if (region == null)
                return;

            RemoveItemFromRegion(GetItemFromTabItem(e.OriginalSource), region);
        }

        object GetItemFromTabItem(object source)
        {
            if (source is not TabItemEx tabItem)
                return null;

            return tabItem.Content;
        }

        bool CanRemoveItem(object item, IRegion region)
        {
            bool canRemove = true;

            var context = new NavigationContext(region.NavigationService, null);

            if (item is IConfirmNavigationRequest confirmRequestItem)
            {
                confirmRequestItem.ConfirmNavigationRequest(context, result =>
                {
                    canRemove = result;
                });
            }

            if (item is FrameworkElement frameworkElement && canRemove)
            {
                if (frameworkElement.DataContext is IConfirmNavigationRequest confirmRequestDataContext)
                {
                    confirmRequestDataContext.ConfirmNavigationRequest(context, result =>
                    {
                        canRemove = result;
                    });
                }
            }

            return canRemove;
        }

        void RemoveItemFromRegion(object item, IRegion region)
        {
            var context = new NavigationContext(region.NavigationService, null);

            InvokeOnNavigatedFrom(item, context);

            region.Remove(item);
        }

        void InvokeOnNavigatedFrom(object item, NavigationContext navigationContext)
        {
            if (item is INavigationAware navigationAwareItem)
            {
                navigationAwareItem.OnNavigatedFrom(navigationContext);
            }

            if (item is FrameworkElement frameworkElement)
            {
                if (frameworkElement.DataContext is INavigationAware navigationAwareDataContext)
                {
                    navigationAwareDataContext.OnNavigatedFrom(navigationContext);
                }
            }
        }
    }
}
