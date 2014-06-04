using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Core.ViewModels.Nodes
{
    public class ProjectLogsNode : DisplayNode
    {
        private readonly Project project;

        public override string Name
        {
            get { return project.Name + " Logs"; }
        }

        public ProjectLogsNode(Project project)
        {
            this.project = project;
        }
    }
}