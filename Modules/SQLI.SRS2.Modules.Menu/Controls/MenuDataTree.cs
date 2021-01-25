using Infragistics.Controls.Menus;
using System.Windows;

namespace SQLI.SRS2.Modules.Menu.Controls
{
    public class MenuDataTree : XamDataTree
    {
        public Visibility HorizontalScrollBarVisibility
        {
            get { return (Visibility)GetValue(HorizontalScrollBarVisibilityProperty); }
            set { SetValue(HorizontalScrollBarVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalScrollBarVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalScrollBarVisibilityProperty =
            DependencyProperty.Register("HorizontalScrollBarVisibility", typeof(Visibility), typeof(MenuDataTree), new PropertyMetadata(Visibility.Visible));


        public bool IsMenuDataTreeExpanded
        {
            get { return (bool)GetValue(IsMenuDataTreeExpandedProperty); }
            set { SetValue(IsMenuDataTreeExpandedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsMenuDataTreeExpanded.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsMenuDataTreeExpandedProperty =
            DependencyProperty.Register("IsMenuDataTreeExpanded", typeof(bool), typeof(MenuDataTree), new PropertyMetadata(false));


    }
}
