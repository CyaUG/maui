using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Maps;
using Youth.Models;
using System.Net;

namespace Youth.Views.Account
{

    public partial class LocationPickerPage : ContentPage
    {
        public LocationPickerPage()
        {
            InitializeComponent();
        }

        public async Task<Location> GetPositionAsync()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    return new Location(location.Latitude, location.Longitude);
                }
                else
                {
                    Console.WriteLine("Could not get the location");
                    return new Location();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new Location();
            }
        }

        //bool firsttime = true;
        //public async void MyMap_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    try
        //    {
        //        LocationModule position = new LocationModule();
        //        var m = (Map)sender;
        //        if (m.VisibleRegion != null)
        //        {
        //            if (firsttime == true)
        //            {
        //                firsttime = false;
        //                return;
        //            }
        //            selectedPosition = m.VisibleRegion.Center;
        //            var address = await GetGeocodeReverseData(selectedPosition);
        //            LocationLabel.Text = address;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //}

        bool firsttime = true;
        public async void MyMap_PropertyChanged(Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                if (e.PropertyName == nameof(Microsoft.Maui.Controls.Maps.Map.VisibleRegion)) // Only run when VisibleRegion changes
                {
                    var map = (Microsoft.Maui.Controls.Maps.Map)sender;

                    if (map.VisibleRegion != null)
                    {
                        // Skip the first time to avoid early triggers
                        if (firsttime)
                        {
                            firsttime = false;
                            return;
                        }

                        // Now you can safely access the center
                        selectedPosition = map.VisibleRegion.Center;

                        var address = await GetGeocodeReverseData(selectedPosition);
                        LocationLabel.Text = address;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        private Location selectedPosition;
        public async void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"MapClick: {e.Location.Latitude}, {e.Location.Longitude}");

                selectedPosition = e.Location;
                var address = await GetGeocodeReverseData(selectedPosition);
                LocationLabel.Text = address;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async void OnConfirmLocationClicked(object sender, EventArgs e)
        {
            try
            {
                if (selectedPosition != null)
                {
                    var address = await GetGeocodeReverseData(selectedPosition);
                    LocationLabel.Text = address;

                    LocationModule location = new LocationModule();
                    location.Latitude = selectedPosition.Latitude;
                    location.Longitude = selectedPosition.Longitude;
                    location.Address = address;
                    MessagingCenter.Send<LocationPickerPage, LocationModule>(this, "location", location);
                    await Shell.Current.GoToAsync("..");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async void OnLoadCurentLocation(object sender, EventArgs e)
        {
            try
            {
                selectedPosition = await GetPositionAsync();
                var mapSpan = MapSpan.FromCenterAndRadius(selectedPosition, Distance.FromKilometers(1));
                MyMap.MoveToRegion(mapSpan);

                var address = await GetGeocodeReverseData(selectedPosition);
                LocationLabel.Text = address;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                // Initial region move to default location
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(0.3476, 32.5825), Distance.FromMiles(10)));

                // Await for the user's current position
                selectedPosition = await GetPositionAsync();

                // Move map to the user's location
                var mapSpan = MapSpan.FromCenterAndRadius(selectedPosition, Distance.FromKilometers(1));
                MyMap.MoveToRegion(mapSpan);

                // Optionally wait until VisibleRegion is updated
                await Task.Delay(500); // Give time for the map to render

                // Get the address of the selected position
                var address = await GetGeocodeReverseData(selectedPosition);
                LocationLabel.Text = address;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        public async Task<string> GetGeocodeReverseData(Location location)
        {
            try
            {
                IEnumerable<Placemark> placemarks = await Geocoding.Default.GetPlacemarksAsync(location.Longitude, location.Longitude);

                Placemark placemark = placemarks?.FirstOrDefault();

                if (placemark.FeatureName != null)
                {
                    return $"{placemark.FeatureName}, {placemark.SubAdminArea}, {placemark.AdminArea}, {placemark.CountryName}";
                }
                return location.Longitude + "," + location.Longitude;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return location.Longitude + "," + location.Longitude;
            }
        }
    }
}