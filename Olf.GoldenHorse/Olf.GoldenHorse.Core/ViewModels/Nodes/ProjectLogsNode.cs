using System;
using Olf.GoldenHorse.Foundation.Factories.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Core.ViewModels.Nodes
{
    public class ProjectLogsNode : DisplayNode
    {
        private readonly Project project;
        private readonly ILogNodeFactory logNodeFactory;

        public override string Name
        {
            get { return project.Name + " Logs"; }
        }

        public ProjectLogsNode(Project project, ILogNodeFactory logNodeFactory)
        {
            this.project = project;
            this.logNodeFactory = logNodeFactory;
            project.LogFilesChanged += ProjectOnLogFilesChanged;

            RefreshLogs();
        }

        private void ProjectOnLogFilesChanged(object sender, EventArgs eventArgs)
        {
            RefreshLogs();
        }

        private void RefreshLogs()
        {
            Children.Clear();
            foreach (ProjectFile projectFile in project.LogFiles)
            {
                Children.Add(logNodeFactory.Create(projectFile));
            }
        }
    }
}