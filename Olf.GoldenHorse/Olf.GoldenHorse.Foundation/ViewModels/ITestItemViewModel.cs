using System.Collections.Generic;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface ITestItemViewModel
    {
        TestItem TestItem { get; set; }
        string Name { get; }
        string Operation { get; }
        string Value { get; set; }
        string Description { get; set; }
        string AutowaitTimeout { get; set; }
        IList<ITestItemViewModel> ChildItems { get; }
        Screenshot Screenshot { get; }
        bool HasScreenshot { get; }
    }
}