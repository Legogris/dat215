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

    public partial class CategoryControl : UserControl
    {
        private ProductCategory productCategory;
        private bool expanded = false;
        public event ProductCategoryChangedHandler ProductCategorySelectionChanged;


        public CategoryControl(ProductCategory pc)
        {
            InitializeComponent();
            productCategory = pc;
            if (pc != null)
            {
                categoryLabel.Content = pc.Name;
                categoryLabel.MouseUp += categoryLabel_MouseUp;
            }
            else
            {
                categoryLabel.Content = "DUMMY";
            }
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
            ProductCategorySelectionChanged.Invoke(this, new ProductCategoryChangedEventArgs(productCategory));
        }

        public void Expand()
        {
            expanded = true;
            if (productCategory != null)
            {
                foreach (ProductCategory pc in productCategory.SubCategories)
                {
                    CategoryControl c = new CategoryControl(pc);
                    stackPanel.Children.Add(c);
                    c.ProductCategorySelectionChanged += childProductCategorySelectionChanged;
                }
            }
        }

        void childProductCategorySelectionChanged(UserControl sender, ProductCategoryChangedEventArgs e)
        {
            ProductCategorySelectionChanged.Invoke(sender, e);
        }

        public void Retract() {
            expanded = false;
            stackPanel.Children.Clear();
        }
    }
}
