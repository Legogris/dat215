using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [Serializable()]
    public class FavoriteList : ProductCategory
    {
        private readonly List<ShoppingItem> items;
        public string Name { get; set; }
        public string ID { get; set; }
        public int NumberOfItems { get; private set; }
        public double TotalCost { get; private set; }
        public ProductCategory Parent { get; set; }
        public IList<ProductCategory> SubCategories { get { return new List<ProductCategory>(); } }

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

        public ProductCategory GetCategoryByID(string id)
        {
            return ID == id ? this : null;
        }

        public void Add(ShoppingItem item)
        {
            items.Add(item);
            updateStats();
        }

        public void Add(IEnumerable<ShoppingItem> items)
        {
            foreach (ShoppingItem si in items)
            {
                Add(si);
            }
        }

        public void Change(int index, double newValue)
        {
            if (index == -1 || index > items.Count) return;
            items[index].Amount = newValue;
            updateStats();
        }

        public IEnumerable<ShoppingItem> GetItems()
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
