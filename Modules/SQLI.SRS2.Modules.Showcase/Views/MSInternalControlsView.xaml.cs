using SQLI.SRS2.Core;
using SQLI.SRS2.Core.Attributes;
using SQLI.SRS2.Core.Regions;
using SQLI.SRS2.Modules.Showcase.Inspector;
using System.Windows.Controls;

namespace SQLI.SRS2.Modules.Showcase.Views
{
    [DependentView(RegionNames.DynamicInspectorRegion, typeof(MSInternalControlsInspector))]
    public partial class MSInternalControlsView : UserControl, ISupportDataContext
    {
        public MSInternalControlsView()
        {
            InitializeComponent();
        }
    }
}
