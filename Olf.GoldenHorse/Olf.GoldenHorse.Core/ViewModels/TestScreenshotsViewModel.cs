
using System.Linq;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestScreenshotsViewModel : ViewModelBase, ITestScreenshotsViewModel
    {
        private Screenshot[] screenshots;
        private Screenshot selectedScreenshot;

        public Screenshot[] Screenshots
        {
            get { return screenshots; }
            protected set { screenshots = value; }
        }

        public Screenshot SelectedScreenshot
        {
            get { return selectedScreenshot; }
            set
            {
                if (Equals(selectedScreenshot, value))
                    return;

                selectedScreenshot = value; 
                
                OnPropertyChanged("SelectedScreenshot");
            }
        }

        public TestScreenshotsViewModel(Test test)
        {
            Screenshots = test.TestItems.Where(t => t.HasScreenshot)
                .Select(t => t.Screenshot).ToArray();
        }
    }
}