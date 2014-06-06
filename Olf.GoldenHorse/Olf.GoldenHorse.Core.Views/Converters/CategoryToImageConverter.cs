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
            if (value == "Error")
            {
                return new BitmapImage(new Uri("../WhiteImages/StatusAnnotations_Critical_32xLG_color.png"));
            }
            else if (value == "Warning")
            {
                return new BitmapImage(new Uri("../WhiteImages/StatusAnnotations_Warning_32xLG_color.png"));
            }
            else if (value == "Event")
            {
                return new BitmapImage(new Uri("../WhiteImages/Event_594_exp.png"));
            }
            else if (value == "Message")
            {
                return new BitmapImage(new Uri("../WhiteImages/SendInstantMessage_32x32.png"));
            }
            else if (value == "Checkpoint")
            {
                return new BitmapImage(new Uri("../WhiteImages/Symbols_Complete_and_ok_White.png"));
            }
            if (value == "Log")
            {
                return new BitmapImage(new Uri("../WhiteImages/ProjectExplorerImages/log.png"));
            }
            else if (value == "ProjectLog")
            {
                return new BitmapImage(new Uri("../WhiteImages/ProjectExplorerImages/Logs.png"));
            }
            else if (value == "Project")
            {
                return new BitmapImage(new Uri("../WhiteImages/ProjectExplorerImages/project.png"));
            }
            else if (value == "ProjectSuiteLogs")
            {
                return new BitmapImage(new Uri("../WhiteImages/ProjectExplorerImages/projectSuiteLogs.png"));
            }
            else if (value == "ProjectSuiteProjects")
            {
                return new BitmapImage(new Uri("../WhiteImages/ProjectExplorerImages/projectSuiteProjectspng"));
            }
            else if (value == "Test")
            {
                return new BitmapImage(new Uri("../WhiteImages/ProjectExplorerImages/test2.png"));
            }
            else if (value == "TestGroup")
            {
                return new BitmapImage(new Uri("../WhiteImages/ProjectExplorerImages/tests.png"));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}