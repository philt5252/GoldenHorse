using System.Drawing;
using System.Net.Mime;
using System.Windows;
using System.Windows.Media.Imaging;
using Olf.GoldenHorse.Foundation.Factories.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;

namespace Olf.GoldenHorse.Core.ViewModels.Nodes
{
    public class ProjectSuiteProjectsNode : DisplayNode
    {
        public override bool IsRenamable
        {
            get { return false; }
        }

        public override string Name
        {
            get { return "Project Suite '" + ProjectSuiteManager.CurrentProjectSuite.Name + "' Projects"; }
            set{}
        }

        public ProjectSuiteProjectsNode(IProjectNodeFactory projectNodeFactory)
        {
            foreach (Project project in ProjectSuiteManager.CurrentProjectSuite.Projects)
            {
                Children.Add(projectNodeFactory.Create(project));
            }
        }
    }
}