using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.DataAccess
{
    public interface ILogFileManager
    {
        void Save(Log log);
        Log Open(string filePath);
    }
}