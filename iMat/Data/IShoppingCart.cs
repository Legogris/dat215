using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IShoppingCartNotifier
    {
        event ShoppingCartChangedHandler Changed;
    }

    public delegate void ShoppingCartChangedHandler(object sender, CartEventArgs e);
    public class CartEventArgs {
        public enum CartEventType
        {
            Add,
            Remove,
            Change,
            Clear
        }
        public CartEventType EventType { get; private set; }
        public ShoppingCart ShoppingCart { get; private set; }
        public ShoppingItem ShoppingItem { get; private set; }
                
        public CartEventArgs(CartEventType eventType, ShoppingItem shoppingItem)
        {
            EventType = eventType;
            ShoppingItem = shoppingItem;
        }

        public CartEventArgs(CartEventType eventType, ShoppingItem shoppingItem, ShoppingCart shoppingCart) : this(eventType, shoppingItem)
        {
            ShoppingCart = shoppingCart;
        }
    }
}
