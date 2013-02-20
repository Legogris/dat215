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
    public partial class BulkSelector : UserControl
    {
        public BulkSelector()
        {
            InitializeComponent();
            bulkSpinner.Step = 0.2;
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
        }
    }
}
