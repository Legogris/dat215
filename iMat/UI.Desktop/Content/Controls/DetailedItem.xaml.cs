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
using Data;
using Data.Desktop;

namespace UI.Desktop
{
    /// <summary>
    /// Interaction logic for ListViewItem.xaml
    /// </summary>
    public partial class DetailedItem : ProductView
    {
        private AbstractSelector sel;
        
        override protected double NumberOfItems
        {
            get
            {
                return sel.NumberOfItems;
            }
        }

        public DetailedItem(Product p) :  base(p)
        {
            InitializeComponent();
            selectorContainer.MouseUp += bubbleClick;
            addToListButton.ContextMenu = MainWindow.ListContextMenu;
            initContent();
        }

        void bubbleClick(object sender, MouseButtonEventArgs e)
        {
        }

        private void initContent() {
            productName.Content = Product.Name;
            if (Product.UnitSuffix == "kg")
            {
                sel = new BulkSelector(Product);
            }
            else {
                sel = new ItemSelector(Product);
            }
            sel.MouseUp += bubbleClick;
            sel.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            sel.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            selectorContainer.Children.Add(sel);
            productImage.Source = ImageManager.GetImageForProduct(Product);
        }

        private void addToListButton_Click(object sender, RoutedEventArgs e)
        {
            addToListButton.ContextMenu.IsOpen = true;
            e.Handled = true;
        }
    }
}