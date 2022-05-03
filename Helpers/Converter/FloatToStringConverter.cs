using System;
using System.Globalization;
using System.Windows.Data;

namespace DirectxWpf.Helpers.Converter
{
    [ValueConversion(typeof(float), typeof(string))]
    public class FloatToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float orig = (float)value;
            return $"{orig.ToString("0.00")}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
