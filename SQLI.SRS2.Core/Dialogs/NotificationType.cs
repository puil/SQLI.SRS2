using SQLI.SRS2.Core.Attributes;

namespace SQLI.SRS2.Core.Dialogs
{
    public enum NotificationType
    {
        [SvgIconUri("../Assets/OK.svg")]
        Success,
        [SvgIconUri("../Assets/Alert.svg")]
        Warning,
        [SvgIconUri("../Assets/KO.svg")]
        Error,
        [SvgIconUri(null)]
        Custom
    }
}
