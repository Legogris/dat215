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

        protected abstract Label PriceLabel
        {
            get;
        }

        protected abstract Label JmfLabel
        {
            get;
        }

        public AbstractSelector() {
            
        }

        protected void init() {
            updatePrice();
            updateJmf();
        }

        protected void updatePrice()
        {
            PriceLabel.Content = "100,00 kr";
        }

        private void updateJmf() {
            JmfLabel.Content = "1000,00 kr/frp";
        }
    }
}
