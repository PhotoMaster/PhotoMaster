using PhotoMaster.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using PhotoMaster.Model;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PhotoMaster
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class MapPage : Page
    {

        private ObservableCollection<String> suggestions;
        private PhotoManager pm;

        public MapPage()
        {
            suggestions = new ObservableCollection<string>();
            pm = PhotoManager.GetInstance();
            this.InitializeComponent();
        }

        private void displayPOI(List<Photo> photos)
        {
            foreach (Photo photo in photos)
            {
                BasicGeoposition iconPosition = new BasicGeoposition() { Latitude = photo.PhotoGPS.Position.Latitude, Longitude = photo.PhotoGPS.Position.Longitude };
                Geopoint location = new Geopoint(iconPosition);
                // Create a MapIcon.
                MapIcon mapIcon1 = new MapIcon();
                mapIcon1.Image = RandomAccessStreamReference.CreateFromUri(new Uri(photo.PhotoUri));
                mapIcon1.Location = location;
                mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
                mapIcon1.ZIndex = 0;

                // Add the MapIcon to the map.
                MapControl1.MapElements.Add(mapIcon1);
            }


        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            // Set your current location.
            var accessStatus = await Geolocator.RequestAccessAsync();
            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = 39, Longitude = 116 };
            Geopoint cityCenter = new Geopoint(cityPosition);
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:

                    // Get the current location.
                    Geolocator geolocator = new Geolocator();
                    Geoposition pos = await geolocator.GetGeopositionAsync();
                    cityCenter = pos.Coordinate.Point;

                    // Set the map location.
                    MapControl1.Center = cityCenter;
                    MapControl1.ZoomLevel = 14;
                    MapControl1.LandmarksVisible = true;
                    break;

                case GeolocationAccessStatus.Denied:
                    // Handle the case  if access to location is denied.
                    // Set the map location.
                    MapControl1.Center = cityCenter;
                    MapControl1.ZoomLevel = 14;
                    MapControl1.LandmarksVisible = true;
                    break;

                case GeolocationAccessStatus.Unspecified:
                    // Handle the case if  an unspecified error occurs.
                    // Set the map location.
                    MapControl1.Center = cityCenter;
                    MapControl1.ZoomLevel = 14;
                    MapControl1.LandmarksVisible = true;
                    break;
            }
            List<Photo> photoToShow = pm.getPhotosToShow(MapControl1.Center, MapControl1.ZoomLevel);
            displayPOI(photoToShow);

        }


        private async void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            string addressToGeocode;
            if (args.ChosenSuggestion != null)
                addressToGeocode = args.ChosenSuggestion.ToString();
            else
                addressToGeocode = sender.Text;

            MapLocationFinderResult result =
                await MapLocationFinder.FindLocationsAsync(addressToGeocode, MapControl1.Center, 3);

            if (result.Status == MapLocationFinderStatus.Success && result.Locations.Count != 0)
            {
                MapControl1.Center = result.Locations[0].Point;
                MapControl1.ZoomLevel = 14;
                MapControl1.LandmarksVisible = true;
            }
            else
            {
                sender.Text = "";
            }
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // TO DO
            suggestions.Clear();
            /*
            int maxSuggestion = 2;
            List<string> similarNames = findSimilarNames(sender.Text);
            for(int i=0;i<similarNames.Count || i< maxSuggestion; i++)
            {
                suggestions.Add(similarNames[i]);
            }
            */
            suggestions.Add("suggestion1");
            suggestions.Add("suggestion2");

            // END
            sender.ItemsSource = suggestions;
        }

        private void OnMapTapped(MapControl sender, MapInputEventArgs args)
        {
            IReadOnlyList<MapElement> mapElements = MapControl1.FindMapElementsAtOffset(args.Position);

            foreach (MapElement mapElement in mapElements)
            {
                MapIcon mapIcon = (MapIcon)(mapElement);
                if (mapIcon == null) continue;

                Photo photo = PhotoManager.GetInstance().GetPhotoByGPS(mapIcon.Location);
                if(photo != null)
                {
                    Frame root = Window.Current.Content as Frame;
                    root.Navigate(typeof(DetailView), photo);
                }
                
                return;
            }
        }


        private List<string> findSimilarNames(string palace_name)
        {
            List<string> ret = new List<string>();

            // TO DO
            // Return similar palace_names of name in search bar.

            return ret;
        }

    }
}
