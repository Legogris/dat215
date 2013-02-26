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
        public string Name { get; private set; }
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

        public void Add(ShoppingItem item)
        {
            items.Add(item);
            updateStats();
        }

        public void Add(IList<ShoppingItem> items)
        {
            foreach (ShoppingItem si in items)
            {
                Add(si);
            }
        }

        public IList<ShoppingItem> GetItems()
        {
            return items;
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
