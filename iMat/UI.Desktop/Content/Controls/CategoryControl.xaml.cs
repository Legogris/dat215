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
    /// Interaction logic for CategoryControl.xaml
    /// </summary>
    public delegate void ProductCategoryChangedHandler(UserControl sender, ProductCategoryChangedEventArgs e);

    public class ProductCategoryChangedEventArgs
    {
        public ProductCategory Category { get; private set; }
        public ProductCategoryChangedEventArgs(ProductCategory pc)
        {
            Category = pc;
        }
    }

    public interface ProductCategoryChangedEmitter
    {
        event ProductCategoryChangedHandler ProductCategorySelectionChanged;
        event ProductCategoryChangedHandler PreviewProductCategorySelectionChanged;
    }

    public partial class CategoryControl : UserControl, ProductCategoryChangedEmitter
    {
        private ProductCategory productCategory;
        private bool expanded = false;
        public event ProductCategoryChangedHandler ProductCategorySelectionChanged;
        public event ProductCategoryChangedHandler PreviewProductCategorySelectionChanged;
        private int level;

        public CategoryControl(ProductCategory pc, int level, ProductCategoryChangedEmitter parentControl)
        {
            InitializeComponent();
            productCategory = pc;
            this.level = level;
            
            if (pc != null)
            {
                categoryLabel.Content = pc.Name;
                categoryLabel.MouseUp += categoryLabel_MouseUp;
            }
            else
            {
                categoryLabel.Content = "DUMMY";
            }
            this.Background = (Brush)App.Current.Resources[string.Format("CategoryLevel{0}Background", level)];
            ProductCategorySelectionChanged += CategoryControl_ProductCategorySelectionChanged;
            if (parentControl != null)
            {
                parentControl.PreviewProductCategorySelectionChanged += parentControl_PreviewProductCategorySelectionChanged;
            }
            ShoppingListHandler listHandler = productCategory as ShoppingListHandler;
            if (listHandler != null)
            {
                icon.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public void Refresh()
        {
            ShoppingListHandler listHandler = productCategory as ShoppingListHandler;
            if (listHandler != null && listHandler.GetFavLists().Count == 0)
            {
                this.Retract();
                this.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                this.Visibility = System.Windows.Visibility.Visible;
            }
            foreach(CategoryControl c in stackPanel.Children.OfType<CategoryControl>()) {
                c.Refresh();
            }
        }

        void parentControl_PreviewProductCategorySelectionChanged(UserControl sender, ProductCategoryChangedEventArgs e)
        {
            updateVisuals(e.Category == productCategory);
            if (PreviewProductCategorySelectionChanged != null)
            {
                PreviewProductCategorySelectionChanged.Invoke(sender, e);
            }
        }
        private void updateVisuals(bool currentlySelected)
        {
            categoryLabel.FontWeight = currentlySelected ? FontWeights.Bold : FontWeights.Normal;
        }

        void CategoryControl_ProductCategorySelectionChanged(UserControl sender, ProductCategoryChangedEventArgs e)
        {
            updateVisuals(e.Category == productCategory);
        }

        void categoryLabel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (productCategory.Parent != null) //Can't expand/contract root category
            {
                if (expanded)
                {
                    Retract();
                }
                else
                {
                    Expand();
                }
            }
            if (productCategory as ShoppingListHandler == null) // Don't replace content just for displaying list of favorite lists
            {
                if (ProductCategorySelectionChanged != null)
                {
                    ProductCategorySelectionChanged.Invoke(this, new ProductCategoryChangedEventArgs(productCategory));
                }
                if (PreviewProductCategorySelectionChanged != null)
                {
                    PreviewProductCategorySelectionChanged.Invoke(this, new ProductCategoryChangedEventArgs(productCategory));
                }
            }

        }

        public void Expand()
        {
            expanded = true;
            if (productCategory != null)
            {
                List<ProductCategory> pcs = new List<ProductCategory>();
                pcs.AddRange(productCategory.SubCategories.OfType<ShoppingListHandler>());
                pcs.AddRange(productCategory.SubCategories.OfType<FavoriteList>());
                pcs.AddRange(productCategory.SubCategories.OfType<ConcreteProductCategory>());
                foreach (ProductCategory pc in pcs)
                {
                    CategoryControl c = new CategoryControl(pc, level+1, this);
                    stackPanel.Children.Add(c);
                    c.ProductCategorySelectionChanged += childProductCategorySelectionChanged;
                }
            }
        }

        void childProductCategorySelectionChanged(UserControl sender, ProductCategoryChangedEventArgs e)
        {
            if (ProductCategorySelectionChanged != null)
            {
                ProductCategorySelectionChanged.Invoke(sender, e);
            }
            if (PreviewProductCategorySelectionChanged != null)
            {
                PreviewProductCategorySelectionChanged.Invoke(sender, e);
            }
        }

        public void Retract() {
            expanded = false;
            stackPanel.Children.Clear();
        }
    }
}
