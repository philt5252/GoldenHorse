using System.Collections.Generic;
using System.Drawing;
using System.Windows.Documents;

namespace TestForGolden
{
    public class Screenshot
    {
        public string ImagePath { get; set; }
        public List<ScreenshotAdornment> Adornments { get; set; }
        public string DateTime { get; set; }

        public Screenshot()
        {
            Adornments = new List<ScreenshotAdornment>();
        }

        public Bitmap RenderImage()
        {
            Bitmap bitmap = new Bitmap(ImagePath);

            foreach (ScreenshotAdornment screenshotAdornment in Adornments)
            {
                screenshotAdornment.Adorn(bitmap);
            }

            return bitmap;
        }
    }
}