using PhotoMaster.Model;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PhotoMaster
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailView : Page
    {
        public DetailView()
        {
            this.InitializeComponent();

            m_photo = new Photo();

            // Test data
            m_photo.PhotoImage = new BitmapImage(new Uri("ms-appx:/Assets/index_bg.png"));
            m_photo.PhotoDescription = "This image is a test image, XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";

            this.image.Source = m_photo.PhotoImage;
            this.detail.Text = m_photo.PhotoDescription;
        }

        private Photo m_photo;
    }
}
