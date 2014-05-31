using Olf.GoldenHorse.Foundation.Factories.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;

namespace Olf.GoldenHorse.Core.ViewModels.Nodes
{
    public class ProjectNode : DisplayNode
    {
        private readonly Project project;

        public override string Name
        {
            get { return project.Name; }
        }

        public ProjectNode(Project project, ITestGroupNodeFactory testGroupNodeFactory)
        {
            this.project = project;

            Children.Add(testGroupNodeFactory.Create(project));
        }
    }
}