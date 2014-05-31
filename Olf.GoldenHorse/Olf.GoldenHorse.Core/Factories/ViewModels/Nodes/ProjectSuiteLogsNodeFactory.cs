using System;
using Olf.GoldenHorse.Core.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.Factories.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;

namespace Olf.GoldenHorse.Core.Factories.ViewModels.Nodes
{
    public class ProjectSuiteLogsNodeFactory : IProjectSuiteLogsNodeFactory
    {
        private readonly Func<ProjectSuiteLogsNode> createNodeFunc;

        public ProjectSuiteLogsNodeFactory(Func<ProjectSuiteLogsNode> createNodeFunc )
        {
            this.createNodeFunc = createNodeFunc;
        }

        public IDisplayNode Create()
        {
            return createNodeFunc();
        }
    }
}