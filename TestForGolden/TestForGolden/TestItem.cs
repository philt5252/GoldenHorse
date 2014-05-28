using System.Collections.Generic;

namespace TestForGolden
{
    public abstract class TestItem
    {
        public bool HasScreenshot { get { return Screenshot == null; } }
        public Screenshot Screenshot { get; set; }
        public List<TestItem> Children { get; set; }
        public string Description { get; set; }
    }
}