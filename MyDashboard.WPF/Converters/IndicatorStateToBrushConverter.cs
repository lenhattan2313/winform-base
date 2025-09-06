using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using MyDashboard.WPF.ViewModels; // để nhận diện IndicatorState

namespace MyDashboard.WPF.Converters
{
    public class ProgressToStarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double progress = 0;

            if (value is double d)
                progress = Math.Clamp(d, 0, 1);

            return new GridLength(progress, GridUnitType.Star);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class InverseProgressToStarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double progress = 0;

            if (value is double d)
                progress = Math.Clamp(d, 0, 1);

            return new GridLength(1 - progress, GridUnitType.Star);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class IndicatorStateToBrushConverter : IValueConverter
    {
        public Brush GrayBrush { get; set; } = Brushes.Gray;
        public Brush GreenBrush { get; set; } = Brushes.Green;
        public Brush RedBrush { get; set; } = Brushes.Red;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                IndicatorState.Green => GreenBrush,
                IndicatorState.Red => RedBrush,
                IndicatorState.Gray => GrayBrush,
                _ => GrayBrush
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
