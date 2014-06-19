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
    public class LogGroupOperationViewModel : IOperationViewModel
    {
        private readonly Test test;
        private AddTestItemEvent addTestItemEvent;
        public Bitmap Icon { get { return null; } }
        public string Name { get { return "Log Group"; }}
        public ICommand AddToTestCommand { get; protected set; }


        public LogGroupOperationViewModel(Test test, IEventAggregator eventAggregator)
        {
            this.test = test;
            AddToTestCommand = new DelegateCommand(ExecuteAddToTestCommand);

            addTestItemEvent = eventAggregator.GetEvent<AddTestItemEvent>();
        }

        public TestItem GetNewTestItem()
        {
            return CreateTestItem();
        }

        private void ExecuteAddToTestCommand()
        {
            var testItem = CreateTestItem();

            addTestItemEvent.Publish(testItem);
        }

        private TestItem CreateTestItem()
        {
            TestItem testItem = new TestItem();
            testItem.Type = TestItemTypes.LogGroup;
            testItem.Operation = new LogGroupOperation {GroupName = "Log Group"};
            testItem.Test = test;
            testItem.SupportsChildren = true;
            return testItem;
        }
    }
}