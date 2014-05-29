
using System.Collections.Generic;
using System.Collections.Specialized;
using Olf.GoldenHorse.Core.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.Services;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class ProjectExplorerViewModel : IProjectExplorerViewModel
    {
        public List<DisplayNode> Nodes { get; protected set; } 
        public ProjectExplorerViewModel()
        {
            
            Nodes = new List<DisplayNode>();

            Nodes.Add(new ProjectSuiteProjectsNode());
            Nodes.Add(new ProjectSuiteLogsNode());
        }
    }
}