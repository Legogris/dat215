﻿using Data;
using Data.Desktop;
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

namespace UI.Desktop.Content
{
    /// <summary>
    /// Interaction logic for ListControl.xaml
    /// </summary>
    public partial class ListControl : UserControl
    {
        private ShoppingListHandler listHandler;
        private IList<ShoppingItem> detailList
        {
            get { return (IList<ShoppingItem>) detail.ItemsSource; }
            set { detail.ItemsSource = value; }
        }
        private FavoriteList current = null;

        public IList<FavoriteList> ShoppingListsCollection
        { get { return listHandler.GetFavLists(); } }

        public ListControl(DataHandler dh)
        {
            listHandler = dh.GetFavorites();
            InitializeComponent();       
        }

        private void overview_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)e.OriginalSource).DataContext as FavoriteList;
            if (item != null) updateDetail(item);
        }

        private void listNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && current != null)
            {
                current.Name = listNameTextBox.Text;
                listHandler.Change(current);
                reloadOverview();
            }
        }

        private void deleteShoppingListClick(object sender, RoutedEventArgs e)
        {
            if (current != null)
            {
                listHandler.Remove(current);
                reloadOverview();
                updateDetail(null);
                current = null;
            }
        }

        private void updateDetail(FavoriteList item)
        {
            if (item == null)
            {
                current = null;
                detailList = null;
                listTotalPriceLabel.Content = "";
                listNameTextBox.Text = "";
            }
            else
            {
                current = item;
                detailList = item.GetItems();
                listTotalPriceLabel.Content = item.TotalCost + " kr";
                listNameTextBox.Text = item.Name;
            }
        }

        private void reloadOverview()
        {
            overview.ItemsSource = null;
            overview.ItemsSource = ShoppingListsCollection;
        }
    }
}