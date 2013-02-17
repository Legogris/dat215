using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
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

        public override int GetHashCode()
        {
            return Product.GetHashCode() * 3 + Amount.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            ShoppingItem si = obj as ShoppingItem;
            return Equals(si);
        }

        public bool Equals(ShoppingItem si)
        {
            if ((object)si == null)
            {
                return false;
            }
            return si.Amount == Amount && si.Product == Product;
        }

        public static bool operator ==(ShoppingItem a, ShoppingItem b)
        {
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }
            if ((object)a == null || (object)b == null)
            {
                return false;
            }
            return a.Equals(b);
        }

        public static bool operator !=(ShoppingItem a, ShoppingItem b)
        {
            return !(a == b);
        }
    }
}
