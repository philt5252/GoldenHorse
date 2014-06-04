using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;

namespace Olf.GoldenHorse.Foundation.Factories.ViewModels.Nodes
{
    public interface ILogNodeFactory
    {
        IDisplayNode Create(ProjectFile projectFile);
    }
}