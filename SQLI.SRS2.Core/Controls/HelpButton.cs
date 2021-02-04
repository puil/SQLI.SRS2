using System.Windows;

namespace SQLI.SRS2.Core.Controls
{
    public class HelpButton : SvgIconButton
    {
        public static readonly DependencyProperty ToolTipMessageProperty =
            DependencyProperty.Register("ToolTipMessage", typeof(string), typeof(HelpButton), new PropertyMetadata(string.Empty));

        public string ToolTipMessage
        {
            get { return (string)GetValue(ToolTipMessageProperty); }
            set { SetValue(ToolTipMessageProperty, value); }
        }
    }
}
