namespace Olf.GoldenHorse.Foundation.Controllers
{
    public interface IProjectSuiteController
    {
        void New();
        void Create(string folderPath, string projectSuiteName);
        void ShowOpen();
        void CancelNew();
        void Open(string fileName);
    }
}