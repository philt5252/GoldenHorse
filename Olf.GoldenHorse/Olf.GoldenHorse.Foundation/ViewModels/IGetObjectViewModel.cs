using System;
using System.Windows.Input;
using TestStack.White.UIItems;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface IGetObjectViewModel
    {
        string Type { get; }
        IUIItem UIItem { get; }
        string Description { get; }
        ICommand GetObjectCommand { get; }
        event EventHandler UIItemChanged;
    }
}