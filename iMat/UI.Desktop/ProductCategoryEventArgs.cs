using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Desktop
{
    public class ProductCategoryEventArgs : EventArgs
    {
        public ProductCategory ProductCategory { get; private set; }

        public ProductCategoryEventArgs(ProductCategory pc)
            : base()
        {
            ProductCategory = pc;
        }

    }
}
