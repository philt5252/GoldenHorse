using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Xceed.Wpf.AvalonDock.Layout;

namespace Olf.GoldenHorse
{
    public class LayoutDocumentPaneRegionAdapter : RegionAdapterBase<LayoutDocumentPane>
    {
        private Dictionary<object, LayoutDocument> anchorableDict = new Dictionary<object, LayoutDocument>();

        public LayoutDocumentPaneRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, LayoutDocumentPane regionTarget)
        {
            IRegionManager regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();

            region.RegionManager = regionManager;
            regionManager.Regions.Add(region);


            region.ActiveViews.CollectionChanged +=
                (sender, args) =>
                {
                    if (args.Action == NotifyCollectionChangedAction.Add)
                    {
                        foreach (object newView in args.NewItems)
                        {
                            if (!anchorableDict.ContainsKey(newView))
                            {
                                CreateLayoutDocument(newView);
                            }

                            regionTarget.Children.Add(anchorableDict[newView]);
                        }

                    }

                };

            region.Views.CollectionChanged +=
                (sender, args) =>
                {
                    if (args.Action == NotifyCollectionChangedAction.Add)
                    {
                        foreach (object newItem in args.NewItems)
                        {
                            CreateLayoutDocument(newItem);
                        }

                    }
                    else if (args.Action == NotifyCollectionChangedAction.Remove)
                    {
                        foreach (object oldItem in args.OldItems)
                        {
                            if (region.Views.Contains(oldItem))
                                region.Remove(oldItem);
                        }
                    }
                };
        }

        private void CreateLayoutDocument(object newItem)
        {
            LayoutDocument layoutDocument = new LayoutDocument();
            layoutDocument.Title = GetTag(newItem);
            layoutDocument.Content = newItem;
            anchorableDict[newItem] = layoutDocument;
        }

        private string GetTag(object newItem)
        {
            object tag = newItem.GetType().GetProperty("Tag").GetValue(newItem, null);
            return tag == null ? "" : tag.ToString();
        }

        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }
    }
}