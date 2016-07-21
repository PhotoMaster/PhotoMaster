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

        private static PhotoManager instance = null;

        private Db db;

        private PhotoManager()
        {
            db = Db.GetInstance();
            AllPhotos = new List<Photo>();
            //FakeDisplayData();
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
            if (instance == null)
            {
                instance = new PhotoManager();
            }
            return instance;
        }

        public Photo GetPhotoByGPS(Geopoint pos)
        {
            foreach(Photo photo in db.photos)
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


            // Return a list of photos shown in screen via map center and zoomLevel.
             // Should be set by zoomLevel
            double range = 0.01;

            foreach (Photo p in db.photos)
            {
                if (p.PhotoGPS.Position.Latitude<center.Position.Latitude+ range && p.PhotoGPS.Position.Latitude > center.Position.Latitude - range 
                    && p.PhotoGPS.Position.Longitude<center.Position.Longitude+ range && p.PhotoGPS.Position.Longitude > center.Position.Longitude - range)
                {
                    // We suppose db.photos have already been sorted by their score.
                    if (ret.Count < 10)
                    {
                        ret.Add(p);
                    }else
                    {
                        break;
                    }
                    
                }
            }


            return ret;
        }
        
    }
}
