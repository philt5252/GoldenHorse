
using System.Collections.Generic;
using System.Collections.Specialized;
using Olf.GoldenHorse.Core.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.Factories.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.Services;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;
using DisplayNode = Olf.GoldenHorse.Core.ViewModels.Nodes.DisplayNode;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class ProjectExplorerViewModel : IProjectExplorerViewModel
    {
        public List<IDisplayNode> Nodes { get; protected set; } 
        public ProjectExplorerViewModel(IProjectSuiteProjectsNodeFactory projectSuiteProjectsNodeFactory,
            IProjectSuiteLogsNodeFactory projectSuiteLogsNodeFactory)
        {
            
            Nodes = new List<IDisplayNode>();

            Nodes.Add(projectSuiteProjectsNodeFactory.Create());
            Nodes.Add(projectSuiteLogsNodeFactory.Create());
        }
    }
}