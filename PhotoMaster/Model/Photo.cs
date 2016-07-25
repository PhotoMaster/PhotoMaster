using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Media.Imaging;

namespace PhotoMaster.Model
{
    public class Photo:IComparer<Photo>
    {
        public int PhotoId;
        public BitmapImage PhotoImage;
        public Geopoint PhotoGPS;
        public String PhotoDescription;
        public bool PhotoIsSelected;
        public double PhotoScore;
        public string PhotoUri;
        public string PhotoCheckedUri;

        public Photo()
        {

        }

        public Photo(int id, BitmapImage image, Geopoint gps, double score, string uri, string checkedUri, String des = null)
        {
            PhotoId = id;
            PhotoUri = uri;
            PhotoCheckedUri = checkedUri;
            PhotoImage = image;
            PhotoGPS = gps;
            PhotoIsSelected = false;
            PhotoScore = score;
            if (null == des)
            {
                PhotoDescription = "This image is a test image, XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
            }
            else
            {
                PhotoDescription = des;
            }
            
        }

        public int Compare(Photo x, Photo y)
        {
            return Comparer.Default.Compare(x.PhotoScore, y.PhotoScore);
        }

        public bool Equals(Photo x)
        {
            bool ret = false;
            if (x.PhotoId == this.PhotoId)
            {
                ret = true;
            }
            return ret;
        }

    }
}
