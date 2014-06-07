using Castle.Components.DictionaryAdapter;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public abstract class TabItemViewModel : ViewModelBase, ITabItemViewModel
    {
        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (Equals(isSelected, value))
                    return;

                isSelected = value; 
                OnPropertyChanged("IsSelected");
            }
        }
    }
}