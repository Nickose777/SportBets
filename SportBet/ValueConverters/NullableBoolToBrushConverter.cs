using System;
using System.Windows.Data;
using System.Windows.Media;

namespace SportBet.ValueConverters
{
    [ValueConversion(typeof(bool?), typeof(Brush))]
    public class NullableBoolToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Color color;

            bool? b = value as bool?;

            if (b.HasValue)
            {
                color = b.Value ? Color.FromRgb(0, 192, 48) : Color.FromRgb(192, 56, 56);
            }
            else
            {
                color = Color.FromRgb(255, 255, 255);
            }

            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
