using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SQLI.SRS2.Modules.Menu.Converters
{
    public class UriToSvgImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                SharpVectors.Converters.SvgImageConverterExtension xx = new SharpVectors.Converters.SvgImageConverterExtension();
                var xxxx =  xx.Convert(value, targetType, parameter, culture);
                return xxxx;


                var imageDrawing = new ImageDrawing()
                {
                    ImageSource = new BitmapImage(new Uri(value as string, UriKind.RelativeOrAbsolute))
                };

                //
                // Use a DrawingImage and an Image control to
                // display the drawings.
                //
                var drawingImageSource = new DrawingImage(imageDrawing);

                // Freeze the DrawingImage for performance benefits.
                drawingImageSource.Freeze();

                Image imageControl = new Image();
                imageControl.Stretch = Stretch.None;
                imageControl.Source = drawingImageSource;

                return imageControl;

                //var settings = new WpfDrawingSettings
                //{
                //    IncludeRuntime = false,
                //    TextAsGeometry = true
                //};

                //// 2. Select a file to be converted
                //string svgTestFile = value as string;

                //// 3. Create a file converter
                //using var converter = new FileSvgConverter(settings);

                //SharpVectors.Converters.ImageSvgConverter x = new ImageSvgConverter(settings);

                

                //// 4. Perform the conversion to XAML
                //converter.Convert(svgTestFile);
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
