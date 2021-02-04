using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using SQLI.SRS2.Core.Extensions;
using SQLI.SRS2.Core.Mvvm;

namespace SQLI.SRS2.Modules.Showcase.ViewModels
{
    public class MSInternalControlsViewModel : RegionViewModelBase
    {
        private readonly IDialogService dialogService;
      
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public MSInternalControlsViewModel(IRegionManager regionManager, IDialogService dialogService) : base(regionManager)
        {
            Title = "MS Internal Controls";
            Message = "MSInternalControlsViewv";
            this.dialogService = dialogService;
        }

        private DelegateCommand<string> showToastCommand;
        public DelegateCommand<string> ShowToastCommand => showToastCommand ??= new DelegateCommand<string>(ExecuteShowToastCommand);

        void ExecuteShowToastCommand(string parameter)
        {
            Core.Dialogs.NotificationType notificationType = parameter switch
            {
                "Ok" => Core.Dialogs.NotificationType.Success,
                "Warning" => Core.Dialogs.NotificationType.Warning,
                "Error" => Core.Dialogs.NotificationType.Error,
                _ => Core.Dialogs.NotificationType.Custom,
            };

            var parameters = new DialogParameters
            {
                { "message", "A description of the dialog will appear here." },
                { "notificationType", notificationType }
            };

            dialogService.Show("ToastDialog", parameters, null, "ToastDialogWindow");
        }


        private DelegateCommand showDialogNotificationCommand;
        public DelegateCommand ShowDialogNotificationCommand => showDialogNotificationCommand ??= new DelegateCommand(ExecuteShowDialogNotificationCommand);

        void ExecuteShowDialogNotificationCommand()
        {
            var parameters = new DialogParameters
            {
                { "title" , "Dialog title"},
                { "message", "Hello, I'm a dialog notification!" },
            };

            dialogService.ShowDialog("NotificationDialog", parameters, OnNotificationDialogClosed);
        }

        private void OnNotificationDialogClosed(IDialogResult obj)
        {
            if (obj.Result == ButtonResult.OK)
                dialogService.ShowToast("Ok is pressed");
            else if (obj.Result == ButtonResult.Cancel)
                dialogService.ShowToast("Cancel is pressed", Core.Dialogs.NotificationType.Warning);
        }



        private DelegateCommand<string> inspectorCommand;
        public DelegateCommand<string> InspectorCommand => inspectorCommand ??= new DelegateCommand<string>(ExecuteInspectorCommand);

        void ExecuteInspectorCommand(string parameter)
        {
            dialogService.ShowToast($"Inspector item pressed: {parameter}");
        }
    }
}
