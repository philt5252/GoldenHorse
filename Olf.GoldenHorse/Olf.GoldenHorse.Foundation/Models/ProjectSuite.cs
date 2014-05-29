using System.Collections.Generic;

namespace Olf.GoldenHorse.Foundation.Models
{
    public class ProjectSuite
    {
        public string Name { get; set; }
        public string ProjectSuiteFolder { get; set; }
        public List<Project> Projects { get; protected set; }

        public ProjectSuite()
        {
            Projects = new List<Project>();
        }
    }
}