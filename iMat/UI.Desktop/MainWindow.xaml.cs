﻿using Data;
using Data.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.Desktop.Content;
using UI.Desktop.Controls;
using UI.Desktop.Preferences;

namespace UI.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly String PRODUCTS = "Resources/products.txt";
        public static readonly String DATABASE = "Resources/favs.db";
        private DataHandler dataHandler;
        private User user;
        private static ListContextMenuManager listContextMenu;
        public static ContextMenu ListContextMenu { get { return listContextMenu.ShoppingListContextMenu; } }
        public static Window WindowContainer { get; set; }

        public MainWindow()
        {
            WindowContainer = this;
            dataHandler = DataHandler.ReadFromFile(DATABASE, PRODUCTS);
            user = dataHandler.GetUser();
            listContextMenu = new ListContextMenuManager(this, dataHandler);
            DataContext = listContextMenu;
            InitializeComponent();
            filterButton.Click += filterButton_Click;
            initDataBinding();
        }

        void filterButton_Click(object sender, RoutedEventArgs e)
        {
            filterButton.ContextMenu.IsOpen = true;
            e.Handled = true;
        }

        private void initDataBinding() {
            productBrowser.RootCategory = dataHandler.GetRootCategory();
            productBrowser.ItemAdded += productBrowser_ItemAdded;
            dataHandler.GetCart().Changed += shoppingCart_ItemAdded;
            shoppingCart.DataHandler = dataHandler;
            shoppingCart.SetListContextMenu(listContextMenu.ShoppingListContextMenu);
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

        private void shoppingCart_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dataHandler.WriteToFile(DATABASE);
        }

        private void shoppingListPrefsClick(object sender, RoutedEventArgs e)
        {
            openPreferences(PreferencesWindow.StartupView.List);
        }

        private void openPreferences(PreferencesWindow.StartupView prefs)
        {
            PreferencesWindow pref = new PreferencesWindow(dataHandler, prefs);
            pref.Owner = this;
            pref.ShowDialog();
        }

        private void menuItemPropertiesClick(object sender, RoutedEventArgs e)
        {
            openPreferences(PreferencesWindow.StartupView.Account);
        }

        private void shoppingListComboBoxSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(listContextMenu.ShoppingListDummy.Name);
            //TODO: koppla till product browser
        }

        public void screenFlash()
        {
            comboBoxBrush.Color = Colors.Black;
            ColorAnimation color = new ColorAnimation();
            color.To = Colors.White;
            color.Duration = TimeSpan.FromSeconds(2);
            Storyboard.SetTargetName(color, "comboBoxBrush");
            Storyboard.SetTargetProperty(color, new PropertyPath(SolidColorBrush.ColorProperty));
            Storyboard story = new Storyboard();
            story.Children.Add(color);
            story.Begin(this);
        }

        private void menuItemAbout_Click_1(object sender, RoutedEventArgs e)
        {
            Window about = new About();
            about.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            ((MenuItem)sender).IsChecked = true;
            e.Handled = true;
        }

        private void menuItemLogOut_Click(object sender, RoutedEventArgs e)
        {
            if (user != null) 
            {
                //Doesn't work for shit
                user = null;
            }
        }
    }

    public class ListContextMenuManager {
        public ContextMenu ShoppingListContextMenu { get; private set; }
        private DataHandler dataHandler;
        private ShoppingListHandler listHandler { get; set; }
        public IList<FavoriteList> ShoppingListsCollection { get { return listHandler.GetFavLists(); } }
        public FavoriteList ShoppingListDummy { get; set; } // TODO: selected-hook för dropdown
        private MainWindow mainWindow;

        public ListContextMenuManager(MainWindow window, DataHandler dh)
        {
            listHandler = dh.GetFavorites();
            dataHandler = dh;
            mainWindow = window;
            createContextMenu();
        }

        private void createContextMenu()
        {
            listHandler.Changed += listHandler_Changed;
            ShoppingListContextMenu = new ContextMenu();
            foreach (FavoriteList list in listHandler.GetFavLists())
            {
                MenuItem item = menuItemFactory(list);
                ShoppingListContextMenu.Items.Add(item);
            }
            ShoppingListContextMenu.Items.Add(new Separator());
            MenuItem addNew = new MenuItem();
            addNew.Header = "Lägg till i ny lista...";
            addNew.DataContext = null;
            ShoppingListContextMenu.Items.Add(addNew);
            addNew.Click += shoppingList_Click;
        }

        private MenuItem menuItemFactory(FavoriteList list)
        {
            MenuItem newList = new MenuItem();
            newList.DataContext = list;
            newList.Header = list.Name;
            newList.Click += shoppingList_Click;
            return newList;
        }

        void shoppingList_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuItem;
            while (item.Parent is MenuItem)
            {
                item = (MenuItem)item.Parent;
            }
            var menu = item.Parent as ContextMenu;
            if (menu != null)
            {
                var droidsYouAreLookingFor = menu.PlacementTarget as Button;
                var itemToAdd = droidsYouAreLookingFor.DataContext;
                FavoriteList listToAddTo = item.DataContext as FavoriteList;
                if (itemToAdd is IList<ShoppingItem>)
                {
                    IList<ShoppingItem> shoppingItems = itemToAdd as IList<ShoppingItem>; 
                    if (listToAddTo == null)
                    {
                        openDialog(shoppingItems);
                    }
                    else
                    {                        
                        listToAddTo.Add(shoppingItems);
                    }
                }
            }
            mainWindow.screenFlash();
        }

        private MenuItem findMenuItemByList(FavoriteList list)
        {
            MenuItem menuItem = null;
            foreach (MenuItem item in ShoppingListContextMenu.Items)
            {
                if (list == item.DataContext)
                {
                    menuItem = item;
                    break;
                }
            }
            return menuItem;
        }

        void listHandler_Changed(ShoppingListsChangedEventArgs e)
        {
            if (e.Type == ShoppingListsChangedEventArgs.ListEventType.Add)
            {
                MenuItem item = menuItemFactory(e.List);
                ShoppingListContextMenu.Items.Insert(ShoppingListContextMenu.Items.Count - 2, item);
            }
            else if (e.Type == ShoppingListsChangedEventArgs.ListEventType.Change)
            {
                MenuItem changeIT = findMenuItemByList(e.List);
                if (changeIT != null)
                {
                    changeIT.Header = e.List.Name;
                }
            }
            else if (e.Type == ShoppingListsChangedEventArgs.ListEventType.Remove)
            {
                MenuItem removeIT = findMenuItemByList(e.List);
                if (removeIT != null)
                {
                    ShoppingListContextMenu.Items.Remove(removeIT);
                }
            }
            mainWindow.shoppingListSelectComboBox.ItemsSource = null;
            mainWindow.shoppingListSelectComboBox.ItemsSource = ShoppingListsCollection;
        }
        
        void openDialog(IList<ShoppingItem> items)
        {
            TextBoxPopup tbp = new TextBoxPopup();
            tbp.Owner = mainWindow;
            tbp.ShowDialog();
            if (tbp.DialogResult == true)
            {
                string s = tbp.TextBoxText;
                FavoriteList list = new FavoriteList(s);
                listHandler.Add(list);
                list.Add(items);
            }
        }
    }
}
