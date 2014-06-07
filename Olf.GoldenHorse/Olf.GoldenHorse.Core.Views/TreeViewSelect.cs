using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Olf.GoldenHorse.Core.Views
{
    public class TreeViewSelect : TreeView
    {
        public static readonly DependencyProperty SelectedObjectProperty = DependencyProperty.Register(
            "SelectedObject", typeof (Object), typeof (TreeViewSelect), new PropertyMetadata(default(Object)));

        public Object SelectedObject
        {
            get { return (Object) GetValue(SelectedObjectProperty); }
            set
            {
                SetValue(SelectedObjectProperty, value);
                SelectTreeViewItem(value);
            }
        }

         private void SelectTreeViewItem(object item)
        {
            try
            {
                var tvi = GetContainerFromItem(this, item);

                tvi.Focus();
                tvi.IsSelected = true;

                var selectMethod =
                    typeof(TreeViewItem).GetMethod("Select",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                selectMethod.Invoke(tvi, new object[] { true });
            }
            catch { }
        }

        private TreeViewItem GetContainerFromItem(ItemsControl parent, object item)
        {
            var found = parent.ItemContainerGenerator.ContainerFromItem(item);
            if (found == null)
            {
                for (int i = 0; i < parent.Items.Count; i++)
                {
                    var childContainer = parent.ItemContainerGenerator.ContainerFromIndex(i) as ItemsControl;
                    TreeViewItem childFound = null;
                    if (childContainer != null && childContainer.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                    {
                        childContainer.ItemContainerGenerator.StatusChanged += (o, e) =>
                        {
                            childFound = GetContainerFromItem(childContainer, item);
                        };
                    }
                    else
                    {
                        childFound = GetContainerFromItem(childContainer, item);
                    }
                    if (childFound != null)
                        return childFound;
                }
            }
            return found as TreeViewItem;
        }
    }
    
}