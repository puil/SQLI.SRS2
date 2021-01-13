using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Shapes;

namespace SQLI.SRS2
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : Window
    {
        public const int HtCaption = 0x2;
        public const int WmNclbuttondown = 0xA1;

        public Shell()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        //Resize Handlers
        #region ResizeWindows
        bool ResizeInProcess = false;
        private void Resize_Init(object sender, MouseButtonEventArgs e)
        {
            Rectangle senderRect = sender as Rectangle;
            if (senderRect != null)
            {
                ResizeInProcess = true;
                senderRect.CaptureMouse();
            }
        }

        private void Resize_End(object sender, MouseButtonEventArgs e)
        {
            Rectangle senderRect = sender as Rectangle;
            if (senderRect != null)
            {
                ResizeInProcess = false; ;
                senderRect.ReleaseMouseCapture();
            }
        }

        private void Resizeing_Form(object sender, MouseEventArgs e)
        {
            if (ResizeInProcess)
            {
                Rectangle senderRect = sender as Rectangle;
                if (senderRect != null)
                {
                    Window mainWindow = senderRect.Tag as Window;
                    if (senderRect != null)
                    {
                        double width = e.GetPosition(mainWindow).X;
                        double height = e.GetPosition(mainWindow).Y;

                        senderRect.CaptureMouse();
                        if (senderRect.Name.ToLower().Contains("right"))
                        {
                            width += 5;
                            mainWindow.Width = System.Math.Max(width, mainWindow.MinWidth);
                        }
                        if (senderRect.Name.ToLower().Contains("left"))
                        {
                            width -= 5;
                            var delta = mainWindow.Width - width;

                            if (delta >= mainWindow.MinWidth)
                                mainWindow.Left += width;

                            mainWindow.Width = System.Math.Max(delta, mainWindow.MinWidth);
                        }
                        if (senderRect.Name.ToLower().Contains("bottom"))
                        {
                            height += 5;
                            mainWindow.Height = System.Math.Max(height, mainWindow.MinHeight);
                        }
                        if (senderRect.Name.ToLower().Contains("top"))
                        {
                            height -= 5;
                            var delta = mainWindow.Height - height;
                            if (delta >= mainWindow.MinHeight)
                                mainWindow.Top += height;

                            mainWindow.Height = System.Math.Max(delta, mainWindow.MinHeight);
                        }
                    }
                }
            }
        }
        #endregion

        private void TitleBarMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
            }

            ReleaseCapture();
            SendMessage(new WindowInteropHelper(this).Handle, WmNclbuttondown, HtCaption, 0);
        }
    }
}
