﻿using System.Xml.Serialization;

namespace TestForGolden
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
                screenshot.Owner = this;
            }
        }

        public abstract string GetScreenshotFolder();
    }
}