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
using System.Windows.Shapes;
using UI.Desktop.Preferences.Content;

namespace UI.Desktop.Controls
{
    /// <summary>
    /// Interaction logic for DetailShoppingCartView.xaml
    /// </summary>
    public partial class DetailShoppingCartView : Window
    {
        public DetailShoppingCartView()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            head.MakeHeader();
            head.First("Vara");
            head.Second("Antal");
            head.Third("Pris");
        }

        public void setData(IList<ShoppingItem> list)
        {
            foreach (ShoppingItem item in list)
            {
                AbstractListItem listItem = new AbstractListItem();
                content.Children.Add(listItem);
                listItem.First(item.Product.Name);
                string s = "";
                if (item.Amount == 1)
                {
                    s = item.Amount + item.Product.UnitSuffix;
                }
                else
                {
                    s = item.Amount + item.Product.UnitSuffix + " * " + item.Product.Price + item.Product.Unit;
                }                    
                listItem.Second(s);
                listItem.Third(item.Total + " kr");
            }
        }

        public void setOrder(Order o)
        {
            setData(o.Items);
        }
    }
}
