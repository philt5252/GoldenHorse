using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Olf.GoldenHorse.Core.Views.Converters
{
    public class CategoryToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString().Contains("Validate"))
            {
                return new BitmapImage(new Uri("../../WhiteImages/TestItemImages/variable.png", UriKind.RelativeOrAbsolute));
            }
            
            else if (value.ToString().Equals("Error"))
            {
                return new BitmapImage(new Uri("../../WhiteImages/StatusAnnotations_Critical_32xLG_color.png", UriKind.RelativeOrAbsolute));
            }
            else if (value.ToString().Equals("Warning"))
            {
                return new BitmapImage(new Uri("../../WhiteImages/StatusAnnotations...ng_32xLG_color.png", UriKind.RelativeOrAbsolute));
            }
            else if (value.ToString().Equals("Event"))
            {
                return new BitmapImage(new Uri("../../WhiteImages/BlackCircleCheck.png", UriKind.RelativeOrAbsolute));
            }
            else if (value.ToString().Equals("Message"))
            {
                return new BitmapImage(new Uri("../../WhiteImages/SendInstantMessage_32x32.png", UriKind.RelativeOrAbsolute));
            }
            else if (value.ToString().Equals("Validation"))
            {
                return new BitmapImage(new Uri("../../WhiteImages/GreenCircleCheck.png", UriKind.RelativeOrAbsolute));
            }
            else if (value.Equals("Log"))
            {
                return new BitmapImage(new Uri("../WhiteImages/ProjectExplorerImages/log.png", UriKind.RelativeOrAbsolute));
            }
            else if (value.Equals("ProjectLogs"))
            {
                return new BitmapImage(new Uri("../WhiteImages/ProjectExplorerImages/Logs.png", UriKind.RelativeOrAbsolute));
            }
            else if (value.Equals("Project"))
            {
                return new BitmapImage(new Uri("../WhiteImages/ProjectExplorerImages/project.png", UriKind.RelativeOrAbsolute));
            }
            else if (value.Equals("ProjectSuiteLogs"))
            {
                return new BitmapImage(new Uri("../WhiteImages/ProjectExplorerImages/projectSuiteLogs.png", UriKind.RelativeOrAbsolute));
            }
            else if (value.Equals("ProjectSuiteProjects"))
            {
                return new BitmapImage(new Uri("../WhiteImages/ProjectExplorerImages/projectSuiteProjects.png", UriKind.RelativeOrAbsolute));
            }
            else if (value.Equals("Test"))
            {
                return new BitmapImage(new Uri("../WhiteImages/ProjectExplorerImages/test2.png", UriKind.RelativeOrAbsolute));
            }
            else if (value.Equals("TestGroup"))
            {
                return new BitmapImage(new Uri("../WhiteImages/ProjectExplorerImages/tests.png", UriKind.RelativeOrAbsolute));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}