using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Olf.GoldenHorse.Foundation.Services;

namespace Olf.GoldenHorse.Foundation.Models
{
    public class Project : ILogOwner
    {
        private string testsFolder;
        private string logsFolder;
        private string appManagerFolder;
        private bool init = false;
        private List<ProjectFile> testFiles;
        private List<ProjectFile> logFiles;
        private bool isDefaultProject;

        public event EventHandler TestFilesChanged;
        public event EventHandler LogFilesChanged;

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
                            return new ProjectFile
                            {
                                Name = fileInfo.Name, 
                                FilePath = fileInfo.FullName,
                                Project = this
                            };
                        }).ToList();
                }

                return testFiles;
            }
        }

        [XmlIgnore]
        public List<ProjectFile> LogFiles
        {
            get
            {
                if (logFiles == null)
                {
                    string projectFolder = ProjectSuiteManager.GetProjectFolder(this);

                    string logsDir = Path.Combine(projectFolder, LogsFolder);

                    if (!Directory.Exists(logsDir))
                        Directory.CreateDirectory(logsDir);

                    logFiles = Directory.EnumerateFiles(logsDir, "*.ghlog", SearchOption.AllDirectories)
                        .Select(f =>
                        {
                            FileInfo fileInfo = new FileInfo(f);
                            return new ProjectFile
                            {
                                Name = fileInfo.Name,
                                FilePath = fileInfo.FullName,
                                Project = this
                            };
                        }).ToList();
                }

                return logFiles;
            }
        }

        [XmlIgnore]
        public string ProjectFolder { get { return ProjectSuiteManager.GetProjectFolder(this); }}

        [XmlIgnore]
        public AppManager AppManager { get; set; }

        public Project()
        {
            TestsFolder = "Tests";
            LogsFolder = "Logs";
            AppManagerFolder = "AppManager";
            AppManager = new AppManager();
        }

        public void RefreshTestFiles()
        {
            testFiles = null;
            OnTestFilesChanged();
        }

        public void RefreshLogFiles()
        {
            logFiles = null;
            OnLogFilesChanged();
        }

        protected virtual void OnTestFilesChanged()
        {
            EventHandler handler = TestFilesChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnLogFilesChanged()
        {
            EventHandler handler = LogFilesChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }

    public interface ILogOwner
    {
        string LogsFolder { get; set; }
        void RefreshLogFiles();
    }
}