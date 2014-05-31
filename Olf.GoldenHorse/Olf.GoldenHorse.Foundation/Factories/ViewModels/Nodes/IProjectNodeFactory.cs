using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;

namespace Olf.GoldenHorse.Foundation.Factories.ViewModels.Nodes
{
    public interface IProjectNodeFactory
    {
        IDisplayNode Create(Project project);
    }
}