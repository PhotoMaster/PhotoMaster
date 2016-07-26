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
            //FakeDisplayData();
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
            List<Photo> ret = new List<Photo>();
            
            double range = 0.01;

            foreach (Photo p in db.photos)
            {
                if (p.PhotoGPS.Position.Latitude < pos.Position.Latitude + range && p.PhotoGPS.Position.Latitude > pos.Position.Latitude - range
                    && p.PhotoGPS.Position.Longitude < pos.Position.Longitude + range && p.PhotoGPS.Position.Longitude > pos.Position.Longitude - range)
                {
                    // We suppose db.photos have already been sorted by their score.
                    if (ret.Count < 5)
                    {
                        ret.Add(p);
                    }
                    else
                    {
                        break;
                    }

                }
            }
            Photo tmp = new Photo();
            while (ret.Count < 5)
            {
                ret.Add(tmp);
            }

            return ret;
        }

        public Dictionary<Photo,bool> getPhotosToShow(Geopoint center, double zoomLevel)
        {
            Dictionary<Photo, bool> ret = new Dictionary<Photo, bool>();


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
                        ret.Add(p,false);
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
