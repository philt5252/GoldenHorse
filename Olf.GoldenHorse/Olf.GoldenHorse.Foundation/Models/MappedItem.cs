using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Xml.Serialization;

namespace Olf.GoldenHorse.Foundation.Models
{
    public abstract class MappedItem
    {
        private string id;
        private string friendlyName = null;
        private Rect bounds;

        public virtual string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Text { get; set; }
        public string ParentId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }

        public string FriendlyName
        {
            get { return friendlyName ?? Name; }
            set
            {
                friendlyName = value;
            }
        }
        public double X;
        public double Y;
        public double Width;
        public double Height;
        private bool boundsInit;

        [XmlIgnore]
        public Rect Bounds
        {
            get
            {
                if (!boundsInit)
                {
                    bounds = new Rect(X, Y, Width, Height);
                    boundsInit = true;
                }

                return bounds;
            }
            set
            {
                X = value.X;
                Y = value.Y;
                Width = value.Width;
                Height = value.Height;
            }
        }

        public List<MappedItem> Children { get; set; }

        protected MappedItem()
        {
            id = Guid.NewGuid().ToString();
            Children = new List<MappedItem>();
        }
    }
}