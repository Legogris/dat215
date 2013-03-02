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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace UI.Desktop.Content.Controls
{
    /// <summary>
    /// Interaction logic for SpecialOfferCarousel.xaml
    /// </summary>
    public partial class SpecialOfferCarousel : UserControl
    {
        private Storyboard storyboard = new Storyboard();
        private static TimeSpan duration = TimeSpan.FromMilliseconds(500);
        private DoubleAnimation fadeInAnimation = new DoubleAnimation() { From = 0.0, To = 1.0, Duration = new Duration(duration) };
        private DoubleAnimation fadeOutAnimation = new DoubleAnimation() { From = 1.0, To = 0.0, Duration = new Duration(duration) };

        public SpecialOfferCarousel()
        {
            InitializeComponent();
            image_two.Opacity = 0;
            DispatcherTimer dispatcherTimer = new DispatcherTimer();

            dispatcherTimer.Tick += new EventHandler(timer_tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
        }

        void timer_tick(object sender, EventArgs e)
        {
            triggerSwitch();
        }

        private void leftButton_Click(object sender, RoutedEventArgs e)
        {
            triggerSwitch();
        }

        private void triggerSwitch()
        {
            if (image_one.Opacity == 1)
            {
                switchActive(image_one, image_two);
            }
            else
            {
                switchActive(image_two, image_one);
            }
        }

        private void switchActive(Image outImg, Image inImg)
        {
            fadeOut(outImg);
            fadeIn(inImg);
        }

        private void fadeOut(Image image)
        {
            Storyboard.SetTargetName(fadeOutAnimation, image.Name);
            Storyboard.SetTargetProperty(fadeOutAnimation, new PropertyPath("Opacity", 0));
            storyboard.Children.Add(fadeOutAnimation);
            storyboard.Begin(image);
        }

        private void fadeIn(Image image)
        {
            Storyboard.SetTargetName(fadeInAnimation, image.Name);
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath("Opacity", 1));
            storyboard.Children.Add(fadeInAnimation);
            storyboard.Begin(image);
        }
    }
}
