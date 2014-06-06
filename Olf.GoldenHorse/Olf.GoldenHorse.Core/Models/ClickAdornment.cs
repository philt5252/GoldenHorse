using System;
using System.Drawing;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Core.Models
{
    public class ClickAdornment : ScreenshotAdornment
    {
        public int ClickX { get; set; }
        public int ClickY { get; set; }

        public override void Adorn(Bitmap image)
        {
            using (var graphics = Graphics.FromImage(image))
            {
                int yOffset = Resources.click.Height/2;
                int xOffset = Resources.click.Width/2;
                graphics.DrawImage(Resources.click, ClickX-xOffset, ClickY-yOffset);
            }
        }
    }
}