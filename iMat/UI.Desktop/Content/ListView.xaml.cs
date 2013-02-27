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

namespace UI.Desktop
{
    /// <summary>
    /// Interaction logic for ListView.xaml
    /// </summary>
    public partial class ListView : UserControl
    {
        private readonly SolidColorBrush evenColor = new SolidColorBrush(Color.FromRgb(250, 255, 250));
        private readonly SolidColorBrush oddColor = new SolidColorBrush(Color.FromRgb(220, 255, 190));

        Dictionary<Product, ListViewItem> listItems = new Dictionary<Product, ListViewItem>();
        Dictionary<Product, DetailedItem> detailedItems = new Dictionary<Product, DetailedItem>();

        public ListView()
        {
            InitializeComponent();
            this.DataContextChanged += ListView_DataContextChanged;
            this.MouseUp += ListView_MouseUp;
        }

        void ListView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            object o = e.OriginalSource;

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
                    foreach(Product product in pc.GetProducts())
                    {
                        try
                        {
                            ListViewItem li;
                            DetailedItem dli;
                            if (listItems.ContainsKey(product))
                            {
                                li = listItems[product];
                            } else {
                                li = new ListViewItem(product);
                                listItems[product] = li;
                                li.ItemAdded += li_ItemAdded;
                                li.MouseDown += li_MouseDown;
                            }
                            if (detailedItems.ContainsKey(product))
                            {
                                dli = detailedItems[product];
                            } else {
                                dli = new DetailedItem(product);
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
