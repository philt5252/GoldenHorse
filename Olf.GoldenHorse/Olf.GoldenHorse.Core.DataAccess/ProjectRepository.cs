using System.IO;
using System.Xml.Serialization;
using Olf.GoldenHorse.Foundation.DataAccess;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;

namespace Olf.GoldenHorse.Core.DataAccess
{
    public class ProjectRepository : IProjectRepository
    {
        public Project Open(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(ProjectManager.CurrentProject.GetType());

            using (FileStream fileStream = File.Create(filePath))
            {
                Project project = serializer.Deserialize(fileStream) as Project;
                return project;
            } 
        }

        public void Save(Project project)
        {
            XmlSerializer serializer = new XmlSerializer(ProjectManager.CurrentProject.GetType());
            var projectPath = ProjectManager.GetProjectPath();

            using (FileStream fileStream = File.Create(projectPath))
            {
                serializer.Serialize(fileStream, ProjectManager.CurrentProject);

                fileStream.Flush();
            } 
        }
    }
}