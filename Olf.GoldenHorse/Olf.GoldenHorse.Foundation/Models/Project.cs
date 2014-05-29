using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Olf.GoldenHorse.Foundation.Services;

namespace Olf.GoldenHorse.Foundation.Models
{
    public class Project
    {
        private string testsFolder;
        private string logsFolder;
        private string appManagerFolder;
        private bool init = false;

        public string Name { get; set; }

        public string TestsFolder
        {
            get { return testsFolder; }
            set
            {
                testsFolder = value;

                if (!init)
                    return;

                string projectFolder = ProjectSuiteManager.GetProjectFolder(this);

                TestFiles = Directory.EnumerateFiles(Path.Combine(projectFolder, TestsFolder), "*.ghtest")
                    .Select(f =>
                    {
                        FileInfo fileInfo = new FileInfo(f);
                        return new ProjectFile {Name = fileInfo.Name, FilePath = fileInfo.FullName};
                    }).ToList();
            }
        }

        public string LogsFolder
        {
            get { return logsFolder; }
            set { logsFolder = value; }
        }

        public string AppManagerFolder
        {
            get { return appManagerFolder; }
            set { appManagerFolder = value; }
        }

        public List<ProjectFile> TestFiles { get; protected set; }

        [XmlIgnore]
        public string ProjectFolder { get { return ProjectSuiteManager.GetProjectFolder(this); }}

        public Project()
        {
            TestsFolder = "Tests";
            LogsFolder = "Logs";
            AppManagerFolder = "AppManager";

            TestFiles = new List<ProjectFile>();

            init = true;
        }
    }
}