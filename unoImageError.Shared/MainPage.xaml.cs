using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace unoImageError
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ImageSource image
        {
            get { return (ImageSource)GetValue(imageProperty); }
            set { SetValue(imageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty imageProperty =
            DependencyProperty.Register("image", typeof(ImageSource), typeof(MainPage), new PropertyMetadata(null));

        public MainPage()
        {
            this.InitializeComponent();

            this.Loaded += async (sender, e) =>
            {
                this.image = await ImageHelper.GenerateSource();
                //Fallback to previous saved image
                if (this.image is null)
                {
                    string imagePath = Path.GetFullPath(Path.Combine("cache", "image.png"));
                    var imgSrc = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
                    imgSrc.UriSource = new Uri($"file://{imagePath}");
                    this.image = imgSrc;
                }
            };
        }






    }
}
