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
        private List<ProjectFile> testFiles;
        private bool isDefaultProject;

        public string Name { get; set; }

        public bool IsDefaultProject
        {
            get { return isDefaultProject; }
            set
            {
                isDefaultProject = value;

                if (isDefaultProject && ProjectSuiteManager.CurrentProjectSuite != null)
                {
                    foreach (Project project in ProjectSuiteManager.CurrentProjectSuite.Projects)
                    {
                        if (Equals(project, this))
                            continue;

                        project.IsDefaultProject = false;
                    }
                }
            }
        }

        public string TestsFolder
        {
            get { return testsFolder; }
            set
            {
                testsFolder = value;
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

        [XmlIgnore]
        public List<ProjectFile> TestFiles
        {
            get
            {
                if (testFiles == null)
                {
                    string projectFolder = ProjectSuiteManager.GetProjectFolder(this);

                    string testsDir = Path.Combine(projectFolder, TestsFolder);

                    if (!Directory.Exists(testsDir))
                        Directory.CreateDirectory(testsDir);

                    testFiles = Directory.EnumerateFiles(testsDir, "*.ghtest")
                        .Select(f =>
                        {
                            FileInfo fileInfo = new FileInfo(f);
                            return new ProjectFile { Name = fileInfo.Name, FilePath = fileInfo.FullName };
                        }).ToList();
                }

                return testFiles;
            }
        }

        [XmlIgnore]
        public string ProjectFolder { get { return ProjectSuiteManager.GetProjectFolder(this); }}

        public Project()
        {
            TestsFolder = "Tests";
            LogsFolder = "Logs";
            AppManagerFolder = "AppManager";
        }
    }
}