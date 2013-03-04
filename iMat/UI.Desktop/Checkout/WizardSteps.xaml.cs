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

namespace UI.Desktop.Checkout
{
    /// <summary>
    /// Interaction logic for WizardSteps.xaml
    /// </summary>
    public partial class WizardSteps : UserControl
    {
        public String Title {
            get { return title.Content.ToString(); }
            set { title.Content = value; }
        }

        public String ImgSource { get; set; }

        private bool stepactive;
        public bool StepActive {
            get { return stepactive; }
            set { stepactive = value; updateImage(); }
        }

        public WizardSteps()
        {
            InitializeComponent();
        }

        private void updateImage()
        {
            image.Source = (ImageSource) App.Current.Resources[ImgSource];
        }
    }
}
