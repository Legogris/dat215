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
    /// Interaction logic for ListView.xaml
    /// </summary>
    public partial class ListView : UserControl
    {
        Dictionary<ShoppingItem, ListViewItem> listItems = new Dictionary<ShoppingItem, ListViewItem>();
        Dictionary<ShoppingItem, DetailedItem> detailedItems = new Dictionary<ShoppingItem, DetailedItem>();

        public ListView()
        {
            InitializeComponent();
            this.DataContextChanged += ListView_DataContextChanged;
            MainWindow.ListContextMenu.Closed += ListContextMenu_Closed;
        }

        void ListContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            dli_MouseDown(MainWindow.ListContextMenu.Tag, null);
        }

        void ListView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                stackPanel.Children.Clear();
                ProductCategory pc = e.NewValue as ProductCategory;
                if (pc != null)
                {
                    int i = 0;
                    foreach(ShoppingItem item in pc.GetItems())
                    {
                        try
                        {
                            Product product = item.Product;

                            ListViewItem li;
                            DetailedItem dli;
                            if (listItems.ContainsKey(item))
                            {
                                li = listItems[item];
                            } else {
                                li = new ListViewItem(item);
                                listItems[item] = li;
                                li.ItemAdded += li_ItemAdded;
                                li.MouseEnter += li_MouseDown;
                                li.BorderThickness = new Thickness(0, 0, 0, 1);
                                li.BorderBrush = (Brush)App.Current.Resources["ListBorder"];
                            }
                            if (detailedItems.ContainsKey(item))
                            {
                                dli = detailedItems[item];
                            } else {
                                dli = new DetailedItem(item);
                                detailedItems[item] = dli;
                                dli.ItemAdded += li_ItemAdded;
                                dli.MouseLeave += dli_MouseDown;
                                dli.BorderThickness = new Thickness(0, 0, 0, 1);
                                dli.BorderBrush = (Brush)App.Current.Resources["ListBorder"];
                            }
                            li.Visibility = System.Windows.Visibility.Visible;
                            dli.Visibility = System.Windows.Visibility.Collapsed;
                            li.Background = (Brush)(i % 2 == 0 ? App.Current.Resources["ItemOddBack"] : App.Current.Resources["ItemEvenBack"]);
                            stackPanel.Children.Add(li);
                            stackPanel.Children.Add(dli);
                        }
                        catch {}
                        i++;
                    }
                }
            }
        }

        void li_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewItem li = (ListViewItem)sender;
            DetailedItem dli = detailedItems[li.Item];
            li.Visibility = System.Windows.Visibility.Collapsed;
            dli.Visibility = System.Windows.Visibility.Visible;
        }
        
        void dli_MouseDown(object sender, MouseEventArgs e)
        {
            
            DetailedItem dli = (DetailedItem)sender;
            if (dli != null && !dli.IsListContextMenuOpen)
            {
                ListViewItem li = listItems[dli.Item];

                li.Visibility = System.Windows.Visibility.Visible;
                dli.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        void li_ItemAdded(object sender, CartEventArgs e)
        {
            if (ItemAdded != null)
            {
                ItemAdded.Invoke(sender, e);
            }
        }

        public event ShoppingCartChangedHandler ItemAdded;
    }
}
