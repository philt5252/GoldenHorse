using System.Xml.Serialization;

namespace Olf.GoldenHorse.Foundation.Models
{
    public abstract class ScreenshotOwner
    {
        private Screenshot screenshot;

        [XmlIgnore]
        public bool HasScreenshot { get { return Screenshot != null; } }

        public Screenshot Screenshot
        {
            get { return screenshot; }
            set
            {
                screenshot = value;

                if(screenshot != null)
                    screenshot.Owner = this;
            }
        }

        public abstract string GetScreenshotFolder();
    }
}