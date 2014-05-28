using System;
using System.Collections.Generic;

namespace TestForGolden
{
    public abstract class MappedItem
    {
        private string id;

        public virtual string Id
        {
            get { return id; }
            set { id = value; }
        }

        public virtual string Name { get; set; }
        public List<MappedItem> Children { get; set; }

        protected MappedItem()
        {
            id = Guid.NewGuid().ToString();
            Children = new List<MappedItem>();
        }
    }
}