using System;
using System.Windows.Input;
using Olf.GoldenHorse.Foundation.ViewModels;
using TestStack.White.UIItems;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public abstract class GetObjectViewModel : IGetObjectViewModel
    {
        private IUIItem uiItem;

        public event EventHandler UIItemChanged;

        public IUIItem UIItem
        {
            get { return uiItem; }
            protected set
            {
                uiItem = value;
                OnUIItemChanged();
            }
        }

        public abstract string Description { get; }
        public ICommand GetObjectCommand { get; protected set; }

        protected virtual void OnUIItemChanged()
        {
            EventHandler handler = UIItemChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}