﻿using Data;
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
    /// Interaction logic for ListViewItem.xaml
    /// </summary>
    public partial class ListViewItem : UserControl
    {
        private Product product;
        private AbstractSelector sel;

        public Product Product { get { return product; } }

        public ListViewItem(Product p)
        {
            InitializeComponent();
            product = p;
            initContent();
        }

        private void initContent() {
            productName.Content = product.Name;
            if (product.UnitSuffix == "kg")
            {
                sel = new BulkSelector(product);
            }
            else {
                sel = new ItemSelector(product);
            }
            sel.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            sel.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            try
            {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.UriSource = new Uri("pack://application:,,,/UI.Desktop;component/Resources/ProductImages/" + product.ImageName);
                bmp.EndInit();
                productImage.Source = bmp;
            }
            catch { }
        }

        private void addToShoppingCartButton_Click(object sender, RoutedEventArgs e)
        {
            if (ItemAdded != null)
            {
                ItemAdded.Invoke(this, new Data.CartEventArgs(Data.CartEventArgs.CartEventType.Add, new Data.ShoppingItem(product, sel.NumberOfItems)));
            }
        }

        public event Data.ShoppingCartChangedHandler ItemAdded;
    }
}