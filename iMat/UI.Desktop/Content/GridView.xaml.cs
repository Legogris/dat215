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
    /// Interaction logic for GridView.xaml
    /// </summary>
    public partial class GridView : UserControl
    {
        Dictionary<Product, GridViewItem> gridItems = new Dictionary<Product, GridViewItem>();
        public GridView()
        {
            InitializeComponent();
            DataContextChanged += GridView_DataContextChanged;
        }
        void GridView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                stackPanel.Children.Clear();
                ProductCategory pc = e.NewValue as ProductCategory;
                if (pc != null)
                {
                    foreach (Product product in pc.GetProducts())
                    {
                        GridViewItem li;
                        if(gridItems.ContainsKey(product)) {
                            li = gridItems[product];
                        } else {
                            li = new GridViewItem(product);
                            gridItems[product] = li;
                            li.ItemAdded += li_ItemAdded;
                        }
                        stackPanel.Children.Add(li);
                    }
                }
            }
        }

        void li_ItemAdded(object sender, CartEventArgs e)
        {
            ItemAdded.Invoke(sender, e);
        }

        public event Data.ShoppingCartChangedHandler ItemAdded;

    }
}
