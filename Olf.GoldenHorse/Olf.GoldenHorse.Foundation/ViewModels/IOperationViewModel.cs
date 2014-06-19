using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface IOperationViewModel
    {
        Bitmap Icon { get; }
        string Name { get; }
        ICommand AddToTestCommand { get; }
        TestItem GetNewTestItem();
    }
}