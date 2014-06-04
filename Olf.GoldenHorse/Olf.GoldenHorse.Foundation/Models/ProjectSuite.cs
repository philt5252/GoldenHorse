using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.Serialization;

namespace Olf.GoldenHorse.Foundation.Models
{
    public class ProjectSuite : ILogOwner
    {
        private ObservableCollection<Project> projects;
        public string Name { get; set; }
        public string ProjectSuiteFolder { get; set; }
        public string LogsFolder { get; set; }
        
        public string[] ProjectFiles { get; set; }

        [XmlIgnore]
        public ObservableCollection<Project> Projects
        {
            get { return projects; }
            set
            {

                if (projects != null)
                    projects.CollectionChanged -= ProjectsOnCollectionChanged;

                projects = value;
                
                projects.CollectionChanged += ProjectsOnCollectionChanged;
            }
        }

        private void ProjectsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            ProjectFiles = Projects.Select(p => p.Name).ToArray();
        }

        public ProjectSuite()
        {
            Projects = new ObservableCollection<Project>();
            LogsFolder = "Logs";
        }

        public void RefreshLogFiles()
        {
            throw new NotImplementedException();
        }
    }
}