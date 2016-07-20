using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Media.Imaging;

namespace PhotoMaster.Model
{
    public class Photo
    {
        public int PhotoId;
        public BitmapImage PhotoImage;
        public Geopoint PhotoGPS;
        public String PhotoDescription;
        public bool PhotoIsSelected;
    }
}
