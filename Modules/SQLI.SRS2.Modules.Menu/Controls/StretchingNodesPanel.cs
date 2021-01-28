using Infragistics.Controls.Menus.Primitives;
using System.Windows;

namespace SQLI.SRS2.Modules.Menu.Controls
{
    public class StretchingNodesPanel : NodesPanel
    {
        protected override Size RenderNode(Infragistics.Controls.Menus.XamDataTreeNode node, double availableWidth)
        {
            base.RenderNode(node, availableWidth);

            if (node.Control != null)
            {
                if (!double.IsPositiveInfinity(availableWidth))
                    node.Control.Width = availableWidth;

                node.Control.Measure(new Size(availableWidth, double.PositiveInfinity));
            }

            return node.Control.DesiredSize;
        }
    }
}
