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
    public class LogMessageOperationViewModel : IOperationViewModel
    {
        private AddTestItemEvent addTestItemEvent;
        public Bitmap Icon { get { return null; } }
        public string Name { get { return "Log Message"; }}
         public ICommand AddToTestCommand { get; protected set; }

         public LogMessageOperationViewModel(IEventAggregator eventAggregator)
        {
            AddToTestCommand = new DelegateCommand(ExecuteAddToTestCommand);

            addTestItemEvent = eventAggregator.GetEvent<AddTestItemEvent>();
        }

        private void ExecuteAddToTestCommand()
        {
            TestItem testItem = new TestItem();
            testItem.Type = TestItemTypes.Message;
            testItem.Operation = new LogMessageOperation{Message = "Default Message"};

            addTestItemEvent.Publish(testItem);
        }
    }
}