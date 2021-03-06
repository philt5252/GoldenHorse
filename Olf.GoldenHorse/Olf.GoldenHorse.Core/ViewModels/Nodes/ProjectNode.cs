﻿using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Olf.GoldenHorse.Foundation.DataAccess;
using Olf.GoldenHorse.Foundation.Factories.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;

namespace Olf.GoldenHorse.Core.ViewModels.Nodes
{
    public class ProjectNode : DisplayNode
    {
        private readonly Project project;

        public override bool IsRenamable
        {
            get { return false; }
        }

        public override string Name
        {
            get { return project.Name; }

            set{}
        }

        public ProjectNode(Project project, ITestGroupNodeFactory testGroupNodeFactory)
        {
            this.project = project;

            Children.Add(testGroupNodeFactory.Create(project));
        }
    }
}