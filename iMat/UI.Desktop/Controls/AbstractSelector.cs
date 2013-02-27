using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UI.Desktop
{
    public abstract class AbstractSelector : UserControl
    {
        public static readonly string CURRENCY = " kr";
        protected Product product;

        protected abstract Label PriceLabel { get; }
        protected abstract Label PieceLabel { get; }
        protected abstract Label JmfLabel { get; }
        protected abstract Spinner AmountSpinner { get; }
        private double numberOfItems;
        public double NumberOfItems
        {
            get { return numberOfItems; }
            protected set { numberOfItems = value;
            if (AmountSpinner != null) { AmountSpinner.Value = value; }
            }
        }

        public AbstractSelector(ShoppingItem item) {
            product = item.Product;
            NumberOfItems = item.Amount;
        }

        protected void init() {
            updatePrice(1);
            updateJmf();
            AmountSpinner.Step = 1;
            AmountSpinner.Value = NumberOfItems;
            AmountSpinner.ValueChanged += spinner_AmountChanged;
        }

        protected void updatePrice(double multiplier)
        {
            PriceLabel.Content = (multiplier*product.Price) + CURRENCY;
        }

        private void updateJmf() {
            JmfLabel.Content = product.ComparePrice + CURRENCY + "/" + product.BulkUnit;
            PieceLabel.Content = product.Price + CURRENCY + "/" + product.PieceUnit;
        }

        protected void spinner_AmountChanged(Spinner spinner) {
            if (spinner.Value > 0)
            {
                updatePrice(spinner.Value);
                NumberOfItems = spinner.Value;
            }
        }
    }
}
