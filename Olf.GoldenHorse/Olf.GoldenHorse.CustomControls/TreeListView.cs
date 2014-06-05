using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Olf.GoldenHorse.CustomControls
{
    /// <summary>
    ///
    /// Represents a control that displays hierarchical data in a tree structure
    /// that has items that can expand and collapse.
    /// </summary>
    public class TreeListView : TreeView
    {
        static TreeListView()
        {
            //Override the default style and the default control template
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeListView), new FrameworkPropertyMetadata(typeof(TreeListView)));
            
        }

        /// <summary>
        /// Initialize a new instance of TreeListView.
        /// </summary>
        public TreeListView()
        {
            Columns = new GridViewColumnCollection();
        }

        #region Properties
        /// <summary>
        /// Gets or sets the collection of System.Windows.Controls.GridViewColumn 
        /// objects that is defined for this TreeListView.
        /// </summary>
        public GridViewColumnCollection Columns
        {
            get { return (GridViewColumnCollection)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }        

        /// <summary>
        /// Gets or sets whether columns in a TreeListView can be
        /// reordered by a drag-and-drop operation. This is a dependency property.
        /// </summary>
        public bool AllowsColumnReorder
        {
            get { return (bool)GetValue(AllowsColumnReorderProperty); }
            set { SetValue(AllowsColumnReorderProperty, value); }
        }

        protected override void OnSelectedItemChanged(RoutedPropertyChangedEventArgs<object> e)
        {
            base.OnSelectedItemChanged(e);
            SelectedObject = SelectedItem;
        }

        #endregion

        #region Static Dependency Properties
        // Using a DependencyProperty as the backing store for AllowsColumnReorder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowsColumnReorderProperty =
            DependencyProperty.Register("AllowsColumnReorder", typeof(bool), typeof(TreeListView), new UIPropertyMetadata(null));

        // Using a DependencyProperty as the backing store for Columns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(GridViewColumnCollection),
            typeof(TreeListView),
            new UIPropertyMetadata(null));
        #endregion

        public static readonly DependencyProperty SelectedObjectProperty = DependencyProperty.Register(
            "SelectedObject", typeof (object), typeof (TreeListView), new PropertyMetadata(default(object)));

        public object SelectedObject
        {
            get { return (object)GetValue(SelectedObjectProperty); }
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

    /// <summary>
    /// Represents a control that can switch states in order to expand a node of a TreeListView.
    /// </summary>
    public class TreeListViewExpander : ToggleButton { }

   
   

}
