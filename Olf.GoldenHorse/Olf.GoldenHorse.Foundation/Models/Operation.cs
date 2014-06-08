using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Olf.GoldenHorse.Foundation.Services;

namespace Olf.GoldenHorse.Foundation.Models
{
    public abstract class Operation
    {
        private OperationParameter[] parameters;
        public abstract string Name { get; }
        public abstract string ParametersDescription { get; }

        public OperationParameter[] Parameters
        {
            get { return parameters; }
            set
            {
                parameters = value;

                foreach (OperationParameter operationParameter in parameters)
                {
                    operationParameter.OwningOperation = this;
                }
            }
        }

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

        public OperationParameter GetParameterNamed(string name)
        {
            return Parameters.FirstOrDefault(p => Equals(name, p.Name));
        }

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