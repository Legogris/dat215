using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI.Desktop.Content.Controls
{
    /// <summary>
    /// Interaction logic for TreeViewItem.xaml
    /// </summary>
    public partial class TreeViewItem : UserControl
    {
        public TreeViewItem()
        {
            InitializeComponent();
            this.DataContextChanged += TreeViewItem_DataContextChanged;
        }

        void TreeViewItem_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ProductCategory pc = e.NewValue as ProductCategory;
            AbstractTreeViewItem item = null;
            if (pc != null)
            {
                item = new TreeViewCategoryItem();
            }
            else
            {
                Product p = e.NewValue as Product;
                if (p != null)
                {
                    item = new TreeViewProductItem();
                }
            }
            grid.Children.Add(item);
        }
    }
}
