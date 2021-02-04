using SQLI.SRS2.Core.Attributes;
using SQLI.SRS2.Core.Helpers;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SQLI.SRS2.Core.Converters
{
    public class EnumToSvgIconUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is Enum)
            {
                var svgIconUriAttribute = EnumHelper.GetAttribute<SvgIconUriAttribute>(value as Enum);
                if (svgIconUriAttribute != null)
                    return svgIconUriAttribute.SourceUri;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
