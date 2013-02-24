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
    /// Interaction logic for TreeViewCategoryItem.xaml
    /// </summary>
    public partial class TreeViewCategoryItem : AbstractTreeViewItem
    {
        public TreeViewCategoryItem()
        {
            InitializeComponent();
            this.DataContextChanged += TreeViewCategoryItem_DataContextChanged;
        }

        void TreeViewCategoryItem_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ProductCategory pc = e.NewValue as ProductCategory;
            if (pc != null)
            {
                nameLabel.Content = pc.Name;
            }
        }
    }
}
