using System.Collections.Generic;
using System.Windows.Input;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface ITestItemViewModel
    {
        TestItem TestItem { get; set; }
        string Type { get; set; }
        IList<ITestItemViewModel> ChildItems { get; }
        Screenshot Screenshot { get; }
        bool HasScreenshot { get; }
        string ControlId { get; set; }
        string Name { get; set; }
        string Operation { get; }
        string Value { get; }
        string Description { get; set; }
        string AutowaitTimeout { get; set; }
        ICommand EditObjectCommand { get; }
        ICommand EditOperationCommand { get; }
        ICommand EditParameterCommand { get; }
        ICommand EditDescriptionCommand { get; }
    }
}