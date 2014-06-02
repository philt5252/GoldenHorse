using System;
using System.Drawing;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Core.Models
{
    public class ScreenshotClickAdornment : ScreenshotAdornment
    {
        public int ClickX { get; set; }
        public int ClickY { get; set; }

        public override void Adorn(Bitmap image)
        {
            using (var graphics = Graphics.FromImage(image))
            {
                graphics.DrawImage(Resources.mouseCursorClick, ClickX, ClickY);
            }
        }
    }
}