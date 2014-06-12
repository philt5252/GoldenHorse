using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Input;

namespace Olf.GoldenHorse.Foundation.ViewModels.Nodes
{
    public interface IDisplayNode
    {
        string Name { get; set; }
        bool IsRenamable { get; }
        ObservableCollection<IDisplayNode> Children { get; }
        ICommand DefaultCommand { get; }
    }
}