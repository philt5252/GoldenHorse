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

        public static string GetScreenshotsFolder(Test test)
        {
            string screenshotsFolder = Path.Combine(GetTestsFolder(test.Project), "Screenshots", test.Name);

            if (!Directory.Exists(screenshotsFolder))
                Directory.CreateDirectory(screenshotsFolder);

            return screenshotsFolder;
        }

        public static string GetAppManagerFolder(Project project)
        {
            string appManagerFolder = Path.Combine(GetProjectFolder(project), project.AppManagerFolder);

            if (!Directory.Exists(appManagerFolder))
                Directory.CreateDirectory(appManagerFolder);

            return appManagerFolder;
        }

        public static string GetScreenshotsFolder(Log log)
        {
            return GetLogFolder(log);
        }

        public static string GetLogFolder(Log log)
        {
            string rootFolder = "";

            if (log.Owner is Project)
                rootFolder = GetProjectFolder(log.Owner as Project);

            string logFolder = Path.Combine(rootFolder, log.Owner.LogsFolder, log.Name);

            if (!Directory.Exists(logFolder))
                Directory.CreateDirectory(logFolder);

            return logFolder;
        }

        public static string GetTestScreenshotsFolder(Project project)
        {
            string screenshotsFolder = Path.Combine(GetProjectFolder(project), "Tests", "Screenshots");

            if (!Directory.Exists(screenshotsFolder))
                Directory.CreateDirectory(screenshotsFolder);

            return screenshotsFolder;
        }
    }
}