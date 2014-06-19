

using System.Collections;
using System.Collections.Generic;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface ILogItemViewModel
    {
        LogItem LogItem { get; }
        IList<ILogItemViewModel> Children { get; }
        LogItemCategory Category { get; }
        string Description { get; }
        string StartTime { get; }
        bool HasScreenshot { get; }
    }
}