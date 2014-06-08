using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace Olf.GoldenHorse.CustomControls
{
    public class TreeViewLineConverter : IValueConverter
    {
        private TreeViewItem treeViewItem;


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TreeViewItem item = (TreeViewItem) value;

            item.IsExpanded = true;
            ContentPresenter dp = item.TemplatedParent as ContentPresenter;
            GridViewRowPresenter dp2 = dp.Parent as GridViewRowPresenter;
            Border border = dp2.Parent as Border;
            treeViewItem = border.TemplatedParent as TreeViewItem;
            


            ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(treeViewItem);


            return ic.ItemContainerGenerator.IndexFromContainer(treeViewItem) == ic.Items.Count - 1;

        }



        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {

            return false;



        }
    }
}