using System.Collections.Generic;

namespace TestForGolden
{
    public abstract class MappedItem
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public List<MappedItem> Children { get; set; }

        protected MappedItem()
        {
            Children = new List<MappedItem>();
        }
    }
}