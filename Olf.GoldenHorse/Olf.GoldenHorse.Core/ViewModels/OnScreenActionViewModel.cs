using Olf.GoldenHorse.Core.Models;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class OnScreenActionViewModel : TestItemViewModelBase, IOnScreenActionViewModel
    {
        private readonly TestItem testItem;

        public OnScreenActionViewModel(TestItem testItem)
        {
            this.testItem = testItem;
            TestItem = testItem;
        }

        public override string Name
        {
            get { return testItem.Control.FriendlyName; }
        }

        public override string Operation { get { return testItem.Operation.Name; } }
        public override string Value { get { return testItem.Operation.ParametersDescription; } }

        public override string Description
        {
            get { return testItem.Description; }
            set { testItem.Description = value; }
        }

        public override string AutowaitTimeout { get; set; }
    }
}