using System;
using System.Globalization;
using System.Windows.Data;

namespace SQLI.SRS2.Core.Converters
{
    public class CompareToBooleanConverter : IValueConverter
    {
        public bool IsInverted { get; set; } = false;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = value != null && value.Equals(parameter);
            if (IsInverted) flag = !flag;
            return flag;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
