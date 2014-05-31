
using System.Collections.ObjectModel;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestDetailsViewModel : ITestDetailsViewModel
    {
        public ObservableCollection<TestItem> TestItems { get; protected set; }
 
        public TestDetailsViewModel(Test test)
        {
            TestItems = new ObservableCollection<TestItem>(test.TestItems);
        }
    }
}