using System;
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
    public class RunTestOperationViewModel : IOperationViewModel
    {
        private readonly Test test;
        private readonly Func<RunTestOperation> createOpFunc;
        private AddTestItemEvent addTestItemEvent;
        public Bitmap Icon { get { return null; } }
        public string Name { get { return "Run Test"; } }
        public ICommand AddToTestCommand { get; protected set; }


        public RunTestOperationViewModel(Test test, IEventAggregator eventAggregator, Func<RunTestOperation> createOpFunc )
        {
            this.test = test;
            this.createOpFunc = createOpFunc;
            AddToTestCommand = new DelegateCommand(ExecuteAddToTestCommand);

            addTestItemEvent = eventAggregator.GetEvent<AddTestItemEvent>();
        }

        private void ExecuteAddToTestCommand()
        {
            var testItem = CreateTestItem();

            addTestItemEvent.Publish(testItem);
        }

        public TestItem GetNewTestItem()
        {
            return CreateTestItem();
        }

        private TestItem CreateTestItem()
        {
            TestItem testItem = new TestItem();
            testItem.Type = TestItemTypes.Delay;
            testItem.Operation = createOpFunc();//new RunTestOperation();
            testItem.Test = test;
            return testItem;
        } 
    }
}