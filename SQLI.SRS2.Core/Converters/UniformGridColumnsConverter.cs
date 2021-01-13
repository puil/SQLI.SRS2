using System;
using System.Globalization;
using System.Windows.Data;

namespace SQLI.SRS2.Core.Converters
{
    public class UniformGridColumnsConverter : IValueConverter
    {
        public int SizeToChangeColumns { get; set; }
        public int SmallSizeColumns { get; set; }
        public int BigSizeColumns { get; set; }

        public UniformGridColumnsConverter()
        {
            this.SizeToChangeColumns = 900;
            this.SmallSizeColumns = 1;
            this.BigSizeColumns = 2;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var decimalValue = System.Convert.ToDecimal(value);
            return decimalValue > SizeToChangeColumns ? BigSizeColumns : SmallSizeColumns;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
