using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Documents;
using System.Xml.Serialization;

namespace TestForGolden
{
    public class Screenshot
    {
        [XmlIgnore]
        public ScreenshotOwner Owner { get; set; }

        public string ImageFile { get; set; }
        public List<ScreenshotAdornment> Adornments { get; set; }
        public string DateTime { get; set; }

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