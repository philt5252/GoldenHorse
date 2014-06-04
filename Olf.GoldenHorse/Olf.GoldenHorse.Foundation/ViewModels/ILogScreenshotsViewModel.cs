

using System.ComponentModel;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface ILogScreenshotsViewModel : INotifyPropertyChanged
    {
        Screenshot[] Screenshots { get; }
        Screenshot SelectedScreenshot { get; set; }
    }
}