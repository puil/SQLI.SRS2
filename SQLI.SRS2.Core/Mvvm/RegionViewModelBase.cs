using Prism;
using Prism.Regions;
using System;

namespace SQLI.SRS2.Core.Mvvm
{
    public class RegionViewModelBase : ViewModelBase, INavigationAware, IConfirmNavigationRequest, IActiveAware
    {
        private bool isActive;
        protected IRegionManager RegionManager { get; private set; }
        
        public string Title { get; set; }
        public bool IsActive { get => IsActive; set => SetProperty(ref isActive, value); }
        
        public RegionViewModelBase(IRegionManager regionManager)
        {
            RegionManager = regionManager;
        }

        public event EventHandler IsActiveChanged;

        public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            continuationCallback(true);
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {

        }
    }
}
