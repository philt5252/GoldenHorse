using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;

namespace Olf.GoldenHorse.Core.ViewModels.Nodes
{
    public class TestGroupNode : DisplayNode
    {
        public override string Name
        {
            get { return "Tests"; }
        }

        public TestGroupNode(Project project)
        {
            foreach (ProjectFile projectFile in project.TestFiles)
            {
                Children.Add(new TestNode(projectFile));
            }
        }

        
    }
}