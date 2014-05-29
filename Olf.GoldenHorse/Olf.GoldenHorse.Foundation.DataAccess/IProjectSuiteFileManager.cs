using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.DataAccess
{
    public interface IProjectSuiteFileManager
    {
        ProjectSuite Open(string filePath);
        void Save();
        void Create(ProjectSuite projectSuite);
    }
}