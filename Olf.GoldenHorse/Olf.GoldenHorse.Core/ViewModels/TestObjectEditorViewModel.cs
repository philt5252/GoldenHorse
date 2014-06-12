
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
    public class TestObjectEditorViewModel : ViewModelBase, ITestObjectEditorViewModel
    {
        private readonly TestItem testItem;
        private readonly IGetObjectScreenSelectionViewModelFactory getObjectScreenSelectionViewModelFactory;
        private IGetObjectViewModel[] getObjectViewModels;
        private bool isSelected;
        private MappedItem selectedObject;
        private MappedItem tempSelectedObject;

        public MappedItem[] Objects { get; protected set; }

        public MappedItem TempSelectedObject
        {
            get { return tempSelectedObject; }
            set
            {
                tempSelectedObject = value;

                OnPropertyChanged("TempSelectedObject");
            }
        }

        public MappedItem SelectedObject
        {
            get { return selectedObject; }
            protected set
            {
                selectedObject = value;

                OnPropertyChanged("SelectedObject");
            }
        }

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

        public ICommand SetObjectCommand { get; protected set; }

        public TestObjectEditorViewModel(TestItem testItem,
            IGetObjectScreenSelectionViewModelFactory getObjectScreenSelectionViewModelFactory)
        {
            this.testItem = testItem;
            this.getObjectScreenSelectionViewModelFactory = getObjectScreenSelectionViewModelFactory;

            SetObjectCommand = new DelegateCommand(ExeuteSetObjectCommand);

            if (testItem != null && testItem.Control != null)
            {
                SelectedObject = testItem.Control;
            }

            Objects = testItem.AppManager.Processes.ToArray();

            SetGetObjectViewModels();
        }

        private void ExeuteSetObjectCommand()
        {
            if (TempSelectedObject == null)
                return;

            SelectedObject = TempSelectedObject;
            testItem.ControlId = SelectedObject.Id;
        }

        private void GetObjectViewModelOnUIItemChanged(object sender, EventArgs eventArgs)
        {
            IGetObjectViewModel getObjectViewModel = sender as IGetObjectViewModel;
            MappedItem mappedItem = ExternalAppInfoManager.GetMappedItemFromUIItem(getObjectViewModel.UIItem, testItem.AppManager);
            testItem.ControlId = mappedItem.Id;

            SelectedObject = testItem.Control;
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