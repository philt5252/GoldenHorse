using Olf.GoldenHorse.Core.Models;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class OnScreenActionViewModel : TestItemViewModelBase, IOnScreenActionViewModel
    {
        private readonly OnScreenAction onScreenAction;

        public OnScreenActionViewModel(OnScreenAction onScreenAction)
        {
            this.onScreenAction = onScreenAction;
            TestItem = onScreenAction;
        }

        public override string Name
        {
            get { return onScreenAction.Control.FriendlyName; }
        }

        public override string Operation { get { return onScreenAction.Operation.Name; }}
        public override string Value { get { return onScreenAction.Operation.ParametersDescription; } }

        public override string Description
        {
            get { return onScreenAction.Description; }
            set { onScreenAction.Description = value; }
        }

        public override string AutowaitTimeout { get; set; }
    }
}