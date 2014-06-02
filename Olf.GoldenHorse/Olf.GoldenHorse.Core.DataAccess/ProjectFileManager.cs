using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using Olf.GoldenHorse.Foundation.DataAccess;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;

namespace Olf.GoldenHorse.Core.DataAccess
{
    public class ProjectFileManager : IProjectFileManager
    {
        private XmlSerializer projectSerializer;
        private XmlSerializer appManagerSerializer;

        private const string extension = ".ghproj";

        public ProjectFileManager()
        {
            projectSerializer = new XmlSerializer(typeof(Project));

            List<Type> types = new List<Type>();

            FileInfo fileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);

            foreach (string assemblyFile in Directory.EnumerateFiles(fileInfo.Directory.FullName)
                .Where(f => f.EndsWith(".exe") || f.EndsWith(".dll")))
            {
                types.AddRange(Assembly.LoadFile(assemblyFile).GetTypes()
                    .Where(t => t.IsSubclassOf(typeof (MappedItem))));
            }

            appManagerSerializer = new XmlSerializer(typeof(AppManager), types.ToArray());
        }

        public Project Open(string filePath)
        {
            Project project;

            using (FileStream fileStream = File.OpenRead(filePath))
            {
                 project = projectSerializer.Deserialize(fileStream) as Project;
            } 

            string appManagerFolder = ProjectSuiteManager.GetAppManagerFolder(project);
                
            using (FileStream fileStream = File.OpenRead(appManagerFolder + "AppManager.gham"))
            {
                project.AppManager = appManagerSerializer.Deserialize(fileStream) as AppManager;
            }

            return project;
        }

        public void Save(Project project)
        {
            var projectPath =Path.Combine(project.ProjectFolder,  project.Name + extension);

            if (!Directory.Exists(project.ProjectFolder))
                Directory.CreateDirectory(project.ProjectFolder);

            using (FileStream fileStream = File.Create(projectPath))
            {
                projectSerializer.Serialize(fileStream, project);

                fileStream.Flush();
            }

            string appManagerFolder = ProjectSuiteManager.GetAppManagerFolder(project);

            using (FileStream fileStream = File.Create(Path.Combine(appManagerFolder, "AppManager.gham")))
            {
                appManagerSerializer.Serialize(fileStream, project.AppManager);
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
                projectSerializer.Serialize(fileStream, project);

                fileStream.Flush();
            }

            string appManagerFolder = ProjectSuiteManager.GetAppManagerFolder(project);

            using (FileStream fileStream = File.Create(Path.Combine(appManagerFolder, "AppManager.gham")))
            {
                appManagerSerializer.Serialize(fileStream, project.AppManager);
            }

        }
    }
}