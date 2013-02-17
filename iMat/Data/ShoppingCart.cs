using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [Serializable()]
    public class ShoppingCart
    {
        public class CartEventArgs {
            public enum CartEventType
            {
                Add,
                Remove,
                Change,
                Clear
            }
            public CartEventType EventType { get; private set; }
            public ShoppingItem ShoppingItem { get; private set; }

            public CartEventArgs(CartEventType eventType, ShoppingItem shoppingItem)
            {
                EventType = eventType;
                ShoppingItem = shoppingItem;
            }
        }

        public delegate void ShoppingCartChangedHandler(ShoppingCart sender, CartEventArgs e);

        private IList<ShoppingItem> items;

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
            items.Add(item);
            Changed(this, new CartEventArgs(CartEventArgs.CartEventType.Add, item));
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
            Changed(this, new CartEventArgs(CartEventArgs.CartEventType.Clear, null));
        }

        public bool Remove(ShoppingItem item)
        {
            bool retVal = items.Remove(item);
            Changed(this, new CartEventArgs(CartEventArgs.CartEventType.Remove, item));
            return retVal;
        }

        public void RemoveAt(int index)
        {
            ShoppingItem item = items[index];
            Remove(item);
        }

        public event ShoppingCartChangedHandler Changed;

    }
}
