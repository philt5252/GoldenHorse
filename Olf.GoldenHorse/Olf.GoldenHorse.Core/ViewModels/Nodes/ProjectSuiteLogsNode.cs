using Olf.GoldenHorse.Foundation.Services;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;

namespace Olf.GoldenHorse.Core.ViewModels.Nodes
{
    public class ProjectSuiteLogsNode : DisplayNode
    {
        public override string Name
        {
            get { return ProjectSuiteManager.CurrentProjectSuite.Name + " Logs"; }
        }
    }
}