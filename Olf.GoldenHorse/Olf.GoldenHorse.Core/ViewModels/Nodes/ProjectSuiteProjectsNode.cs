using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;

namespace Olf.GoldenHorse.Core.ViewModels.Nodes
{
    public class ProjectSuiteProjectsNode : DisplayNode
    {
        public override string Name
        {
            get { return "Project Suite '" + ProjectSuiteManager.CurrentProjectSuite.Name + "' Projects"; }
        }

        public ProjectSuiteProjectsNode()
        {
            foreach (Project project in ProjectSuiteManager.CurrentProjectSuite.Projects)
            {
                Children.Add(new ProjectNode(project));
            }
        }
    }
}