using System.IO;
using System.Xml.Serialization;
using Olf.GoldenHorse.Foundation.DataAccess;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;

namespace Olf.GoldenHorse.Core.DataAccess
{
    public class ProjectRepository : IProjectRepository
    {
        private XmlSerializer serializer;
        private const string extension = ".ghproj";

        public ProjectRepository()
        {
            serializer = new XmlSerializer(typeof(Project));
        }

        public Project Open(string filePath)
        {
            using (FileStream fileStream = File.Create(filePath))
            {
                Project project = serializer.Deserialize(fileStream) as Project;
                return project;
            } 
        }

        public void Save(Project project)
        {
            var projectPath =Path.Combine(project.ProjectFolder,  project.Name + extension);

            using (FileStream fileStream = File.Create(projectPath))
            {
                serializer.Serialize(fileStream, project);

                fileStream.Flush();
            } 
        }

        public void Create(Project project)
        {
            var projectDir = project.ProjectFolder;

            if (!Directory.Exists(projectDir))
                Directory.CreateDirectory(projectDir);

            string projectPath = Path.Combine(project.ProjectFolder, project.Name + extension);
            using (FileStream fileStream = File.Create(projectPath))
            {
                serializer.Serialize(fileStream, project);

                fileStream.Flush();
            } 

        }
    }
}