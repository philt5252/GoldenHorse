using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.Views.Converters
{
    public class TestItemImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ITestItemViewModel testItemViewModel = value as ITestItemViewModel;
            if (testItemViewModel.Type.Equals(TestItemTypes.OnScreenAction))
            {
                if (testItemViewModel.Operation.Equals("Left Click") || testItemViewModel.Operation.Equals("Right Click"))
                {
                    return new BitmapImage(new Uri("../../WhiteImages/TestItemImages/MousePointer.png", UriKind.RelativeOrAbsolute));
                }
                else if (testItemViewModel.Operation.Equals("Keyboard"))
                {
                    return new BitmapImage(new Uri("../../WhiteImages/TestItemImages/keyboard.png", UriKind.RelativeOrAbsolute));
                }
                else if (testItemViewModel.Operation.Equals("Validate text at point"))
                {
                    return new BitmapImage(new Uri("../../../WhiteImages/TestItemImages/MousePointer.png", UriKind.RelativeOrAbsolute));
                }
            }
            return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}