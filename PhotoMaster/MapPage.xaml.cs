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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PhotoMaster
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class MapPage : Page
    {
        public MapPage()
        {
            suggestions = new ObservableCollection<string>();
            this.InitializeComponent();
        }

        private ObservableCollection<String> suggestions;

        private void displayPOI(Geopoint snPoint)
        {
            // Create a MapIcon.
            MapIcon mapIcon1 = new MapIcon();
            mapIcon1.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/a-small.jpg"));
            mapIcon1.Location = snPoint;
            mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
            mapIcon1.ZIndex = 0;

            // Add the MapIcon to the map.
            MapControl1.MapElements.Add(mapIcon1);

            // Center the map over the POI.
            MapControl1.Center = snPoint;
            MapControl1.ZoomLevel = 14;
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
                    MapControl1.ZoomLevel = 12;
                    MapControl1.LandmarksVisible = true;
                    break;

                case GeolocationAccessStatus.Denied:
                    // Handle the case  if access to location is denied.
                    // Set the map location.
                    MapControl1.Center = cityCenter;
                    MapControl1.ZoomLevel = 12;
                    MapControl1.LandmarksVisible = true;
                    break;

                case GeolocationAccessStatus.Unspecified:
                    // Handle the case if  an unspecified error occurs.
                    // Set the map location.
                    MapControl1.Center = cityCenter;
                    MapControl1.ZoomLevel = 12;
                    MapControl1.LandmarksVisible = true;
                    break;
            }
            displayPOI(cityCenter);
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
                MapControl1.ZoomLevel = 12;
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
            suggestions.Add(sender.Text + "1");
            suggestions.Add(sender.Text + "2");
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


    }
}
