using System;
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
        public string Name { get; set; }
        public int NumberOfItems { get; private set; }
        public double TotalCost { get; private set; }

        public FavoriteList(string name)
        {
            items = new List<ShoppingItem>();
            Name = name;
            updateStats();
        }

        private void updateStats()
        {
            NumberOfItems = items.Count;
            TotalCost = items.Sum(x => x.Total);
        }

        private void Add(ShoppingItem item)
        {
            foreach (ShoppingItem si in items)
            {
                if (si.Product == item.Product)
                {
                    si.Amount += item.Amount;
                    return;
                }
            }
            items.Add(item);
        }

        public void Add(IList<ShoppingItem> items)
        {
            foreach (ShoppingItem si in items)
            {
                Add(si);
            }
            updateStats();
        }

        public void Change(int index, double newValue)
        {
            if (index == -1 || index > items.Count) return;
            items[index].Amount = newValue;
            updateStats();
        }

        public IList<ShoppingItem> GetItems()
        {
            return items;
        }

        public void Remove(ShoppingItem item)
        {
            items.Remove(item);
            updateStats();
        }

        public void Remove(int i)
        {
            items.RemoveAt(i);
            updateStats();
        }

        public IEnumerator<ShoppingItem> GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}
