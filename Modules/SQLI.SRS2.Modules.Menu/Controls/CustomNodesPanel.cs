using Infragistics.Controls.Menus.Primitives;
using SQLI.SRS2.Business.Menu;
using System.Windows;

namespace SQLI.SRS2.Modules.Menu.Controls
{
    public class CustomNodesPanel : NodesPanel
    {
        protected override Size RenderNode(Infragistics.Controls.Menus.XamDataTreeNode node, double availableWidth)
        {
            var defaultSize = base.RenderNode(node, availableWidth);

            //if (node.Data is MenuItem menuItem && !menuItem.IsFirstLevel)
            //    return defaultSize;

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
