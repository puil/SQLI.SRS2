using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace SQLI.SRS2.Core.Converters
{
    public class BooleanOrToVisibilityConverter : IMultiValueConverter
    {
        public Visibility HiddenVisibility { get; set; }

        public bool IsInverted { get; set; }

        public BooleanOrToVisibilityConverter()
        {
            HiddenVisibility = Visibility.Collapsed;
            IsInverted = false;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = values.OfType<IConvertible>().Any(System.Convert.ToBoolean);
            if (IsInverted) flag = !flag;
            return flag ? Visibility.Visible : HiddenVisibility;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
