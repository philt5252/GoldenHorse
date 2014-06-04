﻿using Olf.GoldenHorse.Foundation.Factories.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;

namespace Olf.GoldenHorse.Core.ViewModels.Nodes
{
    public class ProjectSuiteLogsNode : DisplayNode
    {
        public override string Name
        {
            get { return "Project Suite '" + ProjectSuiteManager.CurrentProjectSuite.Name + "'  Logs"; }
        }

        public ProjectSuiteLogsNode(IProjectLogsNodeFactory projectLogsNodeFactory)
        {
            foreach (Project project in ProjectSuiteManager.CurrentProjectSuite.Projects)
            {
                Children.Add(projectLogsNodeFactory.Create(project));
            }
        }
    }
}