using System.Collections.Generic;

namespace Olf.GoldenHorse.Foundation.Models
{
    public interface IAppManager
    {
        List<AppProcess> Processes { get; set; }
        T GetMappedItem<T>(string id) where T : MappedItem;
    }
}