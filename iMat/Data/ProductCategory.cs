using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [Serializable()]
    public class ProductCategory
    {
        public string Name { get; private set; }
        public string ID { get; private set; }
        public ProductCategory Parent { get; set; }
        public IList<ProductCategory> SubCategories { get; private set; }
        private IList<Product> products;

        public IList<object> Children
        {
            get
            {
                List<object> children = new List<object>();
                children.AddRange(SubCategories);
                children.AddRange(products);
                return children;
            }
        }

        public ProductCategory(string id, string name, ProductCategory parent)
        {
            this.ID = id;
            this.Name = name;
            this.Parent = parent;
            this.SubCategories = new List<ProductCategory>();
            products = new List<Product>();
            if (parent != null)
            {
                parent.SubCategories.Add(this);
            }
        }

        public void AddProduct(Product p)
        {
            products.Add(p);
            p.Categories.Add(this);
        }

        public Product GetProduct(int id)
        {
            Product p = products.Single(x => x.ProductID == id);
            if (p != null)
            {
                return p;
            }
            foreach (ProductCategory c in SubCategories)
            {
                p = c.GetProduct(id);
                if (p != null)
                {
                    return p;
                }
            }
            return null;
        }

        public IEnumerable<Product> GetProducts()
        {
            List<Product> ps = products.ToList<Product>();
            foreach (ProductCategory subCat in SubCategories)
            {
                ps.AddRange(subCat.GetProducts());
            }
            return ps.Distinct();
        }

        public ProductCategory GetCategoryByID(string id)
        {
            if (id == ID) { return this; }
            foreach (ProductCategory category in SubCategories)
            {
                ProductCategory c = category.GetCategoryByID(id);
                if (c != null)
                {
                    return c;
                }
            }
            return null;
        }
    }
}
