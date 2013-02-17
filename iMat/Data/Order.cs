using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [Serializable()]
    public class Order
    {
        public DateTime Date { get; private set; }
        public int OrderNumber { get; private set; }
        public IList<ShoppingItem> Items { get; private set; }

        public Order(ShoppingCart cart, DateTime date, int orderNumber)
        {
            Date = date;
            OrderNumber = orderNumber;
            Items = cart.GetItems();
        }
    }
}
