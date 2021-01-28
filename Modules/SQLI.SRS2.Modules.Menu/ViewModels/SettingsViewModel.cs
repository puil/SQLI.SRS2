using Prism.Events;
using Prism.Regions;
using SQLI.SRS2.Core.Mvvm;
using SQLI.SRS2.Modules.Menu.Controls;
using SQLI.SRS2.Modules.Menu.Events;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SQLI.SRS2.Modules.Menu.ViewModels
{
    public class SettingsViewModel : RegionViewModelBase
    {
        private string selectedTheme;
        private readonly IEventAggregator eventAggregator;

        public string SelectedTheme
        {
            get { return selectedTheme; }
            set
            {
                SetProperty(ref selectedTheme, value);
                OnThemeChanged(selectedTheme);
            }
        }

        public IEnumerable<string> Themes { get; set; } = new List<string>
            {
                "Metro",
                "MetroDark"
            };

        public SettingsViewModel(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager)
        {
            this.eventAggregator = eventAggregator;

            Title = "Settings";
            SetCurrentSelectedTheme();

            eventAggregator.GetEvent<InspectorMenuItemsEvent>().Publish(GetInspectorItems());
        }

        private IEnumerable<InspectorItem> GetInspectorItems()
        {
            return new List<InspectorItem>
            {
                CreateInspectorItem("../Assets/Close.svg", "Close"),
                CreateInspectorItem("../Assets/New Window.svg", "Open in new window"),
                CreateInspectorItem("../Assets/New file.svg", "New file"),
                CreateInspectorItem("../Assets/Block OFF.svg", "Block edition"),
                CreateInspectorItem("../Assets/Import.svg", "Import"),
                CreateInspectorItem("../Assets/New Window.svg", "Open in new window"),
                CreateInspectorItem("../Assets/New file.svg", "New file"),
                CreateInspectorItem("../Assets/Block OFF.svg", "Block edition"),
                CreateInspectorItem("../Assets/Import.svg", "Import"),
            };
        }

        private InspectorItem CreateInspectorItem(string iconUri, string description) => new InspectorItem
        {
            IconUri = iconUri,
            Description = description
        };

        private void SetCurrentSelectedTheme()
        {
            // TODO Change this when implementing how to store current theme by user
            //      For now, it is METRO because it is defined in App.xaml
            selectedTheme = "Metro";
        }

        private void OnThemeChanged(string theme)
        {
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = theme switch
                {
                    "Metro" => new Uri(@"/SQLI.SRS2;component/Themes/Metro.Generic.xaml", UriKind.Relative),
                    "MetroDark" => new Uri(@"/SQLI.SRS2;component/Themes/MetroDark.Generic.xaml", UriKind.Relative),
                    _ => throw new ArgumentException($"Unexisting theme selected {theme}")
                }
            });
        }


    }
}
