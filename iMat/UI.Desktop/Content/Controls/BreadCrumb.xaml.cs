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
    /// Interaction logic for BreadCrumb.xaml
    /// </summary>
    public partial class BreadCrumb : UserControl
    {

        private ProductCategory category;

        public BreadCrumb()
        {
            InitializeComponent();
            DataContextChanged += BreadCrumb_DataContextChanged;
            titleLabel.MouseUp += BreadCrumb_MouseUp;
        }

        void BreadCrumb_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (ProductCategorySelected != null && category != null)
            {
                ProductCategorySelected.Invoke(this, new ProductCategoryChangedEventArgs(category));
            }
        }

        void BreadCrumb_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ProductCategory pc = e.NewValue as ProductCategory;
            if (pc != null && pc != e.OldValue)
            {
                category = pc;
                titleLabel.Content = pc.Name;
                parentPanel.Visibility = arrowLabel.Visibility = pc.Parent == null ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
                parentPanel.Children.Clear();
                if(pc.Parent != null) {
                    BreadCrumb parentCrumb = new BreadCrumb();
                    parentCrumb.DataContext = pc.Parent;
                    parentCrumb.ProductCategorySelected += parentCrumb_ProductCategorySelected;
                    parentPanel.Children.Add(parentCrumb);
                }
            }
        }

        void parentCrumb_ProductCategorySelected(UserControl sender, ProductCategoryChangedEventArgs e)
        {
            if (ProductCategorySelected != null)
            {
                ProductCategorySelected.Invoke(sender, e);
            }
        }

        public event ProductCategoryChangedHandler ProductCategorySelected;
    }
}
