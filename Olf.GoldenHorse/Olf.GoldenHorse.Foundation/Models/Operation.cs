using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using Olf.GoldenHorse.Foundation.Services;

namespace Olf.GoldenHorse.Foundation.Models
{
    public abstract class Operation
    {
        public abstract string Name { get; }
        public abstract string ParametersDescription { get; }

        public OperationParameter[] Parameters { get; set; }

        [XmlIgnore]
        public TestItem TestItem { get; set; }

        [XmlIgnore]
        public AppManager AppManager{ get { return TestItem.AppManager; } }

        protected Operation()
        {
            Parameters = SetParameters();
        }

        protected abstract OperationParameter[] SetParameters();

        public abstract string DefaultDescription(MappedItem control);

        public abstract void Play(MappedItem control, Log log);

        protected static Screenshot CreateScreenshot(Log log)
        {
            Bitmap bitmap = Camera.Capture();
            DateTime dateTime = DateTime.Now;
            string screenshotName = "ghscn_" + dateTime.Ticks + ".bmp";
            bitmap.Save(Path.Combine(ProjectSuiteManager.GetScreenshotsFolder(log), screenshotName));

            Screenshot screenshot = new Screenshot();

            screenshot.ImageFile = screenshotName;
            screenshot.DateTime = dateTime;
            return screenshot;
        }
    }
}