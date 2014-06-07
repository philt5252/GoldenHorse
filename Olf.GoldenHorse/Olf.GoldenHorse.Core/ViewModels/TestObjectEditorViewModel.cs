
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.Commands;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using Olf.GoldenHorse.Core.Services;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using TestStack.White.UIItems;
using Application = System.Windows.Application;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestObjectEditorViewModel : ITestObjectEditorViewModel
    {
        private readonly TestItem testItem;
        private readonly IGetObjectScreenSelectionViewModelFactory getObjectScreenSelectionViewModelFactory;

        public IGetObjectViewModel[] GetObjectViewModels { get; protected set; }

        public TestObjectEditorViewModel(TestItem testItem,
            IGetObjectScreenSelectionViewModelFactory getObjectScreenSelectionViewModelFactory)
        {
            this.testItem = testItem;
            this.getObjectScreenSelectionViewModelFactory = getObjectScreenSelectionViewModelFactory;

            SetGetObjectViewModels();
        }

        private void SetGetObjectViewModels()
        {
            IGetObjectScreenSelectionViewModel getObjectScreenSelectionViewModel = getObjectScreenSelectionViewModelFactory.Create();
            GetObjectViewModels = new IGetObjectViewModel[] { getObjectScreenSelectionViewModel };     
        }

        private void ExecuteDoScreenSelectionCommand()
        {
            
        }
    }
}