namespace Olf.GoldenHorse.Foundation.Controllers
{
    public interface IProjectSuiteController
    {
        void New();
        void Create(string folderPath, string projectSuiteName);
        void Open();
        void CancelNew();
    }
}