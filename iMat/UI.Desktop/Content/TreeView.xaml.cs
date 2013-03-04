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

namespace UI.Desktop
{
    /// <summary>
    /// Interaction logic for TreeView.xaml
    /// </summary>
    public partial class TreeView : UserControl
    {
        public TreeView()
        {
            InitializeComponent();
            DataContextChanged += TreeView_DataContextChanged;
        }

        void TreeView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ProductCategory pc = e.NewValue as ProductCategory;
            treeView.ItemsSource = pc.SubCategories;
            ItemAdded = null;
        }

        public event Data.ShoppingCartChangedHandler ItemAdded;
    }
}
