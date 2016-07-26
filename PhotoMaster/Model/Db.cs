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
            BitmapImage image;
            BasicGeoposition position;
            Geopoint gpsPoint;
            Photo photo;


            image = new BitmapImage(new Uri("ms-appx:/Assets/photos/015.jpg"));
            position = new BasicGeoposition() { Latitude = 39.998373, Longitude = 116.274636 };
            gpsPoint = new Geopoint(position);
            photo = new Photo(15, image, gpsPoint, 95, "ms-appx:///Assets/photos/015-small.jpg", "ms-appx:///Assets/photos/015-small-check.jpg");
            photos.Add(photo);

            image = new BitmapImage(new Uri("ms-appx:/Assets/photos/018.jpg"));
            position = new BasicGeoposition() { Latitude = 39.997715, Longitude = 116.272533 };
            gpsPoint = new Geopoint(position);
            photo = new Photo(18, image, gpsPoint, 94, "ms-appx:///Assets/photos/018-small.jpg", "ms-appx:///Assets/photos/018-small-check.jpg");
            photos.Add(photo);

            image = new BitmapImage(new Uri("ms-appx:/Assets/photos/099.jpg"));
            position = new BasicGeoposition() { Latitude = 39.998036, Longitude = 116.278949 };
            gpsPoint = new Geopoint(position);
            photo = new Photo(99, image, gpsPoint, 93, "ms-appx:///Assets/photos/099-small.jpg", "ms-appx:///Assets/photos/099-small-check.jpg");
            photos.Add(photo);

            image = new BitmapImage(new Uri("ms-appx:/Assets/photos/020.jpg"));
            position = new BasicGeoposition() { Latitude = 39.996778, Longitude = 116.278959 };
            gpsPoint = new Geopoint(position);
            photo = new Photo(20, image, gpsPoint, 92, "ms-appx:///Assets/photos/020-small.jpg", "ms-appx:///Assets/photos/020-small-check.jpg");
            photos.Add(photo);

            image = new BitmapImage(new Uri("ms-appx:/Assets/photos/021.jpg"));
            position = new BasicGeoposition() { Latitude = 39.997222, Longitude = 116.279614 };
            gpsPoint = new Geopoint(position);
            photo = new Photo(21, image, gpsPoint, 91, "ms-appx:///Assets/photos/021-small.jpg", "ms-appx:///Assets/photos/021-small-check.jpg");
            photos.Add(photo);

            image = new BitmapImage(new Uri("ms-appx:/Assets/photos/022.jpg"));
            position = new BasicGeoposition() { Latitude = 39.996811, Longitude = 116.279936 };
            gpsPoint = new Geopoint(position);
            photo = new Photo(22, image, gpsPoint, 90, "ms-appx:///Assets/photos/022-small.jpg", "ms-appx:///Assets/photos/022-small-check.jpg");
            photos.Add(photo);

            image = new BitmapImage(new Uri("ms-appx:/Assets/photos/040.jpg"));
            position = new BasicGeoposition() { Latitude = 39.990712, Longitude = 116.27941 };
            gpsPoint = new Geopoint(position);
            photo = new Photo(40, image, gpsPoint, 89, "ms-appx:///Assets/photos/040-small.jpg", "ms-appx:///Assets/photos/040-small-check.jpg", "Summer bull located on the north banks of the profile, such as kiosks, when Emperor Qianlong which adorn this is the hope that it Wing Yau water town, long flood surrender to the garden and nearby people bring endless Xiangfu. To illustrate the significance of the construction of the bull, the emperor deliberately wrote a four - character inscription, with the body of the book of seal engraved on the front and the bull. Copper is the Summer Palace, Kunming Lake Cattle east shore of a unique landscape and art treasures.");
            photos.Add(photo);

            image = new BitmapImage(new Uri("ms-appx:/Assets/photos/060.jpg"));
            position = new BasicGeoposition() { Latitude = 39.986248, Longitude = 116.273786 };
            gpsPoint = new Geopoint(position);
            photo = new Photo(60, image, gpsPoint, 87, "ms-appx:///Assets/photos/060-small.jpg", "ms-appx:///Assets/photos/060-small-check.jpg");
            photos.Add(photo);

            image = new BitmapImage(new Uri("ms-appx:/Assets/photos/017.jpg"));
            position = new BasicGeoposition() { Latitude = 39.998693, Longitude = 116.275247 };
            gpsPoint = new Geopoint(position);
            photo = new Photo(17, image, gpsPoint, 86, "ms-appx:///Assets/photos/017-small.jpg", "ms-appx:///Assets/photos/017-small-check.jpg");
            photos.Add(photo);

            image = new BitmapImage(new Uri("ms-appx:/Assets/photos/016.jpg"));
            position = new BasicGeoposition() { Latitude = 39.999383, Longitude = 116.273906 };
            gpsPoint = new Geopoint(position);
            photo = new Photo(16, image, gpsPoint, 85, "ms-appx:///Assets/photos/016-small.jpg", "ms-appx:///Assets/photos/016-small-check.jpg");
            photos.Add(photo);

            image = new BitmapImage(new Uri("ms-appx:/Assets/photos/061.jpg"));
            position = new BasicGeoposition() { Latitude = 39.98619, Longitude = 116.273915 };
            gpsPoint = new Geopoint(position);
            photo = new Photo(61, image, gpsPoint, 84, "ms-appx:///Assets/photos/061-small.jpg", "ms-appx:///Assets/photos/061-small-check.jpg");
            photos.Add(photo);

            image = new BitmapImage(new Uri("ms-appx:/Assets/photos/062.jpg"));
            position = new BasicGeoposition() { Latitude = 39.98626, Longitude = 116.273931 };
            gpsPoint = new Geopoint(position);
            photo = new Photo(62, image, gpsPoint, 83, "ms-appx:///Assets/photos/062-small.jpg", "ms-appx:///Assets/photos/062-small-check.jpg");
            photos.Add(photo);

            image = new BitmapImage(new Uri("ms-appx:/Assets/photos/054.jpg"));
            position = new BasicGeoposition() { Latitude = 39.983601, Longitude = 116.27355 };
            gpsPoint = new Geopoint(position);
            photo = new Photo(54, image, gpsPoint, 82, "ms-appx:///Assets/photos/054-small.jpg", "ms-appx:///Assets/photos/054-small-check.jpg");
            photos.Add(photo);

            image = new BitmapImage(new Uri("ms-appx:/Assets/photos/011.jpg"));
            position = new BasicGeoposition() { Latitude = 39.996223, Longitude = 116.26758 };
            gpsPoint = new Geopoint(position);
            photo = new Photo(11, image, gpsPoint, 81, "ms-appx:///Assets/photos/011-small.jpg", "ms-appx:///Assets/photos/011-small-check.jpg");
            photos.Add(photo);

            image = new BitmapImage(new Uri("ms-appx:/Assets/photos/075.jpg"));
            position = new BasicGeoposition() { Latitude = 39.993428, Longitude = 116.267236 };
            gpsPoint = new Geopoint(position);
            photo = new Photo(75, image, gpsPoint, 80, "ms-appx:///Assets/photos/075-small.jpg", "ms-appx:///Assets/photos/075-small-check.jpg");
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
