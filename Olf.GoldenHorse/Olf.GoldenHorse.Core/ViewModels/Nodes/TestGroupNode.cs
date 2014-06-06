using System;
using System.Drawing;
using Olf.GoldenHorse.Foundation.Factories.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;

namespace Olf.GoldenHorse.Core.ViewModels.Nodes
{
    public class TestGroupNode : DisplayNode
    {
        private readonly Project project;
        private readonly ITestNodeFactory testNodeFactory;

        public override string Name
        {
            get { return "Tests"; }
        }

        public override Bitmap Icon
        {
            get { return Resources.project1; }
        }   

        public TestGroupNode(Project project, ITestNodeFactory testNodeFactory)
        {
            this.project = project;
            this.testNodeFactory = testNodeFactory;
            project.TestFilesChanged += ProjectOnTestFilesChanged;

            RefreshTests();
        }

        protected virtual void ProjectOnTestFilesChanged(object sender, EventArgs eventArgs)
        {
            RefreshTests();
        }

        private void RefreshTests()
        {
            Children.Clear();
            foreach (ProjectFile projectFile in project.TestFiles)
            {
                Children.Add(testNodeFactory.Create(projectFile));
            }
        }
    }
}