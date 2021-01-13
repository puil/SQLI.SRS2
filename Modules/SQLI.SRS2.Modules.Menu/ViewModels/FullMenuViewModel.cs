using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SQLI.SRS2.Core;
using SQLI.SRS2.Modules.Menu.Models;
using System;
using System.Collections.ObjectModel;

namespace SQLI.SRS2.Modules.Menu.ViewModels
{
    public class FullMenuViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;

        private DelegateCommand openSettingsCommand;
        private MenuItem selectedMenuItem;

        public DelegateCommand OpenSettingsCommand =>
            openSettingsCommand ?? (openSettingsCommand = new DelegateCommand(ExecuteOpenSettingsCommand));

        void ExecuteOpenSettingsCommand()
        {
            regionManager.RequestNavigate(RegionNames.ContentRegion, "Settings");
        }

        public ObservableCollection<MenuItem> MenuItems { get; init; } = new ObservableCollection<MenuItem>();
        public MenuItem SelectedMenuItem { 
            get => selectedMenuItem;
            set
            {
                selectedMenuItem = value;
                OnSelectedMenuItemChanged();
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
            var administrationMenuItem = new MenuItem
            {
                Name = "Administration",
                Description = "Administration",
            };

            administrationMenuItem.Children.Add(new MenuItem
            {
                Name = "Settings",
                Description = "Settings",
                ViewName = "Settings",
                Parent = administrationMenuItem
            });

            var powerUsersMenuItem = new MenuItem
            {
                Name = "PowerUsers",
                Description = "Power Users",
            };

            var ingredientsMenuItem = new MenuItem
            {
                Name = "Ingredients",
                Description = "Ingredients",
            };

            var disclosureMenuItem = new MenuItem
            {
                Name = "Disclosure",
                Description = "Disclosure",
                ViewName = "DisclosureView"
            };

            disclosureMenuItem.Children.Add(new MenuItem
            {
                Name = "Disclosure",
                Description = "Disclosure",
                ViewName = "DisclosureView",
                Parent = disclosureMenuItem
            });
            disclosureMenuItem.Children.Add(new MenuItem
            {
                Name = "ProductReviewDisclosure",
                Description = "Product Review Disclosure",
                ViewName = "ProductReviewDisclosure",
                Parent = disclosureMenuItem
            });


            MenuItems.Add(administrationMenuItem);
            MenuItems.Add(powerUsersMenuItem);
            MenuItems.Add(ingredientsMenuItem);
            MenuItems.Add(disclosureMenuItem);

            MenuItems.Add(new MenuItem
            {
                Name = "Settings",
                Description = "Settings",
                ViewName = "Settings",
            });

            MenuItems.Add(new MenuItem
            {
                Name = "MSInternalControlsView",
                Description = "MSInternalControlsView",
                ViewName = "MSInternalControlsView",
            });
        }

        private void OnSelectedMenuItemChanged()
        {
            if (SelectedMenuItem == null || string.IsNullOrWhiteSpace(SelectedMenuItem.ViewName))
                return;

            regionManager.RequestNavigate(RegionNames.TabRegion, SelectedMenuItem.ViewName);
        }
    }
}
