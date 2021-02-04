using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using SQLI.SRS2.Business.Menu;
using SQLI.SRS2.Core;
using System.Collections.ObjectModel;
using System.Linq;

namespace SQLI.SRS2.Modules.Menu.ViewModels
{
    public class MenuViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private readonly IDialogService dialogService;

        private bool isMenuExpanded = true;
        public bool IsMenuExpanded { get => isMenuExpanded; set => SetProperty(ref isMenuExpanded, value); }

        private MenuItem activeDataItem;
        public MenuItem ActiveDataItem
        {
            get => activeDataItem;
            set
            {
                activeDataItem = value;
                OnActiveDataItemChanged();
            }
        }

        public ObservableCollection<MenuItem> MenuItems { get; init; } = new ObservableCollection<MenuItem>();

        #region Commands

        private DelegateCommand openSettingsCommand;
        public DelegateCommand OpenSettingsCommand => openSettingsCommand ??= new DelegateCommand(ExecuteOpenSettingsCommand);

        void ExecuteOpenSettingsCommand()
        {
            NavigateToView("Settings");

            // TODO Settings view should be opened in a new window. Next statement is a sample but "SingleWindow" is not created nor implemented anywhere yet
            // dialogService.Show("Settings", null, null, "SingleWindow");
        }


        private DelegateCommand expandCollapseMenuCommand;
        public DelegateCommand ExpandCollapseMenuCommand => expandCollapseMenuCommand ??= new DelegateCommand(ExecuteExpandCollapseMenuCommand);

        void ExecuteExpandCollapseMenuCommand() => IsMenuExpanded = !IsMenuExpanded;


        private DelegateCommand<object> inspectorItemCommand;
        public DelegateCommand<object> InspectorItemCommand => inspectorItemCommand ??= new DelegateCommand<object>(ExecuteInspectorItemCommand);
        
        void ExecuteInspectorItemCommand(object parameter)
        {
            if (parameter is MenuItem menuItem)
                ActiveDataItem = menuItem;
        }


        private DelegateCommand openInNewWindowCommand;
        public DelegateCommand OpenInNewWindowCommand => openInNewWindowCommand ??= new DelegateCommand(ExecuteOpenInNewWindowCommand);

        void ExecuteOpenInNewWindowCommand()
        {
            // TODO This is the command that should be executed when some menu item would be opened in a new window
        }

        #endregion

        public MenuViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            this.regionManager = regionManager;
            this.dialogService = dialogService;

            LoadMenuItems();
        }

        /// <summary>
        /// For now, just create sample menu items
        /// </summary>
        private void LoadMenuItems()
        {
            CreateMenuItemsForTestingPurposes();
        }

        #region Testing Menu Items

        private MenuItem CreateMenuItem(string name, string description, string iconUri, string viewName)
        {
            var newMenuItem = new MenuItem
            {
                Name = name,
                Description = description,
                IconUri = iconUri,
                ViewName = viewName
            };

            MenuItems.Add(newMenuItem);

            return newMenuItem;
        }

        private ChildMenuItem CreateChildMenuItem(string name, string description, string iconUri, string viewName, MenuItem parent)
        {
            var newChildMenuItem = new ChildMenuItem
            {
                Name = name,
                Description = description,
                IconUri = iconUri,
                ViewName = viewName,
                Parent = parent
            };

            if (parent != null)
                parent.Children.Add(newChildMenuItem);

            return newChildMenuItem;
        }

        private void CreateMenuItemsForTestingPurposes()
        {
            var administrationMenuItem = CreateMenuItem("Administration", "Administration", "../Assets/Administration.svg", null);
            CreateChildMenuItem("Showcase", "Showcase", "../Assets/File Automation.svg", "MSInternalControlsView", administrationMenuItem);

            var powerUsersMenuItem = CreateMenuItem("PowerUsers", "Power Users", "../Assets/PowerUsers.svg", null);
            var powerUsersYear2020MenuItem = CreateChildMenuItem("Year2020", "Year 2020", null, null, powerUsersMenuItem);
            CreateChildMenuItem("FileType01", "File type 01", "../Assets/File Automation.svg", "MSInternalControlsView", powerUsersYear2020MenuItem);
            CreateChildMenuItem("FileType02", "File type 02", "../Assets/File Automation.svg", "DisclosureView", powerUsersYear2020MenuItem);
            var powerUsersYear2021MenuItem = CreateChildMenuItem("Year2021", "Year 2021", null, null, powerUsersMenuItem);
            CreateChildMenuItem("FileType03", "File type 03", null, "MSInternalControlsView", powerUsersYear2021MenuItem);
            CreateChildMenuItem("FileType04", "File type 04", null, "DisclosureView", powerUsersYear2021MenuItem);

            CreateMenuItem("Ingredients", "Ingredients", "../Assets/Ingredients.svg", null);
            CreateMenuItem("Disclosure", "Disclosure", "../Assets/Disclosure.svg", "DisclosureView");
            CreateMenuItem("SAP", "SAP", "../Assets/SAP.svg", null);
            CreateMenuItem("Translation", "Translation", "../Assets/Translation.svg", null);
            CreateMenuItem("NonSAP", "Non SAP", "../Assets/SAPnon.svg", null);
            CreateMenuItem("EUReporting", "EU Reporting", "../Assets/EUreporting.svg", null);
            CreateMenuItem("ECHAReporting", "ECHA Reporting", "../Assets/ECHAreporting.svg", null);
            CreateMenuItem("Compliance", "Compliance", "../Assets/Compliance.svg", null);
            CreateMenuItem("Automation", "Automation", "../Assets/Automation.svg", null);
            CreateMenuItem("DataArea", "Data Area", "../Assets/DataArea.svg", "Settings");
        }

        #endregion

        private void NavigateToView(string viewName) => regionManager.RequestNavigate(RegionNames.ContentRegion, viewName);

        private void OnActiveDataItemChanged()
        {
            if (ActiveDataItem != null)
            {
                if (ActiveDataItem.HasChildren)
                {
                    ActiveDataItem.IsExpanded = true;
                    ActiveDataItem.IsSelected = true;
                    IsMenuExpanded = true;
                }
                else
                {
                    ActiveDataItem.IsSelected = true;
                    //IsMenuExpanded = true;        // Uncomment if we want to expand menu tree everytime a first level item is selected and it has no children
                }

                CollapseAllFirstNodesExceptActive();
            }

            if (ActiveDataItem != null && !string.IsNullOrWhiteSpace(ActiveDataItem.ViewName))
                NavigateToView(ActiveDataItem.ViewName);
        }

        private void CollapseAllFirstNodesExceptActive()
        {
            var firstLevelItem = GetMenuItemFirstLevel(ActiveDataItem);
            MenuItems.Where(x => x.Name != firstLevelItem.Name && x.IsExpanded).ToList().ForEach(x => x.IsExpanded = false);
        }

        private MenuItem GetMenuItemFirstLevel(MenuItem menuItem) => menuItem.HasParent ? GetMenuItemFirstLevel(menuItem.Parent) : menuItem;
    }
}
