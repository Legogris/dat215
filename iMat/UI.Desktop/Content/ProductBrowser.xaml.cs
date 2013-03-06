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
using UI.Desktop.Content;

namespace UI.Desktop
{
    /// <summary>
    /// Interaction logic for ProductBrowser.xaml
    /// </summary>
    public partial class ProductBrowser : UserControl, ProductCategoryChangedEmitter
    {
        public enum ProductsViewMode
        {
            List,
            Grid,
            Tree,
            Start
        }

        private ProductCategory rootCategory;
        private ProductsViewMode viewMode;
        private ListView listView;
        private GridView gridView;
        private TreeView treeView;
        private CategoryControl categoryControl;

        public event ProductCategoryChangedHandler PreviewProductCategorySelectionChanged;
        public event ProductCategoryChangedHandler ProductCategorySelectionChanged;

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
                        showCategories();
                        breadCrumbs.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case ProductsViewMode.List:
                        itemFrame.Content = listView;
                        showCategories();
                        breadCrumbs.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case ProductsViewMode.Tree:
                        itemFrame.Content = treeView;
                        hideCategories();
                        breadCrumbs.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case ProductsViewMode.Start:
                        itemFrame.Content = new StartPage();
                        showCategories();
                        breadCrumbs.Visibility = System.Windows.Visibility.Collapsed;
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
            treeView.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            treeView.Width = 500;
            ViewMode = ProductsViewMode.Start;

            listView.ItemAdded += itemAdded;
            //TODO: Implement for grid view and tree view
            gridView.ItemAdded += itemAdded;
            treeView.ItemAdded += itemAdded;
            breadCrumbs.ItemAdded += itemAdded;
        }

        private void showCategories()
        {
            categoryListScrollView.Visibility = System.Windows.Visibility.Visible;
        }

        private void hideCategories()
        {
            categoryListScrollView.Visibility = System.Windows.Visibility.Collapsed;
        }
        
        private void categorySourceUpdated()
        {
            container.Children.Clear();
            Image image = new Image();
            image.Source = (ImageSource)App.Current.Resources["FeatureImage"];
            image.Width = 24;
            image.Height = 24;
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            sp.Children.Add(image);
            Label homeLabel = new Label();
            homeLabel.Content = "Start";
            homeLabel.FontSize = 14;
            homeLabel.Cursor = Cursors.Hand;
            sp.Children.Add(homeLabel);
            container.Children.Add(sp);
            //container.Children.Add(homeLabel);

            categoryControl = new CategoryControl(rootCategory, 1, this);
            container.Children.Add(categoryControl);
            categoryControl.Expand();
            Thickness margin = categoryControl.stackPanel.Margin;
            margin.Left = 0;
            categoryControl.stackPanel.Margin = margin;
            
            breadCrumbs.DataContext = rootCategory;
            listView.DataContext = rootCategory;
            gridView.DataContext = rootCategory;
            treeView.DataContext = rootCategory;

            homeLabel.MouseUp += homeLabel_MouseUp;
            categoryControl.ProductCategorySelectionChanged += root_ProductCategorySelectionChanged;
            breadCrumbs.ProductCategorySelected += root_ProductCategorySelectionChanged;
        }

        void homeLabel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ViewMode = ProductsViewMode.Start;
        }

        void root_ProductCategorySelectionChanged(UserControl sender, ProductCategoryChangedEventArgs e)
        {
            if (viewMode == ProductsViewMode.Start)
            {
                // TODO: gör så att den sätts till default-ViewModen
                ViewMode = ProductBrowser.ProductsViewMode.List;
            }
            listView.DataContext = gridView.DataContext = breadCrumbs.DataContext = e.Category;
            PreviewProductCategorySelectionChanged.Invoke(sender, e);
        }

        void itemAdded(object sender, CartEventArgs e)
        {
            if (ItemAdded != null)
            {
                ItemAdded.Invoke(sender, e);
            }
        }

        public event ShoppingCartChangedHandler ItemAdded;
    }
}
