using System.Collections.Generic;
using System.IO;
using System.Linq;
using Olf.GoldenHorse.Foundation;
using Olf.GoldenHorse.Foundation.DataAccess;

namespace Olf.GoldenHorse.Core.DataAccess
{
    public class RecentFileManager : IRecentFileManager
    {
        public string[] GetRecentFiles()
        {
            string filePath = DefaultData.GoldenHorseRecentProjectsFilePath;

            if (!File.Exists(filePath))
                return new string[]{};

            string[] projects =  File.ReadAllLines(filePath);

            return projects;
        }

        public void AddToRecentFiles(string filePath)
        {
            string recentFilesPath = DefaultData.GoldenHorseRecentProjectsFilePath;

            if (!File.Exists(recentFilesPath))
                File.Create(recentFilesPath).Close();

            List<string> projects = File.ReadAllLines(recentFilesPath).ToList();

            projects.Remove(filePath);

            if (projects.Count >= 5)
                projects.RemoveAt(4);

            projects.Insert(0, filePath);

            File.WriteAllLines(recentFilesPath, projects);
        }
    }
}