using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Olf.GoldenHorse.Foundation.ViewModels.Nodes
{
    public abstract class DisplayNode
    {
        public abstract string Name { get; }
        public ObservableCollection<DisplayNode> Children { get; protected set; }

        protected DisplayNode()
        {
            Children = new ObservableCollection<DisplayNode>();
        }
    }
}