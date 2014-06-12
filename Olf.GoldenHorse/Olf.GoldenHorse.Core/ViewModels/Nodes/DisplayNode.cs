using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Input;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;

namespace Olf.GoldenHorse.Core.ViewModels.Nodes
{
    public abstract class DisplayNode : IDisplayNode
    {
        public abstract string Name { get; set; }
        public abstract bool IsRenamable { get; }
        public virtual string Type{get { return this.GetType().Name.Replace("Node", ""); }}

        public ObservableCollection<IDisplayNode> Children { get; protected set; }

        public ICommand DefaultCommand { get; protected set; }

        protected DisplayNode()
        {
            Children = new ObservableCollection<IDisplayNode>();
        }
    }
}