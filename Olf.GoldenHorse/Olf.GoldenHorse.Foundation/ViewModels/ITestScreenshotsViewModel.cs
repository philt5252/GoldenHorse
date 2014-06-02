using System.ComponentModel;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Views;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface ITestScreenshotsViewModel : INotifyPropertyChanged
    {
        Screenshot[] Screenshots { get; }
        Screenshot SelectedScreenshot { get; set; }
    }
}