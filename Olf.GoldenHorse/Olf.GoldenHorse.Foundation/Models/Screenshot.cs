using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace Olf.GoldenHorse.Foundation.Models
{
    public class Screenshot
    {
        [XmlIgnore]
        public ScreenshotOwner Owner { get; set; }

        public string ImageFile { get; set; }
        public List<ScreenshotAdornment> Adornments { get; set; }
        public DateTime DateTime { get; set; }

        public Screenshot()
        {
            Adornments = new List<ScreenshotAdornment>();
        }

        public Bitmap RenderImage()
        {
            string imagePath = Path.Combine(Owner.GetScreenshotFolder(), ImageFile);
            Bitmap bitmap = new Bitmap(imagePath);

            foreach (ScreenshotAdornment screenshotAdornment in Adornments)
            {
                screenshotAdornment.Adorn(bitmap);
            }

            return bitmap;
        }
    }
}