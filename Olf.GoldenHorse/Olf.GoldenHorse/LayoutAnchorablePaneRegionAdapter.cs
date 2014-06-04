using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Data;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Xceed.Wpf.AvalonDock.Layout;

namespace Olf.GoldenHorse
{
    public class LayoutAnchorablePaneRegionAdapter : RegionAdapterBase<LayoutAnchorablePane>
    {
        private Dictionary<object, LayoutAnchorable> anchorableDict = new Dictionary<object, LayoutAnchorable>();
 
        public LayoutAnchorablePaneRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, LayoutAnchorablePane regionTarget)
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
                                CreateLayoutAnchorable(newView);
                            }
                                //throw new Exception("Could not find view " + newItem
                                //                    + " to activate. Add it do the Views collection before activating.");
                            
                            regionTarget.Children.Add(anchorableDict[newView]);
                        }
                        
                    }
                    
                };

            region.Views.CollectionChanged +=
                (sender, args) =>
                {
                    if (args.Action == NotifyCollectionChangedAction.Add)
                    {
                        foreach (object newView in args.NewItems)
                        {
                            CreateLayoutAnchorable(newView);
                        }

                    }
                    else if (args.Action == NotifyCollectionChangedAction.Remove)
                    {
                        foreach (object oldItem in args.OldItems)
                        {
                            if(region.Views.Contains(oldItem))
                                region.Remove(oldItem);
                        }
                    }
                };
        }

        private void CreateLayoutAnchorable(object newView)
        {
            LayoutAnchorable layoutAnchorable = new LayoutAnchorable();
            layoutAnchorable.IsSelected = true;
            Binding myBinding = new Binding("Tag");
            myBinding.Source = newView;
            BindingOperations.SetBinding(layoutAnchorable, LayoutAnchorable.TitleProperty, myBinding);

            layoutAnchorable.Content = newView;
            anchorableDict[newView] = layoutAnchorable;
        }

        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }
    }
}