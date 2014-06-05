using System.Drawing;
using System.Windows.Forms;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface IOperationViewModel
    {
        Bitmap Icon { get; }
        string Name { get; }
    }
}