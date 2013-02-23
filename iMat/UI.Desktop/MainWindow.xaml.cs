﻿using Data.Desktop;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataHandler dataHandler;
        public MainWindow()
        {
            InitializeComponent();
            initDataBinding();
        }
        
        private void initDataBinding() {
            dataHandler = DataHandler.ReadFromFile("asdf", "Resources/products.txt");
            productBrowser.RootCategory = dataHandler.GetRootCategory();
            productBrowser.ItemAdded += productBrowser_ItemAdded;
            dataHandler.GetCart().Changed += shoppingCart_ItemAdded;
            shoppingCart.ShoppingCart = dataHandler.GetCart();
        }

        void shoppingCart_ItemAdded(object sender, Data.CartEventArgs e)
        {
            shoppingCart.ShoppingCartChanged(e);
        }

        void productBrowser_ItemAdded(object sender, Data.CartEventArgs e)
        {
            dataHandler.GetCart().Add(e.ShoppingItem);
        }

        private void menuItemQuitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void gridViewButton_Click(object sender, RoutedEventArgs e)
        {
            productBrowser.ViewMode = ProductBrowser.ProductsViewMode.Grid;
        }

        private void listViewButton_Click(object sender, RoutedEventArgs e)
        {
            productBrowser.ViewMode = ProductBrowser.ProductsViewMode.List;
        }

        private void treeViewButton_Click(object sender, RoutedEventArgs e)
        {
            productBrowser.ViewMode = ProductBrowser.ProductsViewMode.Tree;
        }
    }
}
