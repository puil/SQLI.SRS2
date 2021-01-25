using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SQLI.SRS2.Core.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public Visibility HiddenVisibility { get; set; }

        public bool IsInverted { get; set; }

        public NullToVisibilityConverter()
        {
            HiddenVisibility = Visibility.Collapsed;
            IsInverted = false;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = value == null;
            if (IsInverted) flag = !flag;
            return flag ? Visibility.Visible : HiddenVisibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
