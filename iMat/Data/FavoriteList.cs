﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [Serializable()]
    public class FavoriteList 
    {
        private readonly List<ShoppingItem> items;
        public string Name { get; private set; }
        public int NumberOfItems { get; private set; }
        public double TotalCost { get; private set; }

        public FavoriteList(string name)
        {
            items = new List<ShoppingItem>();
            Name = name;
            NumberOfItems = 1337;
            TotalCost = 42;
        }
        public void Add(ShoppingItem item)
        {
            items.Add(item);
        }
        public void Remove(ShoppingItem item)
        {
            items.Remove(item);
        }
        public IEnumerator<ShoppingItem> GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}
