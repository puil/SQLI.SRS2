using SharpVectors.Converters;
using SharpVectors.Runtime;
using System.Windows;
using System.Windows.Media;

namespace SQLI.SRS2.Core.Helpers
{
    public class SvgHelper
    {
        public static readonly DependencyProperty SvgBrushProperty =
            DependencyProperty.RegisterAttached("SvgBrush", typeof(Brush), typeof(SvgViewbox), new PropertyMetadata(OnSvgBrushChanged));

        private static void OnSvgBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SvgViewbox sgvViewBox && sgvViewBox.Child is SvgDrawingCanvas drawingCanvas)
                foreach (Drawing drawing in drawingCanvas.DrawObjects)
                    if (drawing is GeometryDrawing geometryDrawing)
                        geometryDrawing.Brush = e.NewValue as Brush;
        }

        public static void SetSvgBrush(UIElement element, Brush value)
        {
            element.SetValue(SvgBrushProperty, value);
        }

        public static Brush GetSvgBrush(UIElement element)
        {
            return (Brush)element.GetValue(SvgBrushProperty);
        }
    }
}
