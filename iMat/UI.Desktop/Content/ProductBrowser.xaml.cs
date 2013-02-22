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
using Data;

namespace UI.Desktop
{
    /// <summary>
    /// Interaction logic for ProductBrowser.xaml
    /// </summary>
    public partial class ProductBrowser : UserControl
    {
        public enum ProductsViewMode
        {
            List,
            Grid,
            Tree
        }

        private ProductCategory rootCategory;
        private ProductsViewMode viewMode;
        private ListView listView;
        private GridView gridView;
        private TreeView treeView;

        public ProductCategory RootCategory
        {
            get { return rootCategory; }
            set { rootCategory = value;
                categorySourceUpdated(); }
        }

        public ProductsViewMode ViewMode
        {
            get { return viewMode; }
            set {
                switch (value)
                {
                    case ProductsViewMode.Grid:
                        itemFrame.Content = gridView;
                        break;
                    case ProductsViewMode.List:
                        itemFrame.Content = listView;
                        break;
                    case ProductsViewMode.Tree:
                        itemFrame.Content = treeView;
                        break;
                    default:
                        throw new NotImplementedException();
                }
                viewMode = value;
            }
        }

        public ProductBrowser()
        {
            InitializeComponent();
            listView = new ListView();
            gridView = new GridView();
            treeView = new TreeView();
            ViewMode = ProductsViewMode.List;
        }
        
        private void categorySourceUpdated()
        {
            container.Children.Clear();
            CategoryControl root = new CategoryControl(rootCategory);
            container.Children.Add(root);
            root.Expand();
            Thickness margin = root.stackPanel.Margin;
            margin.Left = 0;
            root.stackPanel.Margin = margin;
            listView.DataContext = rootCategory;
            gridView.DataContext = rootCategory;
            treeView.DataContext = rootCategory;

            root.ProductCategorySelectionChanged += root_ProductCategorySelectionChanged;
        }

        void root_ProductCategorySelectionChanged(UserControl sender, CategoryControl.ProductCategoryChangedEventArgs e)
        {
            listView.DataContext = gridView.DataContext = e.Category;
        }
    }
}
