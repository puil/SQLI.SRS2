using Prism.Ioc;
using Prism.Modularity;
using SQLI.SRS2.Modules.Disclosure.Views;

namespace SQLI.SRS2.Modules.Disclosure
{
    public class DisclosureModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<DisclosureView>();
        }
    }
}