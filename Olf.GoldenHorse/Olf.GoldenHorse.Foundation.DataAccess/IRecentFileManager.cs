namespace Olf.GoldenHorse.Foundation.DataAccess
{
    public interface IRecentFileManager
    {
        string[] GetRecentFiles();
        void AddToRecentFiles(string filePath);
    }
}