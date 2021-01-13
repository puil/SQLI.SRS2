using Prism.Regions;
using SQLI.SRS2.Core.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SQLI.SRS2.Modules.Menu.ViewModels
{
    public class SettingsViewModel : RegionViewModelBase
    {
        private string selectedTheme;
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

        public SettingsViewModel(IRegionManager regionManager) : base(regionManager)
        {
            Title = "Settings";
            SetCurrentSelectedTheme();
        }

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
