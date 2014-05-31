using System;
using Olf.GoldenHorse.Core.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.Factories.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;

namespace Olf.GoldenHorse.Core.Factories.ViewModels.Nodes
{
    public class TestNodeFactory : ITestNodeFactory
    {
        private readonly Func<ProjectFile, TestNode> createNodeFunc;

        public TestNodeFactory(Func<ProjectFile, TestNode> createNodeFunc )
        {
            this.createNodeFunc = createNodeFunc;
        }

        public IDisplayNode Create(ProjectFile projectFile)
        {
            return createNodeFunc(projectFile);
        }
    }
}