using System;
using System.Collections.Generic;
using System.Linq;

namespace TestForGolden
{
    public class AppManager
    {
        private readonly Dictionary<string, MappedItem> cachedMappedItemDict = new Dictionary<string, MappedItem>();

        public List<AppProcess> Processes { get; set; }

        public AppManager()
        {
            Processes = new List<AppProcess>();
        }

        public T GetMappedItem<T>(string id) where T : MappedItem
        {
            if (cachedMappedItemDict.ContainsKey(id))
                return cachedMappedItemDict[id] as T;

            MappedItem mappedItem = FindMappedItem(id);

            if (mappedItem == null)
            {
                throw new Exception(string.Format("No mapped item exists with id '{0}'", id));
            }

            cachedMappedItemDict[id] = mappedItem;

            return mappedItem as T;
        }

        private MappedItem FindMappedItem(string id)
        {
            return Processes.Select(mappedItem => FindMappedItem(mappedItem, id))
                .FirstOrDefault(findMappedItem => findMappedItem != null);
        }

        private MappedItem FindMappedItem(MappedItem parentMappedItem, string id)
        {
            if (Equals(parentMappedItem.Id, id))
            {
                return parentMappedItem;
            }

            return parentMappedItem.Children
                .Select(mappedItem => FindMappedItem(mappedItem, id))
                .FirstOrDefault(findMappedItem => findMappedItem != null);
        }
    }
}