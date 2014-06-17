using System;
using System.Windows.Input;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface IRecorderViewModel
    {
        string CurrentTest { get; }
        ICommand RecordCommand { get; }
        ICommand PauseCommand { get; }
        ICommand StopCommand { get; }
        String State { get; set; }
    }
}