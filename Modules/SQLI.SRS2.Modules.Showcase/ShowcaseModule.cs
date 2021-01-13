using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SQLI.SRS2.Modules.Showcase.Views;

namespace SQLI.SRS2.Modules.Showcase
{
    public class ShowcaseModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MSInternalControlsView>();
        }
    }
}