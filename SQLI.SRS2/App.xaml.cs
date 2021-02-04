using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SQLI.SRS2.Core.Dialogs;
using SQLI.SRS2.Core.Regions;
using SQLI.SRS2.Modules.Disclosure;
using SQLI.SRS2.Modules.Menu;
using SQLI.SRS2.Modules.Showcase;
using SQLI.SRS2.Services;
using SQLI.SRS2.Services.Interfaces;
using System.Windows;

namespace SQLI.SRS2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IDisclosureService, DisclosureService>();

            containerRegistry.RegisterDialog<NotificationDialog, NotificationDialogViewModel>();
            containerRegistry.RegisterDialog<ToastDialog, ToastDialogViewModel>();
            
            containerRegistry.RegisterDialogWindow<ToastDialogWindow>(nameof(ToastDialogWindow));
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<MenuModule>();
            moduleCatalog.AddModule<DisclosureModule>();
            moduleCatalog.AddModule<ShowcaseModule>();
        }

        protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            base.ConfigureDefaultRegionBehaviors(regionBehaviors);

            regionBehaviors.AddIfMissing(DependentViewRegionBehavior.BehaviorKey, typeof(DependentViewRegionBehavior));
        }
    }
}
