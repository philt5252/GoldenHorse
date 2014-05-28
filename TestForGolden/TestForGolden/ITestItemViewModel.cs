using System.Collections.Generic;

namespace TestForGolden
{
    public interface ITestItemViewModel
    {
        TestItem TestItem { get; }
        string Name { get; set; }
        string Operation { get; set; }
        string Value { get; set; }
        string Description { get; set; }
        string AutowaitTimeout { get; set; }

        IList<ITestItemViewModel> ChildItems { get; }
        Screenshot Screenshot { get; }
        bool HasScreenshot { get; }
    }
}