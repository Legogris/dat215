using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [Serializable()]
    public class Product
    {
        public ISet<ProductCategory> Categories { get; private set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int ProductID { get; set; }
        public string Unit { get; set; }
        public string UnitSuffix { get; set; }

        public bool BoughtInBulk { get { return UnitSuffix == "kg"; } }
        public double ComparePrice { get { return Price * (BoughtInBulk ? 1 : 3); } } //Fake the compare price just to get a diff for items not bought in bulk :P
        public string BulkUnit { get { return BoughtInBulk ? UnitSuffix : "l"; } }
        public string PieceUnit { get { return BoughtInBulk ? "st" : UnitSuffix; } }

        public Product()
        {
            Categories = new HashSet<ProductCategory>();
        }

        public override int GetHashCode()
        {
            return ProductID.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Product si = obj as Product;
            return Equals(si);
        }

        public bool Equals(Product si)
        {
            if ((object)si == null)
            {
                return false;
            }
            return si.ProductID == ProductID;
        }

        public static bool operator ==(Product a, Product b)
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

        public static bool operator !=(Product a, Product b)
        {
            return !(a == b);
        }
    }
}
