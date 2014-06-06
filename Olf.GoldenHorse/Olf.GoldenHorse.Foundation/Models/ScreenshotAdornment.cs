using System.Drawing;
using System.Xml.Serialization;

namespace Olf.GoldenHorse.Foundation.Models
{
    public abstract class ScreenshotAdornment
    {
        [XmlIgnore]
        public Screenshot Screenshot { get; set; }

        public abstract void Adorn(Bitmap image);
    }
}