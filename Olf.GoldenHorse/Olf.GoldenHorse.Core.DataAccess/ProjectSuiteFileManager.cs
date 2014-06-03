using System.IO;
using System.Xml.Serialization;
using Olf.GoldenHorse.Foundation.DataAccess;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;

namespace Olf.GoldenHorse.Core.DataAccess
{
    public class ProjectSuiteFileManager : IProjectSuiteFileManager
    {
        private readonly IProjectFileManager projectFileManager;
        private XmlSerializer serializer;
        private const string extension = ".ghps";

        public ProjectSuiteFileManager(IProjectFileManager projectFileManager)
        {
            this.projectFileManager = projectFileManager;
            serializer = new XmlSerializer(typeof(ProjectSuite));
        }

        public ProjectSuite Open(string filePath)
        {
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                ProjectSuite projectSuite = serializer.Deserialize(fileStream) as ProjectSuite;
                ProjectSuiteManager.CurrentProjectSuite = projectSuite;

                FileInfo fileInfo = new FileInfo(filePath);

                foreach (string projectFile in projectSuite.ProjectFiles)
                {
                    string projectFilePath = Path.Combine(fileInfo.DirectoryName, projectFile, projectFile + ".ghproj");
                    Project project = projectFileManager.Open(projectFilePath);

                    projectSuite.Projects.Add(project);
                }


                return projectSuite;
            } 
        }

        public void Save()
        {
            var projectSuitePath = Path.Combine(
                ProjectSuiteManager.CurrentProjectSuite.ProjectSuiteFolder,
                ProjectSuiteManager.CurrentProjectSuite.Name + extension);

            using (FileStream fileStream = File.Create(projectSuitePath))
            {
                serializer.Serialize(fileStream, projectSuitePath);

                fileStream.Flush();
            } 
        }

        public void Create(ProjectSuite projectSuite)
        {
            var projectSuiteFolder = projectSuite.ProjectSuiteFolder;

            if (!Directory.Exists(projectSuiteFolder))
                Directory.CreateDirectory(projectSuiteFolder);

            string projectSuitePath = Path.Combine(projectSuiteFolder, projectSuite.Name + extension);
            using (FileStream fileStream = File.Create(projectSuitePath))
            {
                serializer.Serialize(fileStream, projectSuite);

                fileStream.Flush();
            }

            foreach (Project project in projectSuite.Projects)
            {
                projectFileManager.Save(project);
            }

        } 
    }
}