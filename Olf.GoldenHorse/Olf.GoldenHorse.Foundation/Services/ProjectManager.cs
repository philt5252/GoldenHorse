using System.IO;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.Services
{
    public static class ProjectManager
    {
        public static Project CurrentProject { get; set; }

        public static string GetScreenshotsFolder(Test test)
        {
            string screenshotsFolder = Path.Combine(CurrentProject.ProjectFolder,
                CurrentProject.TestsFolder,
                "Screenshots",
                test.Name);

            Directory.CreateDirectory(screenshotsFolder);

            return screenshotsFolder;
        }

        public static string GetAppManagerPath()
        {
            string appManagerDir = Path.Combine(CurrentProject.ProjectFolder, CurrentProject.AppManagerFolder);

            if (!Directory.Exists(appManagerDir))
                Directory.CreateDirectory(appManagerDir);

            string appManagerPath = Path.Combine(appManagerDir, "AppManager.gham");
            return appManagerPath;
        }

        public static string GetTestPath(string testName)
        {
            string testDir = Path.Combine(CurrentProject.ProjectFolder, CurrentProject.TestsFolder);

            if (!Directory.Exists(testDir))
                Directory.CreateDirectory(testDir);

            string testPath = Path.Combine(testDir, testName + ".ghtest");
            return testPath;
        }

        public static string GetProjectPath()
        {
            string projectPath = Path.Combine(CurrentProject.ProjectFolder, CurrentProject.Name + ".ghproj");
            Directory.CreateDirectory(CurrentProject.ProjectFolder);
            return projectPath;
        }
    }
}