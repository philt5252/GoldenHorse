
using System.Linq;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestScreenshotsViewModel : ITestScreenshotsViewModel
    {
        private Screenshot[] screenshots;

        public Screenshot[] Screenshots
        {
            get { return screenshots; }
            protected set { screenshots = value; }
        }

        public Screenshot SelectedScreenshot { get; set; }

        public TestScreenshotsViewModel(Test test)
        {
            Screenshots = test.TestItems.Where(t => t.HasScreenshot)
                .Select(t => t.Screenshot).ToArray();
        }
    }
}