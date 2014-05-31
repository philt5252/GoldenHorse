using System;
using Olf.GoldenHorse.Core.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.Factories.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;

namespace Olf.GoldenHorse.Core.Factories.ViewModels.Nodes
{
    public class ProjectSuiteProjectsNodeFactory : IProjectSuiteProjectsNodeFactory
    {
        private readonly Func<ProjectSuiteProjectsNode> createNodeFunc;

        public ProjectSuiteProjectsNodeFactory(Func<ProjectSuiteProjectsNode> createNodeFunc )
        {
            this.createNodeFunc = createNodeFunc;
        }

        public IDisplayNode Create()
        {
            return createNodeFunc();
        }
    }
}