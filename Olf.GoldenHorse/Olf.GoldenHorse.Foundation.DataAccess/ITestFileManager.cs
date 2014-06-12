using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.DataAccess
{
    public interface ITestFileManager
    {
        Test CreateTestForProject(Project project);
        void Save(Test currentTest);
        Test Open(string filePath);
        void Rename(ProjectFile projectFile, string newName);
    }
}