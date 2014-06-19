using System.Linq;
using System.Windows;
using GongSolutions.Wpf.DragDrop;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.Views
{
    public class OperationDragHandler : DefaultDragHandler
    {
        private readonly ITestItemViewModelFactory testItemViewModelFactory;

        public OperationDragHandler(ITestItemViewModelFactory testItemViewModelFactory)
        {
            this.testItemViewModelFactory = testItemViewModelFactory;
        }

        public override void StartDrag(IDragInfo dragInfo)
        {
            var itemCount = dragInfo.SourceItems.Cast<object>().Count();

            if (itemCount == 1)
            {
                IOperationViewModel operationViewModel = dragInfo.SourceItems.Cast<IOperationViewModel>().First();

                ITestItemViewModel testItemViewModel = testItemViewModelFactory.Create(operationViewModel.GetNewTestItem());
                dragInfo.Data = testItemViewModel;
            }

            dragInfo.Effects = (dragInfo.Data != null) ?
                                 DragDropEffects.Copy | DragDropEffects.Move :
                                 DragDropEffects.None;
        }
    }
}