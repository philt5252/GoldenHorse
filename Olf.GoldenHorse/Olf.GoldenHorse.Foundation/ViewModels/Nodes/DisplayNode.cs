using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Input;

namespace Olf.GoldenHorse.Foundation.ViewModels.Nodes
{
    public abstract class DisplayNode
    {
        public abstract string Name { get; }
        public virtual Bitmap Icon { get { return null; } }

        public ObservableCollection<DisplayNode> Children { get; protected set; }

        public ICommand DefaultCommand { get; protected set; }

        protected DisplayNode()
        {
            Children = new ObservableCollection<DisplayNode>();
        }
    }
}