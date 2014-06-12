using System.Drawing;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Olf.GoldenHorse.Core.Models;
using Olf.GoldenHorse.Foundation.Events;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class DelayOperationViewModel : IOperationViewModel
    {
        private AddTestItemEvent addTestItemEvent;
        public Bitmap Icon { get { return null; } }
        public string Name { get { return "Delay"; } }
        public ICommand AddToTestCommand { get; protected set; }

        public DelayOperationViewModel(IEventAggregator eventAggregator)
        {
            AddToTestCommand = new DelegateCommand(ExecuteAddToTestCommand);

            addTestItemEvent = eventAggregator.GetEvent<AddTestItemEvent>();
        }

        private void ExecuteAddToTestCommand()
        {
            TestItem testItem = new TestItem();
            testItem.Type = TestItemTypes.Delay;
            testItem.Operation = new DelayOperation{Delay = 5};

            addTestItemEvent.Publish(testItem);
        }
    }
}