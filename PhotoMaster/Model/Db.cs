using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace PhotoMaster.Model
{
    class Db
    {
        public Dictionary<Geoposition, Photo> db = new Dictionary<Geoposition, Photo>();

        public Db()
        {
            

        }
    }
}
