
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestScreenshotsViewModel : ITestScreenshotsViewModel
    {
        public Screenshot[] Screenshots { get; protected set; }

        public Screenshot SelectedScreenshot { get; set; }

        public TestScreenshotsViewModel()
        {
            
        }
    }
}