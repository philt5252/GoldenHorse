using System.IO;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class RecentFileViewModel : IRecentFileViewModel
    {
        public string FileName { get; protected set; }

        public RecentFileViewModel(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            FileName = fileInfo.Name;
        }
    }
}