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
        //private readonly SolidColorBrush evenColor = new SolidColorBrush(Color.FromRgb(220, 255, 190));
        //private readonly SolidColorBrush oddColor = new SolidColorBrush(Color.FromRgb(250, 255, 250));
        private readonly SolidColorBrush evenColor = new SolidColorBrush(Color.FromRgb(121, 158, 90));
        private readonly SolidColorBrush oddColor = new SolidColorBrush(Color.FromRgb(149, 191, 107));

        Dictionary<ShoppingItem, ListViewItem> listItems = new Dictionary<ShoppingItem, ListViewItem>();
        Dictionary<ShoppingItem, DetailedItem> detailedItems = new Dictionary<ShoppingItem, DetailedItem>();

        public ListView()
        {
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
                            if (listItems.ContainsKey(item))
                            {
                                li = listItems[item];
                            } else {
                                li = new ListViewItem(item);
                                listItems[item] = li;
                                li.ItemAdded += li_ItemAdded;
                                li.MouseDown += li_MouseDown;
                            }
                            if (detailedItems.ContainsKey(item))
                            {
                                dli = detailedItems[item];
                            } else {
                                dli = new DetailedItem(item);
                                detailedItems[item] = dli;
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
            DetailedItem dli = detailedItems[li.Item];
            li.Visibility = System.Windows.Visibility.Collapsed;
            dli.Visibility = System.Windows.Visibility.Visible;
        }
        
        void dli_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DetailedItem dli = (DetailedItem)sender;
            ListViewItem li = listItems[dli.Item];

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
