using System.Collections.Generic;
using System.Security.Cryptography;

namespace TestForGolden
{
    public class OnScreenActionViewModel : TestItemViewModelBase
    {
        private readonly OnScreenAction onScreenAction;

        public OnScreenActionViewModel(OnScreenAction onScreenAction)
        {
            this.onScreenAction = onScreenAction;
        }

        public override ITestItemViewModel Parent { get; set; }

        public override string Name
        {
            get { return onScreenAction.ControlId + Description; }
            set { onScreenAction.ControlId = value; }
        }

        public override string Operation { get; set; }
        public override string Value { get; set; }

        public override string Description
        {
            get { return onScreenAction.Description; }
            set { onScreenAction.Description = value; }
        }

        public override string AutowaitTimeout { get; set; }
    }
}