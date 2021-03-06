﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Data;
using AvalonDock.Layout;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Olf.Common.Extensions.Reflection;

namespace Olf.GoldenHorse
{
    public class LayoutDocumentPaneRegionAdapter : RegionAdapterBase<LayoutDocumentPane>
    {
        private Dictionary<object, LayoutDocument> documentDict = new Dictionary<object, LayoutDocument>();

        public LayoutDocumentPaneRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, LayoutDocumentPane regionTarget)
        {
            IRegionManager regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();

            if(region.RegionManager == null)
                region.RegionManager = regionManager;

            if(!regionManager.Regions.ContainsRegionWithName(region.Name))
                regionManager.Regions.Add(region);


            region.ActiveViews.CollectionChanged +=
                (sender, args) =>
                {
                    if (args.Action == NotifyCollectionChangedAction.Add)
                    {
                        foreach (object newView in args.NewItems)
                        {
                            if (!documentDict.ContainsKey(newView))
                            {
                                CreateLayoutDocument(newView);
                                object view = newView;
                                documentDict[newView].Closed +=
                                    (o, e) =>
                                    {
                                        region.Remove(view);
                                        documentDict.Remove(view);
                                    };

                            }

                            regionTarget.Children.Add(documentDict[newView]);
                            regionTarget.SelectedContentIndex = regionTarget.Children.Count - 1;
                        }

                    }
                    else if (args.Action == NotifyCollectionChangedAction.Remove)
                    {
                        foreach (object oldView in args.OldItems)
                        {
                            if (!documentDict.ContainsKey(oldView))
                            {
                                continue;
                            }

                            regionTarget.Children.Remove(documentDict[oldView]);
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
                            CreateLayoutDocument(newView);
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

        private void CreateLayoutDocument(object newView)
        {
            LayoutDocument layoutDocument = new LayoutDocument();
            layoutDocument.IsSelected = true;

            Binding myBinding = new Binding("Tag");
            myBinding.Source = newView;
            myBinding.Mode = BindingMode.TwoWay;
            myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(layoutDocument, LayoutDocument.TitleProperty, myBinding);


            Binding isSelectedBinding = new Binding("IsSelected");
            isSelectedBinding.Source = newView.GetProperty("DataContext");
            BindingOperations.SetBinding(layoutDocument, LayoutDocument.IsSelectedProperty, isSelectedBinding);

            layoutDocument.Content = newView;
            documentDict[newView] = layoutDocument;
        }

        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }
    }
}