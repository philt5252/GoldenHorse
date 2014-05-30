using System;
using System.IO;
using System.Linq;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.Services
{
    public static class ProjectSuiteManager
    {
        private static ProjectSuite currentProjectSuite;

        public static ProjectSuite CurrentProjectSuite
        {
            get
            {
                if(currentProjectSuite == null)
                    throw new Exception("CurrentProjectSuite is null.  Set the ProjectSuiteManager.CurrentProjectSuite to a valid value.");
                
                return currentProjectSuite;
            }
            set { currentProjectSuite = value; }
        }

        public static string GetProjectFolder(Project project)
        {
            return Path.Combine(CurrentProjectSuite.ProjectSuiteFolder, project.Name);
        }

        public static void AddProject(Project project)
        {
            CurrentProjectSuite.Projects.Add(project);
        }

        public static Project GetDefaultProject()
        {
            return CurrentProjectSuite.Projects.FirstOrDefault(p => p.IsDefaultProject);
        }

        public static string GetTestsFolder(Project project)
        {
            return Path.Combine(GetProjectFolder(project), project.TestsFolder);
        }
    }
}