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
    /// Interaction logic for GridView.xaml
    /// </summary>
    public partial class GridView : UserControl
    {
        Dictionary<ShoppingItem, GridViewItem> gridItems = new Dictionary<ShoppingItem, GridViewItem>();
        Dictionary<ShoppingItem, DetailedItem> detailedItems = new Dictionary<ShoppingItem, DetailedItem>();

        DetailedItem currentDLI;
        public GridView()
        {
            InitializeComponent();
            DataContextChanged += GridView_DataContextChanged;
            this.SizeChanged += GridView_SizeChanged;
        }

        void GridView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (DetailedItem dli in detailedItems.Values)
            {
                dli.Width = stackPanel.ActualWidth;
            }
        }
        void GridView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                stackPanel.Children.Clear();
                ProductCategory pc = e.NewValue as ProductCategory;
                if (pc != null)
                {
                    int i = 0;
                    foreach (ShoppingItem item in pc.GetItems())
                    {
                        Product product = item.Product;
                        GridViewItem li;
                        DetailedItem dli;
                        if(gridItems.ContainsKey(item)) {
                            li = gridItems[item];
                        } else {
                            li = new GridViewItem(item);
                            gridItems[item] = li;
                            li.ItemAdded += li_ItemAdded;
                            li.MouseEnter += li_MouseUp;
                        }
                        if (detailedItems.ContainsKey(item))
                        {
                            dli = detailedItems[item];
                        } else {
                            dli = new DetailedItem(item);
                            detailedItems[item] = dli;
                            dli.ItemAdded += li_ItemAdded;
                            dli.MouseLeave += dli_MouseUp;
                            dli.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                            dli.Width = stackPanel.ActualWidth;
                        }
                        li.Visibility = System.Windows.Visibility.Visible;
                        dli.Visibility = System.Windows.Visibility.Collapsed;
                        li.Background = new SolidColorBrush((Color)(i % 2 == 0 ? App.Current.Resources["ItemOddBack"] : App.Current.Resources["ItemEvenBack"]));
                        stackPanel.Children.Add(li);
                        i++;
                    }
                }
            }
        }

        void li_MouseUp(object sender, MouseEventArgs e)
        {
            if (currentDLI != null)
            {
                dli_MouseUp(currentDLI, null);
            }
            GridViewItem li = (GridViewItem)sender;
            DetailedItem dli = detailedItems[li.Item];
            int gridItemWidth = (int)Math.Max(1, Math.Floor(stackPanel.ActualWidth / li.ActualWidth));
            li.Visibility = System.Windows.Visibility.Collapsed;
            int index = stackPanel.Children.IndexOf(li);
            stackPanel.Children.Insert(index - (index % gridItemWidth), dli);
            dli.Visibility = System.Windows.Visibility.Visible;
            currentDLI = dli;
        }
        
        void dli_MouseUp(object sender, MouseEventArgs e)
        {
            DetailedItem dli = (DetailedItem)sender;
            GridViewItem li = gridItems[dli.Item];

            if (dli.Visibility == System.Windows.Visibility.Visible)
            {
                li.Visibility = System.Windows.Visibility.Visible;
                dli.Visibility = System.Windows.Visibility.Collapsed;
                stackPanel.Children.Remove(dli);
                currentDLI = null;
            }
        }

        void li_ItemAdded(object sender, CartEventArgs e)
        {
            ItemAdded.Invoke(sender, e);
        }

        public event Data.ShoppingCartChangedHandler ItemAdded;

    }
}
