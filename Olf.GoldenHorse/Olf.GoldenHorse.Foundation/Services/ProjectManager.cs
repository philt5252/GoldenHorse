using System.IO;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.Services
{
    public static class ProjectManager
    {
        public static string GetScreenshotsFolder(Test test)
        {
            string screenshotsFolder = Path.Combine(
                ProjectSuiteManager.GetProjectFolder(test.Project),
                test.Project.TestsFolder,
                "Screenshots",
                test.Name);

            Directory.CreateDirectory(screenshotsFolder);

            return screenshotsFolder;
        }

        public static string GetAppManagerPath(Project project)
        {
            string appManagerDir = Path.Combine(
                ProjectSuiteManager.GetProjectFolder(project)
                , project.AppManagerFolder);

            if (!Directory.Exists(appManagerDir))
                Directory.CreateDirectory(appManagerDir);

            string appManagerPath = Path.Combine(appManagerDir, "AppManager.gham");
            return appManagerPath;
        }

        /*public static string GetTestPath(string testName)
        {
            string testDir = Path.Combine(CurrentProject.ProjectFolder, CurrentProject.TestsFolder);

            if (!Directory.Exists(testDir))
                Directory.CreateDirectory(testDir);

            string testPath = Path.Combine(testDir, testName + ".ghtest");
            return testPath;
        }*/

    }
}