using Infragistics.Themes;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows;

namespace SQLI.SRS2
{
    public class ShellViewModel : BindableBase
    {
        private string _title = "SIMS - SRA Ingredient Management System";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public WindowState WindowState { get; set; }

        public bool IsMaximized => WindowState == WindowState.Maximized;


        private DelegateCommand minimizeCommand;
        public DelegateCommand MinimizeCommand => minimizeCommand ??= new DelegateCommand(ExecuteMinimizeCommand);

        void ExecuteMinimizeCommand()
        {
            WindowState = WindowState.Minimized;
            RaisePropertyChanged(nameof(WindowState));
        }

        private DelegateCommand maximizeCommand;
        public DelegateCommand MaximizeCommand => maximizeCommand ??= new DelegateCommand(ExecuteMaximizeCommand);

        void ExecuteMaximizeCommand()
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            RaisePropertyChanged(nameof(WindowState));
            RaisePropertyChanged(nameof(IsMaximized));
        }

        private DelegateCommand closeCommand;
        public DelegateCommand CloseCommand => closeCommand ??= new DelegateCommand(ExecuteCloseCommand);

        void ExecuteCloseCommand() => Close();

        private void Close()
        {
            System.Windows.Application.Current.Shutdown();
        }

        public ShellViewModel()
        {
            //ThemeManager.ApplicationTheme = new MetroTheme();
            
        }
    }
}
