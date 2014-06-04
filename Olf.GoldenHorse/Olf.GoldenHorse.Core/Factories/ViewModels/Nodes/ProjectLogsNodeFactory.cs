using System;
using Olf.GoldenHorse.Core.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.Factories.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;

namespace Olf.GoldenHorse.Core.Factories.ViewModels.Nodes
{
    public class ProjectLogsNodeFactory : IProjectLogsNodeFactory
    {
        private readonly Func<Project, ProjectLogsNode> createNodeFunc;

        public ProjectLogsNodeFactory(Func<Project, ProjectLogsNode> createNodeFunc )
        {
            this.createNodeFunc = createNodeFunc;
        }

        public IDisplayNode Create(Project project)
        {
            return createNodeFunc(project);
        }
    }
}