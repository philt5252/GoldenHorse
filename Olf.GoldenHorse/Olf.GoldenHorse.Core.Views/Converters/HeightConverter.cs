using System;
using System.Globalization;
using System.Windows.Data;

namespace Olf.GoldenAutomation.Core.Views.Converters
{
    public class HeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            double val = (double)value;

            val = val - 32;

            return val;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}