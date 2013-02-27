using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI.Desktop
{
    /// <summary>
    /// Interaction logic for GridViewItem.xaml
    /// </summary>
    public partial class GridViewItem : ProductView
    {
        private AbstractSelector sel;

        override protected double NumberOfItems
        {
            get
            {
                return Item.Amount;
            }
        }

        public GridViewItem(ShoppingItem item) : base(item)
        {
            InitializeComponent();
            initContent();
        }

        private void initContent() {
            productName.Content = Product.Name;
            sel = Product.BoughtInBulk ? (AbstractSelector)new BulkSelector(Item) : new ItemSelector(Item);
            sel.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            sel.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            selectorContainer.Children.Add(sel);
        }

    }
}
