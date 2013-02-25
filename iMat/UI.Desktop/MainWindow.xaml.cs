using Data;
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
        private ContextMenu shoppingListContextMenu;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex) {
            }
            dataHandler = DataHandler.ReadFromFile(DATABASE, PRODUCTS);
            makeContext();
            initDataBinding();
        }

        private void makeContext()
        {
            shoppingListContextMenu = new ContextMenu();
            foreach (FavoriteList list in dataHandler.GetFavorites()) {
                MenuItem item = new MenuItem();
                item.Header = list.Name;
                //item.Click += new RoutedEventHandler(item_Click);
                shoppingListContextMenu.Items.Add(item);
            }
            MenuItem addNew = new MenuItem();
            addNew.Header = "ny för fan";
            shoppingListContextMenu.Items.Add(addNew);
            addNew.Click += addNewShoppingList_Click;
        }

        void addNewShoppingList_Click(object sender, RoutedEventArgs e)
        {
            TextBoxPopup tbp = new TextBoxPopup();
            tbp.Owner = this;
            
            // Open the dialog box modally 
            tbp.ShowDialog();
            if (tbp.DialogResult == true)
            {
                string s = tbp.TextBoxText;
                dataHandler.GetFavorites().Add(new FavoriteList(s));
            }
        }

        private void initDataBinding() {
            productBrowser.RootCategory = dataHandler.GetRootCategory();
            productBrowser.ItemAdded += productBrowser_ItemAdded;
            dataHandler.GetCart().Changed += shoppingCart_ItemAdded;
            shoppingCart.ShoppingCart = dataHandler.GetCart();
            shoppingCart.SetListContextMenu(shoppingListContextMenu);
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

        private void shoppingCart_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dataHandler.WriteToFile(DATABASE);
        }
    }
}
