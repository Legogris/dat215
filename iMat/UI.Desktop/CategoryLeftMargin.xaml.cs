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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class CategoryLeftMargin : UserControl
    {
        public enum ProductsViewMode
        {
            List,
            Grid
        }

        private ProductCategory rootCategory;
        private ProductsViewMode viewMode;
        private ListView listView;
        private GridView gridView;

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
                        throw new NotImplementedException();
                    case ProductsViewMode.List:
                        itemFrame.Content = listView;
                        break;
                    default:
                        throw new NotImplementedException();
                }
                viewMode = value;
            }
        }

        public CategoryLeftMargin()
        {
            InitializeComponent();
            listView = new ListView();
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

            root.ProductCategorySelectionChanged += root_ProductCategorySelectionChanged;
        }

        void root_ProductCategorySelectionChanged(UserControl sender, CategoryControl.ProductCategoryChangedEventArgs e)
        {
            listView.DataContext = e.Category;
        }
    }
}
