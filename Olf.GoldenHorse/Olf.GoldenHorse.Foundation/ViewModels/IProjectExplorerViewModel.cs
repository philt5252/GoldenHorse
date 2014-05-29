using System.Collections.Generic;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface IProjectExplorerViewModel
    {
        List<DisplayNode> Nodes { get; }
    }
}