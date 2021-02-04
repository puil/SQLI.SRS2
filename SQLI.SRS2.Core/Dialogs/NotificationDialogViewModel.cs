using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace SQLI.SRS2.Core.Dialogs
{
    public class NotificationDialogViewModel : BindableBase, IDialogAware
    {
        private DelegateCommand<string> buttonCommand;
        public DelegateCommand<string> ButtonCommand => buttonCommand ??= new DelegateCommand<string>(ExecuteButtonCommand);

        void ExecuteButtonCommand(string parameter)
        {
            var buttonResult = parameter switch
            {
                "Cancel" => ButtonResult.Cancel,
                "Ok" => ButtonResult.OK,
                _ => ButtonResult.None
            };

            RaiseRequestClose(new DialogResult(buttonResult));
        }

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

        public event Action<IDialogResult> RequestClose;

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
            Title = parameters.GetValue<string>("title");
            Message = parameters.GetValue<string>("message");
        }
    }
}
