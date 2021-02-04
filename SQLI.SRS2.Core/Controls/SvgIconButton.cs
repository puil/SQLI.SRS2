using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SQLI.SRS2.Core.Controls
{
    public class SvgIconButton : Button
    {
        public static readonly DependencyProperty IconUriProperty =
           DependencyProperty.Register("IconUri", typeof(Uri), typeof(SvgIconButton), new PropertyMetadata(null));

        public static readonly DependencyProperty IconWidthProperty =
           DependencyProperty.Register("IconWidth", typeof(double), typeof(SvgIconButton), new PropertyMetadata(20d));

        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(double), typeof(SvgIconButton), new PropertyMetadata(20d));

        public static readonly DependencyProperty ColorBrushProperty =
            DependencyProperty.Register("ColorBrush", typeof(Brush), typeof(SvgIconButton), new PropertyMetadata(null));


        public Uri IconUri
        {
            get { return (Uri)GetValue(IconUriProperty); }
            set { SetValue(IconUriProperty, value); }
        }
       
        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        public Brush ColorBrush
        {
            get { return (Brush)GetValue(ColorBrushProperty); }
            set { SetValue(ColorBrushProperty, value); }
        }
    }
}
