using System.Windows;
using System.Windows.Controls;

namespace SQLI.SRS2.Modules.Menu.Views
{
    /// <summary>
    /// Interaction logic for InspectorView
    /// </summary>
    public partial class InspectorView : UserControl
    {
        public InspectorView()
        {
            InitializeComponent();
            this.SizeChanged += InspectorView_SizeChanged;
        }

        private void InspectorView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
        }
    }
}
