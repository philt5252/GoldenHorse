using System.Collections.Generic;

namespace TestForGolden
{
    public abstract class TestItem
    {
        public List<TestItem> Children { get; set; }
    }
}