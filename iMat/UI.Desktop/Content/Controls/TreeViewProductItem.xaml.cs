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
    /// Interaction logic for TreeViewProductItem.xaml
    /// </summary>
    public partial class TreeViewProductItem : AbstractTreeViewItem
    {
        public TreeViewProductItem()
        {
            InitializeComponent();
            DataContextChanged += TreeViewProductItem_DataContextChanged;
        }

        void TreeViewProductItem_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Product p = e.NewValue as Product;
            if (p != null)
            {
                nameLabel.Content = p.Name;
                priceLabel.Content = p.Price;
            }
        }
    }
}
