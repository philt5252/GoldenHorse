using System.Collections.Generic;
using System.Xml.Serialization;

namespace TestForGolden
{
    public abstract class TestItem
    {
        [XmlIgnore]
        public bool HasScreenshot { get { return Screenshot != null; } }

        public Screenshot Screenshot { get; set; }
        public List<TestItem> Children { get; set; }
        public string Description { get; set; }
    }
}