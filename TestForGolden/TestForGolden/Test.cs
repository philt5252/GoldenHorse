using System.Collections;
using System.Collections.Generic;

namespace TestForGolden
{
    public class Test
    {
        public string Name { get; set; }
        public List<TestItem> TestItems { get; set; }

        public Test()
        {
            TestItems = new List<TestItem>();
        }
    }
}