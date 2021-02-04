using SQLI.SRS2.Core;
using SQLI.SRS2.Core.Attributes;
using SQLI.SRS2.Core.Regions;
using SQLI.SRS2.Modules.Disclosure.Inspector;
using System.Windows.Controls;

namespace SQLI.SRS2.Modules.Disclosure.Views
{
    [DependentView(RegionNames.DynamicInspectorRegion, typeof(DisclosureInspector))]
    public partial class DisclosureView : UserControl, ISupportDataContext
    {
        public DisclosureView()
        {
            InitializeComponent();
        }
    }
}
