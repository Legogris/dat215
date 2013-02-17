using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ProductCategory
    {
        public string Name { get; private set; }
        public string ID { get; private set; }
        public ProductCategory Parent { get; set; }
        public IList<ProductCategory> SubCategories { get; private set; }
        public IList<Product> Products { get; private set; }

        public ProductCategory(string id, string name, ProductCategory parent)
        {
            this.ID = id;
            this.Name = name;
            this.Parent = parent;
            this.SubCategories = new List<ProductCategory>();
            this.Products = new List<Product>();
        }
    }
}
