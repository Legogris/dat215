﻿using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UI.Desktop
{
    public abstract class ProductView : UserControl
    {
        public Product Product { get { return Item.Product; } }
        protected ShoppingItem Item { get; private set; }

        public ProductView(ShoppingItem item)
        {
            Item = item;
        }

        public event Data.ShoppingCartChangedHandler ItemAdded;
        protected abstract double NumberOfItems {get; }

        protected void bubbleItemAddedEvent(object sender, RoutedEventArgs e)
        {
            if (ItemAdded != null)
            {
                ItemAdded.Invoke(this, new Data.CartEventArgs(Data.CartEventArgs.CartEventType.Add, new Data.ShoppingItem(Product, NumberOfItems)));
            }
        }
    }
}
