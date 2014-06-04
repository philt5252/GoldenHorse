using System;
using Olf.GoldenHorse.Core.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.Factories.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;

namespace Olf.GoldenHorse.Core.Factories.ViewModels.Nodes
{
    public class LogNodeFactory : ILogNodeFactory
    {
        private readonly Func<ProjectFile, LogNode> createNodeFunc;

        public LogNodeFactory(Func<ProjectFile, LogNode> createNodeFunc)
        {
            this.createNodeFunc = createNodeFunc;
        }

        public IDisplayNode Create(ProjectFile projectFile)
        {
            return createNodeFunc(projectFile);
        }
    }
}