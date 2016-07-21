using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Media.Imaging;

namespace PhotoMaster.Model
{
    class Db
    {
        private static Db instance = null;

        //public SortedDictionary<Geopoint, Photo> photos = new SortedDictionary<Geopoint, Photo>();
        public List<Photo> photos = new List<Photo>();

        public Db()
        {

            BitmapImage image = new BitmapImage(new Uri("ms-appx:/Assets/photos/040.jpg"));
            BasicGeoposition position = new BasicGeoposition() { Latitude = 39.990712, Longitude = 116.27941 };
            Geopoint gpsPoint = new Geopoint(position);
            Photo photo = new Photo(40, image, gpsPoint, 93, "ms-appx:///Assets/photos/040-small.jpg");
            photos.Add(photo);

            image = new BitmapImage(new Uri("ms-appx:/Assets/photos/018.jpg"));
            position = new BasicGeoposition() { Latitude = 39.997699, Longitude = 116.272619 };
            gpsPoint = new Geopoint(position);
            photo = new Photo(18, image, gpsPoint, 92, "ms-appx:///Assets/photos/018-small.jpg");
            photos.Add(photo);

            image = new BitmapImage(new Uri("ms-appx:/Assets/photos/060.jpg"));
            position = new BasicGeoposition() { Latitude = 39.986248, Longitude = 116.273786 };
            gpsPoint = new Geopoint(position);
            photo = new Photo(60, image, gpsPoint, 91, "ms-appx:///Assets/photos/060-small.jpg");
            photos.Add(photo);

            image = new BitmapImage(new Uri("ms-appx:/Assets/photos/061.jpg"));
            position = new BasicGeoposition() { Latitude = 39.98619, Longitude = 116.273915 };
            gpsPoint = new Geopoint(position);
            photo = new Photo(61, image, gpsPoint, 89, "ms-appx:///Assets/photos/061-small.jpg");
            photos.Add(photo);

            image = new BitmapImage(new Uri("ms-appx:/Assets/photos/062.jpg"));
            position = new BasicGeoposition() { Latitude = 39.98626, Longitude = 116.273931 };
            gpsPoint = new Geopoint(position);
            photo = new Photo(62, image, gpsPoint, 88, "ms-appx:///Assets/photos/062-small.jpg");
            photos.Add(photo);

            image = new BitmapImage(new Uri("ms-appx:/Assets/photos/054.jpg"));
            position = new BasicGeoposition() { Latitude = 39.983601, Longitude = 116.27355 };
            gpsPoint = new Geopoint(position);
            photo = new Photo(54, image, gpsPoint, 87, "ms-appx:///Assets/photos/054-small.jpg");
            photos.Add(photo);

            image = new BitmapImage(new Uri("ms-appx:/Assets/photos/011.jpg"));
            position = new BasicGeoposition() { Latitude = 39.996223, Longitude = 116.26758 };
            gpsPoint = new Geopoint(position);
            photo = new Photo(11, image, gpsPoint, 86, "ms-appx:///Assets/photos/011-small.jpg");
            photos.Add(photo);

            image = new BitmapImage(new Uri("ms-appx:/Assets/photos/075.jpg"));
            position = new BasicGeoposition() { Latitude = 39.993428, Longitude = 116.267236 };
            gpsPoint = new Geopoint(position);
            photo = new Photo(75, image, gpsPoint, 85, "ms-appx:/Assets/photos/075-small.jpg");
            photos.Add(photo);

        }
        public static Db GetInstance()
        {
            if (instance == null)
            {
                instance = new Db();
            }
            return instance;
        }
    }
}
