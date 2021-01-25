using Infragistics.Controls.Menus;
using Microsoft.Xaml.Behaviors;
using System.Linq;

namespace SQLI.SRS2.Modules.Menu.Behaviors
{
    public class DataTreeOneFirstLevelItemExpandedBehavior : Behavior<XamDataTree>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.ActiveNodeChanged += OnActiveNodeChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (this.AssociatedObject != null)
            {
                this.AssociatedObject.ActiveNodeChanged -= OnActiveNodeChanged;
            }
        }

        private void OnActiveNodeChanged(object sender, ActiveNodeChangedEventArgs e)
        {
            if (sender is not XamDataTree xamDataTree)
                return;

            if (e.NewActiveTreeNode.Data is Business.Menu.MenuItem menuItem && menuItem.IsFirstLevel)
            {
                if (menuItem.HasChildren)
                    CollapseNodes(xamDataTree, e.NewActiveTreeNode);
                else
                    CollapseNodes(xamDataTree);
            }
        }

        private void CollapseNodes(XamDataTree xamDataTree, XamDataTreeNode exceptionNode = null)
        {
            switch (exceptionNode)
            {
                case null:
                    xamDataTree.Nodes.ToList().ForEach(x => x.IsExpanded = false);
                    break;
                default:
                    xamDataTree.Nodes.Where(x => x != exceptionNode).ToList().ForEach(x => x.IsExpanded = false);
                    break;
            }
        }
    }
}
