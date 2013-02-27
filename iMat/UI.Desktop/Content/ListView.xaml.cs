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
        //private LinearGradientBrush evenColor = new LinearGradientBrush();
        //private LinearGradientBrush oddColor = new LinearGradientBrush();
        private readonly SolidColorBrush evenColor = new SolidColorBrush(Color.FromRgb(220, 255, 190));
        private readonly SolidColorBrush oddColor = new SolidColorBrush(Color.FromRgb(250, 255, 250));

        Dictionary<Product, ListViewItem> listItems = new Dictionary<Product, ListViewItem>();
        Dictionary<Product, DetailedItem> detailedItems = new Dictionary<Product, DetailedItem>();

        public ListView()
        {
            //evenColor.StartPoint = new Point(0, 0);
            //evenColor.EndPoint = new Point(0, 1);
            //evenColor.GradientStops.Add(new GradientStop(Color.FromRgb(206, 253, 138), 0));
            //evenColor.GradientStops.Add(new GradientStop(Color.FromRgb(151, 255, 0), 1));
            //oddColor.StartPoint = new Point(0, 0);
            //oddColor.EndPoint = new Point(0, 1);
            //oddColor.GradientStops.Add(new GradientStop(Color.FromRgb(151, 255, 0), 0));
            //oddColor.GradientStops.Add(new GradientStop(Color.FromRgb(206, 253, 138), 1));
            InitializeComponent();
            this.DataContextChanged += ListView_DataContextChanged;
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
                            if (listItems.ContainsKey(product))
                            {
                                li = listItems[product];
                            } else {
                                li = new ListViewItem(item);
                                listItems[product] = li;
                                li.ItemAdded += li_ItemAdded;
                                li.MouseDown += li_MouseDown;
                            }
                            if (detailedItems.ContainsKey(product))
                            {
                                dli = detailedItems[product];
                            } else {
                                dli = new DetailedItem(item);
                                detailedItems[product] = dli;
                                dli.ItemAdded += li_ItemAdded;
                                dli.MouseDown += dli_MouseDown;
                            }
                            li.Visibility = System.Windows.Visibility.Visible;
                            dli.Visibility = System.Windows.Visibility.Collapsed;
                            li.Background = i % 2 == 0 ? oddColor : evenColor;
                            stackPanel.Children.Add(li);
                            stackPanel.Children.Add(dli);
                        }
                        catch {}
                        i++;
                    }
                }
            }
        }

        void li_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem li = (ListViewItem)sender;
            DetailedItem dli = detailedItems[li.Product];
            li.Visibility = System.Windows.Visibility.Collapsed;
            dli.Visibility = System.Windows.Visibility.Visible;
        }
        
        void dli_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DetailedItem dli = (DetailedItem)sender;
            ListViewItem li = listItems[dli.Product];

            li.Visibility = System.Windows.Visibility.Visible;
            dli.Visibility = System.Windows.Visibility.Collapsed;
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
