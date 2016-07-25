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
            if ("Windows.Mobile" == Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily)
                Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            m_photo = (Photo)e.Parameter;

            this.image.Source = m_photo.PhotoImage;
            this.detail.Text = m_photo.PhotoDescription;

            if (m_photo.PhotoIsSelected)
            {
                checkBox.IsChecked = true;
            }
        }


        private Photo m_photo;

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            m_photo.PhotoIsSelected = true;
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            m_photo.PhotoIsSelected = false;
        }

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MapPage),m_photo);
            e.Handled = true;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Camera), m_photo);
        }
    }
}
