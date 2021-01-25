using System;
using System.Globalization;
using System.Windows.Data;

namespace SQLI.SRS2.Core.Converters
{
    public enum MathOperation
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public sealed class MathConverter : IValueConverter
    {
        public MathOperation Operation { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                double value1 = System.Convert.ToDouble(value, CultureInfo.InvariantCulture);
                double value2 = System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);
                
                return Operation switch
                {
                    MathOperation.Add => value1 + value2,
                    MathOperation.Divide => value1 / value2,
                    MathOperation.Multiply => value1 * value2,
                    MathOperation.Subtract => value1 - value2,
                    _ => Binding.DoNothing,
                };
            }
            catch (FormatException)
            {
                return Binding.DoNothing;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
