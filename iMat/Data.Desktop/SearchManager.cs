using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Desktop
{
    public class SearchManager
    {
        public static ProductCategory SearchForProducts(string term, ProductCategory rootCategory)
        {
            List<Product> products = new List<Product>();
            products = (List<Product>)searchForProducts(term, rootCategory, new List<Product>());
            ConcreteProductCategory pc = new ConcreteProductCategory("search", "Sökresultat", rootCategory);
            foreach (Product p in products)
            {
                pc.AddProduct(p);
            }
            return pc;
        }

        private static IEnumerable<Product> searchForProducts(string term, ProductCategory rootCategory, List<Product> result)
        {
            IEnumerable<ShoppingItem> items = rootCategory.GetItems();
            foreach (ShoppingItem si in items)
            {
                Product p = si.Product;
                if (p.Name.ToLower().Contains(term.ToLower()) && !result.Contains(p))
                {
                    result.Add(p);
                }
            }
            foreach(ProductCategory pc in rootCategory.SubCategories) {
                searchForProducts(term, pc, result);
            }
            return result;
        }
    }
}
