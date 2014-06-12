using System.Drawing;
using System.Windows.Input;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class OnScreenActionOperationViewModel : IOperationViewModel
    {
        public Bitmap Icon { get { return null; } }
        public string Name { get { return "On Screen Action"; } }
        public ICommand AddToTestCommand { get; protected set; }

        public OnScreenActionOperationViewModel()
        {
            
        }
    }
}