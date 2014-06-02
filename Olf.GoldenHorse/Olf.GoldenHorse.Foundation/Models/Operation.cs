using System.Collections.Generic;
using System.Xml.Serialization;

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

        public abstract void Play(MappedItem control);
    }
}