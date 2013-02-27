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
    /// Interaction logic for ItemSelector.xaml
    /// </summary>
    public partial class ItemSelector : AbstractSelector
    {
        protected override Label PriceLabel
        { get { return priceLabel; } }

        protected override Label JmfLabel
        { get { return jmfLabel; } }

        protected override Label PieceLabel
        { get { return pieceLabel; } }

        protected override Spinner AmountSpinner
        { get { return amountSpinner; } }

        public ItemSelector(ShoppingItem item) : base(item)
        {
            InitializeComponent();
            base.init();
        }
    }
}
