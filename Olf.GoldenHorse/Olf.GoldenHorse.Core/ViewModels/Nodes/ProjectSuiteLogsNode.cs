using Olf.GoldenHorse.Foundation.Services;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;

namespace Olf.GoldenHorse.Core.ViewModels.Nodes
{
    public class ProjectSuiteLogsNode : DisplayNode
    {
        public override string Name
        {
            get { return "Project Suite '" +ProjectSuiteManager.CurrentProjectSuite.Name + "'  Logs"; }
        }
    }
}