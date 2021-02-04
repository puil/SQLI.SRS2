using Prism.Services.Dialogs;
using SQLI.SRS2.Core.Dialogs;

namespace SQLI.SRS2.Core.Extensions
{
    public static class DialogServiceExtension
    {
        public static void ShowToast(this IDialogService dialogService, string message)
        {
            ShowToast(dialogService, message, NotificationType.Success);
        }

        public static void ShowToast(this IDialogService dialogService, string message, NotificationType notificationType)
        {
            var parameters = new DialogParameters
            {
                { "message", message },
                { "notificationType", notificationType }
            };

            dialogService.Show("ToastDialog", parameters, null, "ToastDialogWindow");
        }
    }
}
