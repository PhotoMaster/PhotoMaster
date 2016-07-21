﻿using PhotoMaster.Model;
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

using Windows.UI.Core;
using Windows.UI.Notifications;
using System.Xml;
using System.Diagnostics;
using Windows.UI;

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
        private MapIcon selfLocationIcon = new MapIcon();
        Geolocator geolocator;

        public MapPage()
        {
            suggestions = new ObservableCollection<string>();
            pm = PhotoManager.GetInstance();
            this.InitializeComponent();
        }

        private void displayPhotosPOI()
        {
            List<Photo> photos = pm.getPhotosToShow(MapControl1.Center, MapControl1.ZoomLevel);
            MapControl1.MapElements.Clear();
            MapControl1.MapElements.Add(selfLocationIcon);
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

        private async void drawRoute(Geopoint start, Geopoint end, bool walk = true)
        {
            MapRouteFinderResult routeResult;
            if (walk)
            {
                routeResult =
                        await MapRouteFinder.GetWalkingRouteAsync(
                            start,
                            end);
            }
            else
            {
                routeResult =
                        await MapRouteFinder.GetDrivingRouteAsync(
                            start,
                            end,
                            MapRouteOptimization.Time,
                            MapRouteRestrictions.None);
            }
            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Blue;
                viewOfRoute.OutlineColor = Colors.Black;

                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                MapControl1.Routes.Add(viewOfRoute);

                // Fit the MapControl to the route.
                await MapControl1.TrySetViewBoundsAsync(
                      routeResult.Route.BoundingBox,
                      null,
                      Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
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
                    geolocator = new Geolocator { ReportInterval = 2000 };
                    // Subscribe to the PositionChanged event to get location updates.
                    geolocator.PositionChanged += OnPositionChanged;

                    // Subscribe to StatusChanged event to get updates of location status changes.
                    geolocator.StatusChanged += OnStatusChanged;
                    Geoposition pos = await geolocator.GetGeopositionAsync();
                    cityCenter = pos.Coordinate.Point;

                    // Set the map location.
                    MapControl1.Center = cityCenter;
                    MapControl1.ZoomLevel = 14;
                    MapControl1.LandmarksVisible = true;

                    selfLocationIcon = new MapIcon();
                    selfLocationIcon.Location = cityCenter;
                    selfLocationIcon.NormalizedAnchorPoint = new Point(0.5, 1.0);
                    selfLocationIcon.ZIndex = 0;
                    MapControl1.MapElements.Add(selfLocationIcon);


                    BasicGeoposition startLocation = new BasicGeoposition() { Latitude = 39.9784, Longitude = 116.2749 };
                    BasicGeoposition endLocation = new BasicGeoposition() { Latitude = 39.990712, Longitude = 116.27941 };
                    drawRoute(new Geopoint(startLocation), new Geopoint(endLocation));



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

            displayPhotosPOI();

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

                Photo photo = pm.GetPhotoByGPS(mapIcon.Location);
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

        async private void OnStatusChanged(Geolocator sender, StatusChangedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                // Show the location setting message only if status is disabled.
                switch (e.Status)
                {
                    case PositionStatus.Ready:
                        // Location platform is providing valid data.
                        Debug.WriteLine("Ready");
                        break;

                    case PositionStatus.Initializing:
                        // Location platform is attempting to acquire a fix. 
                        Debug.WriteLine("Initializing");
                        break;

                    case PositionStatus.NoData:
                        // Location platform could not obtain location data.
                        Debug.WriteLine("No data");
                        break;

                    case PositionStatus.Disabled:
                        // The permission to access location data is denied by the user or other policies.
                        Debug.WriteLine("Disabled");

                        // Clear any cached location data.
                        //UpdateLocationData(null);
                        break;

                    case PositionStatus.NotInitialized:
                        // The location platform is not initialized. This indicates that the application 
                        // has not made a request for location data.
                        Debug.WriteLine("Not initialized");
                        break;

                    case PositionStatus.NotAvailable:
                        // The location platform is not available on this version of the OS.
                        Debug.WriteLine("Not available");
                        break;

                    default:
                        Debug.WriteLine("Unknown");
                        break;
                }
            });
        }
        async private void OnPositionChanged(Geolocator sender, PositionChangedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {

                BasicGeoposition iconPosition = new BasicGeoposition() { Latitude = e.Position.Coordinate.Point.Position.Latitude, Longitude = e.Position.Coordinate.Point.Position.Longitude };
                Geopoint location = new Geopoint(iconPosition);
                selfLocationIcon.Location = location;

            });
        }

        private async void selfPositionButton_click(object sender, RoutedEventArgs e)
        {
            Geoposition pos = await geolocator.GetGeopositionAsync();
            MapControl1.Center = pos.Coordinate.Point;
        }

        private void MapControl1_ZoomLevelChanged(MapControl sender, object args)
        {
            displayPhotosPOI();
        }

        private void MapControl1_CenterChanged(MapControl sender, object args)
        {
            displayPhotosPOI();
        }
    }
}
