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
    /// Interaction logic for BulkSelector.xaml
    /// </summary>
    public partial class BulkSelector : AbstractSelector
    {
        protected override Label PriceLabel
        { get { return priceLabel; } }

        protected override Label PieceLabel
        { get { return pieceLabel; } }

        protected override Label JmfLabel
        { get { return jmfLabel; } }

        protected override Spinner AmountSpinner
        { get { return amountSpinner; } }
        
        public BulkSelector(Product p) : base(p)
        {
            InitializeComponent();
            bulkSpinner.Step = 0.2;
            base.init();
            bulkSpinner.ValueChanged += spinner_AmountChanged;
        }

        private void checkedChangeAmount(object sender, RoutedEventArgs e)
        {
            toggleSpinners(true);
        }
        
        private void checkedChangeBulk(object sender, RoutedEventArgs e)
        {
            toggleSpinners(false);
        }

        private void toggleSpinners(bool useAmount)
        {
            amountSpinner.Enabled = useAmount;
            bulkSpinner.Enabled = !useAmount;
            if (useAmount)
            {
                spinner_AmountChanged(amountSpinner);
            }
            else {
                spinner_AmountChanged(bulkSpinner);
            }
        }
    }
}
