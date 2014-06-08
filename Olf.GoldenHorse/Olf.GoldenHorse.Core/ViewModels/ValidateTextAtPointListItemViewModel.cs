using System.Windows.Input;
using Olf.GoldenHorse.Core.Models;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class ValidateTextAtPointListItemViewModel : ValidationListItemViewModel
    {
        public override string Name
        {
            get { return "Validate Text at Point"; }
        }

        public ValidateTextAtPointListItemViewModel(IRecordingController recordingController)
            : base(recordingController)
        {
        }

        protected override TestItem CreateOnScreenValidation()
        {
            TestItem onScreenValidation = new TestItem{Type = TestItemTypes.ValidateTextAtPoint};
            ValidateTextAtPointOperation operation = new ValidateTextAtPointOperation();

            onScreenValidation.Operation = operation;

            return onScreenValidation;
        }
    }
}