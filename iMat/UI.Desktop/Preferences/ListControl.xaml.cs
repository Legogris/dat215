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
using UI.Desktop.Preferences.Content;

namespace UI.Desktop.Content
{
    /// <summary>
    /// Interaction logic for ListControl.xaml
    /// </summary>
    public partial class ListControl : UserControl
    {
        private ShoppingListHandler listHandler;
        public IList<FavoriteList> ShoppingListsCollection { get { return listHandler.GetFavLists(); } }

        public ListControl(DataHandler dh)
        {
            listHandler = dh.GetFavorites();
            InitializeComponent();
            setUpOverview();
            setUpDetail();
        }

        private void setUpOverview()
        {
            overviewHeader.MakeHeader();
            overviewHeader.First("Namn");
            overviewHeader.Second("Antal");
            overviewHeader.Third("Pris");
            foreach (FavoriteList fav in ShoppingListsCollection)
            {
                addFavoriteListToOverview(fav);
            }
        }

        private void addFavoriteListToOverview(FavoriteList fav) {
            AbstractListItem shoppingListItem = new AbstractListItem();
            overviewPanel.Children.Add(shoppingListItem);
            shoppingListItem.First(fav.Name);
            shoppingListItem.Second(fav.NumberOfItems + " st");
            shoppingListItem.Third(fav.TotalCost + " kr");
            shoppingListItem.Cursor = Cursors.Hand;
            shoppingListItem.MouseUp += overviewItem_Click;
            shoppingListItem.DataContext = fav;
        }
        
        void overviewItem_Click(object sender, MouseButtonEventArgs e)
        {
            AbstractListItem item = sender as AbstractListItem;
            if (item.Selected) return;
            deselectOverview();
            item.Selected = true;
            showDetails(item);
        }

        private void setUpDetail()
        {
            detailHeader.MakeHeader();
            detailHeader.First("Vara");
            detailHeader.Second("Antal");
            detailHeader.Third("Pris");
        }

        private void showDetails(AbstractListItem listItem)
        {
            FavoriteList list = listItem.DataContext as FavoriteList;
            listNameTextBox.Text = list.Name;
            listTotalPriceLabel.Content = list.TotalCost + " kr";
            
            foreach (ShoppingItem item in list.GetItems())
            {
                addShoppingItemToDetail(item);
            }
        }

        private void addShoppingItemToDetail(ShoppingItem item)
        {
            AbstractListItem listItem = new AbstractListItem();
            detailPanel.Children.Add(listItem);
            listItem.First(item.Product.Name);
            listItem.Second(item.Amount + " " + item.Product.UnitSuffix);
            listItem.Third(item.Total + " kr");
            listItem.MakeRemovable();
        }

        private void hideDetails()
        {
            listNameTextBox.Text = "";
            listTotalPriceLabel.Content = "";
        }

        private AbstractListItem getSelectedOverview()
        {
            AbstractListItem listItem = null;
            foreach (AbstractListItem item in overviewPanel.Children)
            {
                if (item.Selected)
                {
                    listItem = item;
                    break;
                }
            }
            return listItem;
        }
                
        private void copyClick(object sender, RoutedEventArgs e) // TODO: referenser till fel objekt. fakken hell.
        {
            
        }

        private void newShoppingList(object sender, RoutedEventArgs e)
        {
            FavoriteList list = new FavoriteList("Ny lista");
            listHandler.Add(list);
            deselectOverview();
            addFavoriteListToOverview(list);
            AbstractListItem item = overviewPanel.Children[overviewPanel.Children.Count - 1] as AbstractListItem;
            item.Selected = true;
            showDetails(item);
        }

        private void deleteShoppingListClick(object sender, RoutedEventArgs e)
        {
            AbstractListItem item = getSelectedOverview();
            if (item == null) return;
            overviewPanel.Children.Remove(item);
            listHandler.Remove(item.DataContext as FavoriteList);
            hideDetails();
        }

        private void deselectOverview()
        {
            foreach (AbstractListItem item in overviewPanel.Children)
            {
                item.Selected = false;
            }
        }
    }
}