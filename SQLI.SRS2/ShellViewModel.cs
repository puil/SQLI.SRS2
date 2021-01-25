using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SQLI.SRS2.Core;
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

        private bool isEnvironmentConnected;
        public bool IsEnvironmentConnected
        {
            get { return isEnvironmentConnected; }
            set { SetProperty(ref isEnvironmentConnected, value); }
        }

        private string environmentName;
        public string EnvironmentName
        {
            get { return environmentName; }
            set { SetProperty(ref environmentName, value); }
        }

        public int MyProperty { get; set; }

        private DelegateCommand minimizeCommand;
        public DelegateCommand MinimizeCommand => minimizeCommand ??= new DelegateCommand(ExecuteMinimizeCommand);


        private DelegateCommand maximizeCommand;
        public DelegateCommand MaximizeCommand => maximizeCommand ??= new DelegateCommand(ExecuteMaximizeCommand);

        
        private DelegateCommand closeCommand;
        public DelegateCommand CloseCommand => closeCommand ??= new DelegateCommand(ExecuteCloseCommand);

        private DelegateCommand changeEnvironmentConnectionCommand;
        private readonly IRegionManager regionManager;

        public DelegateCommand ChangeEnvironmentConnectionCommand => changeEnvironmentConnectionCommand ??= new DelegateCommand(ExecuteChangeEnvironmentConnectionCommand);

        void ExecuteChangeEnvironmentConnectionCommand()
        {
            IsEnvironmentConnected = !IsEnvironmentConnected;

            //regionManager.RequestNavigate(RegionNames.InspectorRegion, "InspectorView");
        }

        public ShellViewModel(IRegionManager regionManager)
        {
            EnvironmentName = "DEV environment";
            IsEnvironmentConnected = true;

            this.regionManager = regionManager;
            //regionManager.RequestNavigate(RegionNames.InspectorRegion, "InspectorView");
        }

        void ExecuteMinimizeCommand()
        {
            WindowState = WindowState.Minimized;
            RaisePropertyChanged(nameof(WindowState));
        }

        void ExecuteMaximizeCommand()
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            RaisePropertyChanged(nameof(WindowState));
            RaisePropertyChanged(nameof(IsMaximized));
        }

        void ExecuteCloseCommand() => Close();

        private void Close()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
