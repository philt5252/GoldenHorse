using System;
using Olf.GoldenHorse.Core.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.Factories.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;

namespace Olf.GoldenHorse.Core.Factories.ViewModels.Nodes
{
    public class TestGroupNodeFactory : ITestGroupNodeFactory
    {
        private readonly Func<Project, TestGroupNode> createNodeFunc;

        public TestGroupNodeFactory(Func<Project, TestGroupNode> createNodeFunc )
        {
            this.createNodeFunc = createNodeFunc;
        }

        public IDisplayNode Create(Project project)
        {
            return createNodeFunc(project);
        }
    }
}