using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [Serializable()]
    public class ShoppingCart : IShoppingCartNotifier
    {
        public event ShoppingCartChangedHandler Changed;

        private IList<ShoppingItem> items;

        public ShoppingCart()
        {
            items = new List<ShoppingItem>();
        }

        public double Total
        {
            get
            {
                return items.Sum(x => x.Total);
            }
        }

        public IList<ShoppingItem> GetItems()
        {
            return items.ToList();
        }

        public void Add(ShoppingItem item)
        {
            foreach (ShoppingItem si in items)
            {
                if (si.Product == item.Product) {
                    ChangePlaceHolder(si, item.Amount);
                    return;
                }
            }
            items.Add(item);
            if (Changed != null)
            {
                Changed(this, new CartEventArgs(CartEventArgs.CartEventType.Add, item));
            }
        }

        public void Add(Product product)
        {
            Add(product, 1);
        }

        public void Add(Product product, double amount)
        {
            Add(new ShoppingItem(product, amount));
        }

        public void Clear()
        {
            items.Clear();
            if (Changed != null)
            {
                Changed(this, new CartEventArgs(CartEventArgs.CartEventType.Clear, null));
            }
        }

        public void ChangePlaceHolder(ShoppingItem shoppingItem, double amount)
        {
            shoppingItem.Amount += amount;
            Changed(this, new CartEventArgs(CartEventArgs.CartEventType.Change, shoppingItem));
        }

        public bool Remove(ShoppingItem item)
        {
            bool retVal = items.Remove(item);
            if (Changed != null)
            {
                Changed(this, new CartEventArgs(CartEventArgs.CartEventType.Remove, item));
            }
            return retVal;
        }

        public void RemoveAt(int index)
        {
            ShoppingItem item = items[index];
            Remove(item);
        }

    }
}
