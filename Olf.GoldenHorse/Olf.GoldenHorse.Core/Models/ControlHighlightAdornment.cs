using System.Drawing;
using Olf.GoldenHorse.Core.Services;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Core.Models
{
    public class ControlHighlightAdornment : ScreenshotAdornment
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public override void Adorn(Bitmap image)
        {
            using (var graphics = Graphics.FromImage(image))
            {
                graphics.DrawRectangle(new Pen(Color.Red,3), X, Y, Width, Height);
            }
        }
    }
}