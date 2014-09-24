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
        public event EventHandler ParameterValuesChanged;

        protected virtual void OnParameterValuesChanged()
        {
            EventHandler handler = ParameterValuesChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public OperationParameter[] Parameters
        {
            get { return parameters; }
            set
            {
                if (parameters != null)
                {
                    foreach (OperationParameter operationParameter in parameters)
                    {
                        operationParameter.ValueChanged -= OperationParameterOnValueChanged;
                    }
                }

                parameters = value;

                foreach (OperationParameter operationParameter in parameters)
                {
                    operationParameter.OwningOperation = this;
                    operationParameter.ValueChanged += OperationParameterOnValueChanged;
                }

                RaiseTestChanged();
            }
        }

        private void OperationParameterOnValueChanged(object sender, EventArgs eventArgs)
        {
            OnParameterValuesChanged();
            RaiseTestChanged();
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

        public abstract bool Play(MappedItem control, Log log);

        public OperationParameter GetParameterNamed(string name)
        {
            return Parameters.FirstOrDefault(p => Equals(name, p.Name));
        }

        protected Screenshot CreateScreenshot(Log log, int handle=0)
        {
            Bitmap bitmap = Camera.Capture(handle);
            DateTime dateTime = DateTime.Now;
            string screenshotName = "ghscn_" + dateTime.Ticks + ".bmp";
            bitmap.Save(Path.Combine(ProjectSuiteManager.GetScreenshotsFolder(log), screenshotName));

            Screenshot screenshot = new Screenshot();

            screenshot.ImageFile = screenshotName;
            screenshot.DateTime = dateTime;
            return screenshot;
        }

        private void RaiseTestChanged()
        {
            if(TestItem != null && TestItem.Test != null)
                TestItem.Test.RaiseTestChanged();
        }
    }
}