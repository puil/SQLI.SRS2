using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace SQLI.SRS2.Core.Helpers
{
    public class FocusHelper
    {
        #region Set Keyboard Focus On Load
        public static bool GetSetKeyboardFocusOnLoad(DependencyObject obj)
        {
            return (bool)obj.GetValue(SetKeyboardFocusOnLoadProperty);
        }

        public static void SetSetKeyboardFocusOnLoad(DependencyObject obj, bool value)
        {
            obj.SetValue(SetKeyboardFocusOnLoadProperty, value);
        }

        public static readonly DependencyProperty SetKeyboardFocusOnLoadProperty =
            DependencyProperty.RegisterAttached("SetKeyboardFocusOnLoad", typeof(bool), typeof(FocusHelper),
            new PropertyMetadata(false, SetKeyboardFocusOnLoadChanged));

        private static void SetKeyboardFocusOnLoadChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                if (sender is UIElement ui)
                {
                    ui.Dispatcher.BeginInvoke(DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
                    {
                        Keyboard.Focus(ui);
                    }));
                }
            }
        }
        #endregion Set Keyboard Focus On Load
    }
}
