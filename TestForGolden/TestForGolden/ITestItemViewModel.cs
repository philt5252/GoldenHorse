using System.Collections.Generic;

namespace TestForGolden
{
    public interface ITestItemViewModel
    {
        ITestItemViewModel Parent { get; set; }
        string Name { get; set; }
        string Operation { get; set; }
        string Value { get; set; }
        string Description { get; set; }
        string AutowaitTimeout { get; set; }

        IList<ITestItemViewModel> ChildItems { get; }
    }
}