using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SQLI.SRS2.Core;
using SQLI.SRS2.Modules.Menu.Views;

namespace SQLI.SRS2.Modules.Menu
{
    public class MenuModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public MenuModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.MenuRegion, "FullMenu");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<FullMenu>();
            containerRegistry.RegisterForNavigation<Settings>();
        }
    }
}