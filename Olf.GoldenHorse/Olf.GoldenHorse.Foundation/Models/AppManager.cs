using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Castle.Core.Internal;

namespace Olf.GoldenHorse.Foundation.Models
{
    public class AppManager
    {
        private readonly Dictionary<string, MappedItem> cachedMappedItemDict = new Dictionary<string, MappedItem>();

        public List<AppProcess> Processes { get; set; }

        public AppManager()
        {
            Processes = new List<AppProcess>();
        }

        public MappedItem GetMappedItem(string id)
        {
            if (id == null)
                return null;

            if (cachedMappedItemDict.ContainsKey(id))
                return cachedMappedItemDict[id];

            MappedItem mappedItem = FindMappedItem(id);

            if (mappedItem == null)
            {
                throw new Exception(string.Format("No mapped item exists with id '{0}'", id));
            }

            cachedMappedItemDict[id] = mappedItem;

            return mappedItem;
        }

        public AppProcess FindOrCreateProcess(string processName)
        {
            AppProcess process = Processes.FirstOrDefault(p => p.Name == processName);

            if (process != null)
                return process;

            process = new AppProcess();
            process.Name = processName;
            process.Type = "process";
            Processes.Add(process);
            
            return process;
        }

        public MappedItem FindOrCreateMappedItem(string parentId, string name, Rect bounds, string type, string text)
        {
            MappedItem parentMappedItem = GetMappedItem(parentId);

            MappedItem mappedItem = parentMappedItem.Children
                .FirstOrDefault(m => m.Name == name
                                && m.Bounds.X == bounds.X
                                && m.Bounds.Y == bounds.Y
                                && m.Bounds.Width == bounds.Width
                                && m.Bounds.Height == bounds.Height
                                && m.Type == type);

            if (mappedItem != null)
                return mappedItem;

            mappedItem = new AppControl();
            mappedItem.ParentId = parentId;
            mappedItem.Bounds = bounds;
            mappedItem.Name = name;

            if (mappedItem.Name.IsNullOrEmpty())
            {
                mappedItem.FriendlyName = text + " " + type;
            }

            mappedItem.Type = type;
            mappedItem.Text = text;

            parentMappedItem.Children.Add(mappedItem);

            cachedMappedItemDict[mappedItem.Id] = mappedItem;

            return mappedItem;
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

        public AppProcess GetProcess(MappedItem mappedItem)
        {
            if (mappedItem.Type == "process")
                return null;

            
            MappedItem parent = GetMappedItem(mappedItem.ParentId);


            while (parent != null)
            {
                if (parent.Type == "process")
                    return parent as AppProcess;

                parent = GetMappedItem(parent.ParentId);
            }

            return null;
        }

        public MappedItem GetWindow(MappedItem mappedItem)
        {
            if (mappedItem.Type == "window")
                return mappedItem;

            MappedItem parent = GetMappedItem(mappedItem.ParentId);
            MappedItem previousParent = null;
            while (parent != null)
            {
                if (parent.Type == "window")
                    return parent;
                previousParent = parent;
                parent = GetMappedItem(parent.ParentId);
            }

            return previousParent;
        }
    }
}