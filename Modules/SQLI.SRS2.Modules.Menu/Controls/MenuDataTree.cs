using Infragistics.Controls.Menus;
using System.Windows;

namespace SQLI.SRS2.Modules.Menu.Controls
{
    public class MenuDataTree : XamDataTree
    {
        public static readonly DependencyProperty HorizontalScrollBarVisibilityProperty =
            DependencyProperty.Register("HorizontalScrollBarVisibility", typeof(Visibility), typeof(MenuDataTree), new PropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty IsMenuDataTreeExpandedProperty =
            DependencyProperty.Register("IsMenuDataTreeExpanded", typeof(bool), typeof(MenuDataTree), new PropertyMetadata(false));


        public Visibility HorizontalScrollBarVisibility
        {
            get { return (Visibility)GetValue(HorizontalScrollBarVisibilityProperty); }
            set { SetValue(HorizontalScrollBarVisibilityProperty, value); }
        }
        
        public bool IsMenuDataTreeExpanded
        {
            get { return (bool)GetValue(IsMenuDataTreeExpandedProperty); }
            set { SetValue(IsMenuDataTreeExpandedProperty, value); }
        }
    }
}
