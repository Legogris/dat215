using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Product
    {
        public ISet<ProductCategory> Categories { get; private set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int ProductID { get; set; }
        public int Unit { get; set; }
        public int UnitSuffix { get; set; }

        public Product()
        {
            Categories = new HashSet<ProductCategory>();
        }
    }
}
