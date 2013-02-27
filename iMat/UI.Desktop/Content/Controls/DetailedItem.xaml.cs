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
using Data;
using Data.Desktop;

namespace UI.Desktop
{
    /// <summary>
    /// Interaction logic for ListViewItem.xaml
    /// </summary>
    public partial class DetailedItem : ProductView
    {
        private AbstractSelector sel;
        
        override protected double NumberOfItems
        {
            get
            {
                return sel.NumberOfItems;
            }
        }

        public DetailedItem(ShoppingItem i) :  base(i)
        {
            InitializeComponent();
            addToListButton.ContextMenu = MainWindow.ListContextMenu;
            initContent();
        }

        private void initContent() {
            productName.Content = Product.Name;
            sel = Product.BoughtInBulk ? (AbstractSelector)new BulkSelector(Item) : new ItemSelector(Item);
            sel.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            sel.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            selectorContainer.Children.Add(sel);
            productImage.Source = ImageManager.GetImageForProduct(Product);
        }

        private void addToListButton_Click(object sender, RoutedEventArgs e)
        {
            addToListButton.ContextMenu.IsOpen = true;
            addToListButton.ContextMenu.PlacementTarget = addToListButton;
            IList<ShoppingItem> addIT = new List<ShoppingItem>();
            addIT.Add(new ShoppingItem(Product, NumberOfItems));
            addToListButton.DataContext = addIT;
            e.Handled = true;
        }
    }
}