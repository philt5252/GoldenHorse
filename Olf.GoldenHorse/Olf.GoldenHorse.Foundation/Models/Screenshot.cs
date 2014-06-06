using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace Olf.GoldenHorse.Foundation.Models
{
    public class Screenshot
    {
        private ObservableCollection<ScreenshotAdornment> adornments;

        [XmlIgnore]
        public ScreenshotOwner Owner { get; set; }

        public string ImageFile { get; set; }

        public ObservableCollection<ScreenshotAdornment> Adornments
        {
            get { return adornments; }
            set
            {
                if(adornments != null)
                    adornments.CollectionChanged -= AdornmentsOnCollectionChanged;

                adornments = value;

                adornments.CollectionChanged += AdornmentsOnCollectionChanged;
            }
        }

        private void AdornmentsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (ScreenshotAdornment adornment in args.NewItems)
                {
                    adornment.Screenshot = this;
                }
            }
        }

        public DateTime DateTime { get; set; }

        [XmlIgnore]
        public Bitmap Image
        {
            get { return RenderImage(); }
        }

        public Screenshot()
        {
            Adornments = new ObservableCollection<ScreenshotAdornment>();
        }

        public Bitmap RenderImage()
        {
            string imagePath = Path.Combine(Owner.GetScreenshotFolder(), ImageFile);
            Bitmap bitmap = new Bitmap(imagePath);

            foreach (ScreenshotAdornment screenshotAdornment in Adornments)
            {
                screenshotAdornment.Adorn(bitmap);
            }

            return bitmap;
        }
    }
}