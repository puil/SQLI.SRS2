using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace SQLI.SRS2.Modules.Menu.Converters
{
    public class MenuScrollBarVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isMenuExpanded)
            {
                var x = isMenuExpanded ? ScrollBarVisibility.Auto : ScrollBarVisibility.Hidden;
                return x;
            }

            return Binding.DoNothing;
            //return value is bool isMenuExpanded ? isMenuExpanded ? ScrollBarVisibility.Auto : ScrollBarVisibility.Hidden : Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
