using Prism.Services.Dialogs;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

namespace SQLI.SRS2.Core.Dialogs
{
    /// <summary>
    /// Interaction logic for ToastDialogWindow.xaml
    /// </summary>
    public partial class ToastDialogWindow : Window, IDialogWindow
    {
        private const double AutoCloseAfterSeconds = 3;
        private bool closeCompleted = false;

        public IDialogResult Result { get; set; }
        
        public ToastDialogWindow()
        {
            InitializeComponent();

            SetStartupLocation();
            StartCloseTimer();
        }

        #region Events methods

        private void ToastEaseOut_Completed(object sender, EventArgs e)
        {
            closeCompleted = true;
            this.Close();
        }

        private void ToastWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!closeCompleted)
            {
                ToastEaseOut.Begin();
                e.Cancel = true;
            }
        }

        #endregion

        #region Window Location methods

        public const Int32 MONITOR_DEFAULTTOPRIMARY = 0x00000001;
        public const Int32 MONITOR_DEFAULTTONEAREST = 0x00000002;

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr handle, Int32 flags);

        private bool IsMainWindowInPrimaryMonitor()
        {
            var hwnd = new WindowInteropHelper(Application.Current.MainWindow).EnsureHandle();
            var currentMonitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);
            var primaryMonitor = MonitorFromWindow(IntPtr.Zero, MONITOR_DEFAULTTOPRIMARY);
            var isInPrimary = currentMonitor == primaryMonitor;
            return isInPrimary;
        }

        public void SetStartupLocation()
        {
            WindowStartupLocation = WindowStartupLocation.Manual;

            var titleBarHeight = 32;

            var initialLeft = 0d;
            var initialTop = 0d;

            if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                if (!IsMainWindowInPrimaryMonitor())
                {
                    initialLeft = SystemParameters.VirtualScreenLeft;
                    initialTop = SystemParameters.VirtualScreenTop;
                }
            }
            else
            {
                initialLeft = Application.Current.MainWindow.Left;
                initialTop = Application.Current.MainWindow.Top;
            }

            Left = initialLeft + Application.Current.MainWindow.Width - Width - 9;
            Top = initialTop + titleBarHeight + 8;
        }

        #endregion

        #region Timer methods

        private void StartCloseTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(AutoCloseAfterSeconds);
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= TimerTick;
            Close();
        }

        #endregion
    }
}
