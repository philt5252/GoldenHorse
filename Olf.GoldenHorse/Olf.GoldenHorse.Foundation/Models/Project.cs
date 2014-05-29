namespace Olf.GoldenHorse.Foundation.Models
{
    public class Project
    {
        public string Name { get; set; }
        public string ProjectFolder { get; set; }
        public string TestsFolder { get; set; }
        public string LogsFolder { get; set; }
        public string AppManagerFolder { get; set; }

        public Project()
        {
            TestsFolder = "Tests";
            LogsFolder = "Logs";
            AppManagerFolder = "AppManager";
        }
    }
}