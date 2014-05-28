using System.Drawing;
using TestForGolden.Properties;

namespace TestForGolden
{
    public class ScreenshotClickAdornment : ScreenshotAdornment
    {
        public int ClickX { get; set; }
        public int ClickY { get; set; }

        public override void Adorn(Bitmap image)
        {
            using (var graphics = Graphics.FromImage(image))
            {
                graphics.DrawImage(Resources.mouse_click2, new Point(ClickX, ClickY));
            }
        }
    }
}