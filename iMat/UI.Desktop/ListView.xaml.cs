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
    /// Interaction logic for ListView.xaml
    /// </summary>
    public partial class ListView : UserControl
    {
        public ListView()
        {
            InitializeComponent();
            this.DataContextChanged += ListView_DataContextChanged;
        }

        void ListView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ProductCategory pc = e.NewValue as ProductCategory;
                if (pc != null)
                {
                    foreach (Product product in pc.GetProducts())
                    {
                        stackPanel.Children.Add(new ListViewItem(product));
                    }
                }
            }
        }

        private void categorySourceUpdated()
        {
        }
    }
}
