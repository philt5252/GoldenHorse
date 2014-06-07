
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
        private IGetObjectViewModel[] getObjectViewModels;

        public IGetObjectViewModel[] GetObjectViewModels
        {
            get { return getObjectViewModels; }
            protected set
            {
                if (getObjectViewModels != null)
                {
                    foreach (var getObjectViewModel in getObjectViewModels)
                    {
                        getObjectViewModel.UIItemChanged -= GetObjectViewModelOnUIItemChanged;
                    }
                }

                getObjectViewModels = value;

                foreach (var getObjectViewModel in getObjectViewModels)
                {
                    getObjectViewModel.UIItemChanged += GetObjectViewModelOnUIItemChanged;
                }
                
            }
        }

        private void GetObjectViewModelOnUIItemChanged(object sender, EventArgs eventArgs)
        {
            IGetObjectViewModel getObjectViewModel = sender as IGetObjectViewModel;
            MappedItem mappedItem = ExternalAppInfoManager.GetMappedItemFromUIItem(getObjectViewModel.UIItem, testItem.AppManager);
            testItem.ControlId = mappedItem.Id;
        }

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