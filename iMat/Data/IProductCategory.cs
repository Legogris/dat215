using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface ProductCategory 
    {
        string Name { get; }
        string ID { get; }
        IEnumerable<ShoppingItem> GetItems();
        ProductCategory Parent { get; set; }
        IList<ProductCategory> SubCategories { get; }

        ProductCategory GetCategoryByID(string id);
    }
}
