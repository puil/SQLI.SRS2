using System.Windows;
using System.Windows.Controls;

namespace SQLI.SRS2.Modules.Menu.Controls
{
    public class CustomItemsControl : ItemsControl
    {
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ContentControl();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            // Even wrap other ContentControls
            return false;
        }
    }
}
