using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;

namespace Olf.GoldenHorse.Core.ViewModels.Nodes
{
    public class TestNode : DisplayNode
    {
        private readonly ProjectFile projectFile;

        public override string Name
        {
            get { return projectFile.Name; }
        }

        public TestNode(ProjectFile projectFile)
        {
            this.projectFile = projectFile;
        }
    }
}