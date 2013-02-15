using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class ShoppingItem
    {
        public double Amount { get; private set; }
        public Product Product { get; private set; }
        public double Total { get { return Product.Price * Amount; } }

        public ShoppingItem(Product product, double amount)
        {
            Product = product;
            Amount = amount;
        }
    }
}
