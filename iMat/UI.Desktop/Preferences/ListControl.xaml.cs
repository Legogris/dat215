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
using UI.Desktop.Controls;

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
                detail.ItemsSource = null;
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

        private void copyClick(object sender, RoutedEventArgs e)
        {
            if (current == null) return;
            FavoriteList copyList = new FavoriteList(current.Name);
            copyList.Add(current.GetItems());
            listHandler.Add(copyList);
            reloadOverview();
        }

        private void newShoppingList(object sender, RoutedEventArgs e)
        {
            FavoriteList list = new FavoriteList("Ny lista");
            current = list;
            listHandler.Add(list);
            reloadOverview();
            updateDetail(list);
        }

        private void removeItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (current == null || detail.SelectedIndex == -1) return;
            current.Remove(detail.SelectedIndex);
            updateDetail(current);
            reloadOverview();
        }

        private void detail_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)e.OriginalSource).DataContext as ShoppingItem;
            if (item != null)
            {
                amountTextBox.Text = item.Amount.ToString();
            }
        }

        private void amountTextBox_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && current != null && detail.SelectedIndex != -1)
            {
                double d = Convert.ToDouble(amountTextBox.Text);
                current.Change(detail.SelectedIndex, d);
                updateDetail(current); // TODO: nån jävla bug med selectedindex HELVETETASDFGSADGFD
                reloadOverview();
            }
        }
    }
}