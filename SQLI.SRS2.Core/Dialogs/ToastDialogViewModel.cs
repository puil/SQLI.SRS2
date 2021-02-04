using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace SQLI.SRS2.Core.Dialogs
{
    public class ToastDialogViewModel : BindableBase, IDialogAware
    {
        private DelegateCommand _closeDialogCommand;
        public DelegateCommand CloseDialogCommand => _closeDialogCommand ??= new DelegateCommand(CloseDialog);

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        private string _title = "Notification";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private NotificationType notificationType;
        public NotificationType NotificationType
        {
            get => notificationType;
            set => SetProperty(ref notificationType, value);
        }

        public event Action<IDialogResult> RequestClose;

        protected virtual void CloseDialog()
        {
            RaiseRequestClose(null);
        }

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        public virtual bool CanCloseDialog()
        {
            return true;
        }

        public virtual void OnDialogClosed()
        {
            
        }

        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>("message");
            NotificationType = parameters.GetValue<NotificationType>("notificationType");
        }
    }
}
