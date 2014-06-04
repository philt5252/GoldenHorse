

using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface ILogItemViewModel
    {
        LogItem LogItem { get; }
        LogItemCategory Category { get; }
        string Description { get; }
        string StartTime { get; }
        bool HasScreenshot { get; }
    }
}