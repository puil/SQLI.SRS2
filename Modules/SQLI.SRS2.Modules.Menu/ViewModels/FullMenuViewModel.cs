using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SQLI.SRS2.Business.Menu;
using SQLI.SRS2.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SQLI.SRS2.Modules.Menu.ViewModels
{
    public class FullMenuViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;

        private MenuItem selectedMenuItem;

        public bool IsMenuExpanded { get => isMenuExpanded; set => SetProperty(ref isMenuExpanded, value); }

        #region Commands

        private DelegateCommand openSettingsCommand;
        public DelegateCommand OpenSettingsCommand => openSettingsCommand ??= new DelegateCommand(ExecuteOpenSettingsCommand);

        void ExecuteOpenSettingsCommand()
        {
            NavigateToView("Settings");
        }



        private DelegateCommand<object> previewMouseLeftButtonUpCommand;
        public DelegateCommand<object> PreviewMouseLeftButtonUpCommand =>
            previewMouseLeftButtonUpCommand ??= new DelegateCommand<object>(ExecutePreviewMouseLeftButtonUpCommand);

        void ExecutePreviewMouseLeftButtonUpCommand(object parameter)
        {

        }

        private DelegateCommand<object> fireCommand;
        public DelegateCommand<object> FireCommand =>
            fireCommand ?? (fireCommand = new DelegateCommand<object>(ExecuteFireCommand));

        void ExecuteFireCommand(object parameter)
        {

        }


        private DelegateCommand expandCollapseMenuCommand;
        public DelegateCommand ExpandCollapseMenuCommand => expandCollapseMenuCommand ??= new DelegateCommand(ExecuteExpandCollapseMenuCommand);

        void ExecuteExpandCollapseMenuCommand()
        {
            IsMenuExpanded = !IsMenuExpanded;
        }

        #endregion

        public ObservableCollection<MenuItem> MenuItems { get; init; } = new ObservableCollection<MenuItem>();
        public MenuItem SelectedMenuItem
        {
            get => selectedMenuItem;
            set
            {
                //OnSelectedMenuItemChanged(selectedMenuItem, value);
                selectedMenuItem = value;
            }
        }

        private MenuItem selectedTreeMenuItem;

        public MenuItem SelectedTreeMenuItem
        {
            get => selectedTreeMenuItem;
            set
            {
                selectedTreeMenuItem = value;
                OnSelectedTreeMenuItemChanged();
            }
        }

        private IEnumerable<MenuItem> selectedDataTreeItems;

        public IEnumerable<MenuItem> SelectedDataTreeItems
        {
            get => selectedDataTreeItems;
            set
            {
                selectedDataTreeItems = value;
                OnSelectedDataTreeItemsChanged();
            }
        }

        public FullMenuViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

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
            CreateMenuItem("Administration", "Administration", "../Assets/Administration.svg", "MSInternalControlsView");

            var powerUsersMenuItem = CreateMenuItem("PowerUsers", "Power Users", "../Assets/PowerUsers.svg", null);
            var powerUsersYear2020MenuItem = CreateChildMenuItem("Year2020", "Year 2020", null, null, powerUsersMenuItem);
            var fileType01MenuItem = CreateChildMenuItem("FileType01", "File type 01", "../Assets/Administration.svg", "MSInternalControlsView", powerUsersYear2020MenuItem);
            CreateChildMenuItem("FileType55", "File type 55", "../Assets/Administration.svg", "MSInternalControlsView", fileType01MenuItem);
            CreateChildMenuItem("FileType02", "File type 02", null, "DisclosureView", powerUsersYear2020MenuItem);
            var powerUsersYear2021MenuItem = CreateChildMenuItem("Year2021", "Year 2021", null, null, powerUsersMenuItem);
            CreateChildMenuItem("FileType03", "File type 03", null, "MSInternalControlsView", powerUsersYear2021MenuItem);
            CreateChildMenuItem("FileType04", "File type 04", null, "DisclosureView", powerUsersYear2021MenuItem);

            CreateMenuItem("Ingredients", "Ingredients", "../Assets/Ingredients.svg", null);
            CreateMenuItem("Disclosure", "Disclosure", "../Assets/Disclosure.svg", null);
            CreateMenuItem("SAP", "SAP", "../Assets/SAP.svg", null);
            CreateMenuItem("Translation", "Translation", "../Assets/Translation.svg", null);
            CreateMenuItem("NonSAP", "Non SAP", "../Assets/SAPnon.svg", null);
            CreateMenuItem("EUReporting", "EU Reporting", "../Assets/EUreporting.svg", null);
            CreateMenuItem("ECHAReporting", "ECHA Reporting", "../Assets/ECHAreporting.svg", null);
            CreateMenuItem("Compliance", "Compliance", "../Assets/Compliance.svg", null);
            CreateMenuItem("Automation", "Automation", "../Assets/Automation.svg", null);
            CreateMenuItem("DataArea", "Data Area", "../Assets/DataArea.svg", null);
        }

        #endregion

        private void OnSelectedMenuItemChanged(MenuItem oldMenuItem, MenuItem newMenuItem)
        {
            if (oldMenuItem == newMenuItem)
                return;

            if (oldMenuItem != null)
            {
                oldMenuItem.IsSelected = false;
                oldMenuItem.IsExpanded = false;
            }

            newMenuItem.IsSelected = true;

            selectedMenuItem = newMenuItem;

            if (SelectedMenuItem == null || string.IsNullOrWhiteSpace(SelectedMenuItem.ViewName))
                return;

            if (SelectedMenuItem.HasChildren)
                SelectedMenuItem.IsExpanded = !SelectedMenuItem.IsExpanded;
            else
                NavigateToView(SelectedMenuItem.ViewName);
        }

        internal bool OnMenuItemSelected(object dataContext)
        {
            if (dataContext != null)
            {
                if (dataContext is MenuItem menuItem)
                {
                    if (menuItem.HasChildren)
                    {
                        menuItem.IsExpanded = !menuItem.IsExpanded;
                        SetMenuItemAsSelected(menuItem, menuItem.IsExpanded);
                        return true;
                    }
                    else
                    {
                        SetMenuItemAsSelected(menuItem, true);

                        if (!string.IsNullOrWhiteSpace(menuItem.ViewName))
                        {
                            NavigateToView(menuItem.ViewName);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private void SetMenuItemAsSelected(MenuItem menuItem, bool selectedValue)
        {
            if (selectedValue)
            {
                MenuItems.Where(x => x.Name != menuItem.Name).ToList().ForEach(x =>
                {
                    x.IsSelected = false;
                    x.IsExpanded = false;
                });


                menuItem.IsSelected = true;
            }
            else
            {
                menuItem.IsSelected = false;
            }
        }

        private void OnSelectedTreeMenuItemChanged()
        {
            if (SelectedTreeMenuItem != null && !string.IsNullOrWhiteSpace(SelectedTreeMenuItem.ViewName))
                NavigateToView(SelectedTreeMenuItem.ViewName);
        }

        private void NavigateToView(string viewName)
        {
            //regionManager.RequestNavigate(RegionNames.TabRegion, viewName);
            regionManager.RequestNavigate(RegionNames.ContentRegion, viewName);
        }

        private void OnSelectedDataTreeItemsChanged()
        {

        }


        private MenuItem activeDataItem;
        private bool isMenuExpanded = true;

        public MenuItem ActiveDataItem
        {
            get => activeDataItem;
            set
            {
                activeDataItem = value;
                OnActiveDataItemChanged();
            }
        }

        private void OnActiveDataItemChanged()
        {
            if (ActiveDataItem != null)
            {
                if (ActiveDataItem.HasChildren)
                {
                    ActiveDataItem.IsExpanded = true;
                    IsMenuExpanded = true;
                }
            }

            if (ActiveDataItem != null && !string.IsNullOrWhiteSpace(ActiveDataItem.ViewName))
                NavigateToView(ActiveDataItem.ViewName);
        }
    }
}
