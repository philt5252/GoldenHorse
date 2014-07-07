using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Olf.GoldenHorse.Core.Views.Converters
{
    public class TrainingItemImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString().Equals("NotCompleted"))
            {
                return new BitmapImage(new Uri("../../WhiteImages/BlackCircleCheck.png", UriKind.RelativeOrAbsolute));
            }

            else if (value.ToString().Equals("Completed"))
            {
                return new BitmapImage(new Uri("../../WhiteImages/GreenCircleCheck.png", UriKind.RelativeOrAbsolute));
            }
            
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}