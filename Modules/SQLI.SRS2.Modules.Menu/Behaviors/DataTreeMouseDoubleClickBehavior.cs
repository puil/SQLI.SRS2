using Infragistics.Controls.Menus;
using Microsoft.Xaml.Behaviors;
using System.Windows.Input;

namespace SQLI.SRS2.Modules.Menu.Behaviors
{
    public class DataTreeMouseDoubleClickBehavior : Behavior<XamDataTree>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseDoubleClick += OnMouseDoubleClick;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (this.AssociatedObject != null)
            {
                this.AssociatedObject.MouseDoubleClick -= OnMouseDoubleClick;
            }
        }

        private void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is XamDataTree xamDataTree && xamDataTree.ActiveNode != null)
                xamDataTree.ActiveNode.IsExpanded = !xamDataTree.ActiveNode.IsExpanded;
        }
    }
}
