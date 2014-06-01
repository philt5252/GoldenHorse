using System;
using System.Collections.Generic;

namespace Olf.GoldenHorse.Foundation.Models
{
    public abstract class MappedItem
    {
        private string id;

        public virtual string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string ParentId { get; set; }
        public string Type { get; set; }
        public virtual string Name { get; set; }
        public List<MappedItem> Children { get; set; }

        protected MappedItem()
        {
            id = Guid.NewGuid().ToString();
            Children = new List<MappedItem>();
        }
    }
}