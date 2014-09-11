using System;
using System.Globalization;
using System.Windows.Data;

namespace LearningOcr.Converters
{
    public class HeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            double val = (double)value;

            val = val - 8;

            return val;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}