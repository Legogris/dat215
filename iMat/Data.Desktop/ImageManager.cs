using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Data.Desktop
{
    public static class ImageManager
    {
        public static ImageSource GetImageForProduct(Product product)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri("pack://application:,,,/UI.Desktop;component/Resources/ProductImages/" + product.ImageName);
            bmp.EndInit();
            return bmp;
        }
    }
}
