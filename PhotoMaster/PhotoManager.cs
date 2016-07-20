using PhotoMaster.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Media.Imaging;

namespace PhotoMaster
{
    public sealed class PhotoManager
    {

        private static readonly PhotoManager instance = new PhotoManager();

        private PhotoManager()
        {
            AllPhotos = new List<Photo>();

            FakeDisplayData();
        }

        private void FakeDisplayData()
        {
            Photo photo = new Photo();
            photo.PhotoImage = new BitmapImage(new Uri("ms-appx:/Assets/demo1.jpg"));
            photo.PhotoDescription = "This image is a test image, XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";

            BasicGeoposition position = new BasicGeoposition() { Latitude = 39.992239, Longitude = 116.272634 };
            photo.PhotoGPS = new Geopoint(position);
            AllPhotos.Add(photo);

            photo = new Photo();
            photo.PhotoImage = new BitmapImage(new Uri("ms-appx:/Assets/demo2.jpg"));
            photo.PhotoDescription = "This image is a test image, XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";

            position = new BasicGeoposition() { Latitude = 39.991056, Longitude = 116.275938 };
            photo.PhotoGPS = new Geopoint(position);
            AllPhotos.Add(photo);

            photo = new Photo();
            photo.PhotoImage = new BitmapImage(new Uri("ms-appx:/Assets/demo3.jpg"));
            photo.PhotoDescription = "This image is a test image, XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";

            position = new BasicGeoposition() { Latitude = 39.990201, Longitude = 116.278213 };
            photo.PhotoGPS = new Geopoint(position);
            AllPhotos.Add(photo);
        }

        public static PhotoManager GetInstance()
        {
            return instance;
        }

        public Photo GetPhotoByGPS(Geopoint pos)
        {
            foreach(Photo photo in AllPhotos)
            {
                if (pos.Position.Equals(photo.PhotoGPS.Position))
                {
                    return photo;
                }
            }

            return null;
        }

        public List<Photo> GetPhotoListByGPSArea(Geopoint pos)
        {
            return AllPhotos;
        }

        private List<Photo> AllPhotos;
        public List<Photo> getPhotosToShow(Geopoint center, double zoomLevel)
        {
            List<Photo> ret = new List<Photo>();

            // TO DO
            // Return a list of photos shown in screen via map center and zoomLevel.

            return ret;
        }
    }
}
