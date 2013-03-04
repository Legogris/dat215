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

        public bool IsFairTrade { get { return GetHashCode() % 4 == 0; } }
        public bool IsKRAV { get { return GetHashCode() % 3 == 0; } }

        private static readonly string[] descriptions = {
            "Köp vår{0} smarriga {1} - vi lovar att du inte kommer att bli besviken!\r\n",
            "Varje dag som går utan att du provat vår{0} {1} är en dag som inte är värd att leva - nystarta ditt liv idag!\r\n",
            "Vi vet att du vill ha {2} - stå inte utan {0} när det gäller!\r\n",
            "Köp, köp, köp! Köp {1}! Vad väntar du på? Bunkra upp med {1} innan det är för sent!\r\n"
        };

        public string Description
        {
            get
            {
                string name = (IsKRAV ? "ekologiska " + Name : Name).ToLower();
                string desc = descriptions[GetHashCode() % descriptions.Length];
                if (IsFairTrade) {
                    desc += GetHashCode() % 2 == 0 ? "Rättvisemärkt produktion så du kan sova gott om natten.\r\n" : ("Att de" + (BoughtInBulk ? "" : "n") + " är Fairtrademärkt" + (BoughtInBulk ? "a" : "") + " gör att du vet att du kan lita på ursprung och kvalitet.\r\n" );
                }
                return string.Format(desc,
                    BoughtInBulk ? "a": "",
                    name,
                    BoughtInBulk ? "dem" : "den");
            }
        }

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
