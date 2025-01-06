using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.Plastics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Youth.ViewModels.Base;

namespace Youth.ViewModels
{
    public class MainPlasticsViewModel : BaseViewModel, IMainPlasticsViewModel
    {
        public Command FetchNearestServicePointCommand { get; }
        public ObservableCollection<ServicePoint> ServicePoints { get; set; }
        public SystemSettings systemSettings { get; set; }
        public Command ExploreServicePointCommand { get; }
        public string MapSource { get; set; }

        private ServicePoint _servicePoint;
        public ServicePoint ActiveServicePoint
        {
            get => _servicePoint;
            set
            {
                Debug.WriteLine("MainPlasticsViewModel: ServicePoint");
                SetProperty(ref _servicePoint, value);
            }
        }
        public MainPlasticsViewModel()
        {
            Title = "Service Points";
            //ServicePointMap = new Maps.Map
            //{
            //    MapType = MapType.Hybrid
            //};
            //ServicePointMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(0.3476, 32.5825), Distance.FromMiles(10)));

            ActiveServicePoint = new ServicePoint();
            ActiveServicePoint.address = "Address";
            ActiveServicePoint.label = "◦◦◦";
            ActiveServicePoint.imageUrl = "images/logo.png";
            ServicePoints = new ObservableCollection<ServicePoint>();
            ExploreServicePointCommand = new Command(async () => await ExecuteExploreServicePointCommand());
            FetchNearestServicePointCommand = new Command(async () => await ExecuteFetchNearestServicePointCommand());
            //MapSource = "https://embed.windy.com/embed.html?type=map&location=coordinates&metricRain=default&metricTemp=default&metricWind=default&zoom=5&overlay=wind&product=ecmwf&level=surface&lat=0.321&lon=32.571";
            MapSource = @"
                        <html>
                          <head>
                            <style>
                              .mapouter {
                                position: fixed;
                                text-align: right;
                                left: 0;
                                right: 0;
                                top: 0;
                                bottom: 0;
                              }
                              .gmap_canvas {
                                overflow: hidden;
                                background: none !important;
                                height: 100vh; /* Full height of the viewport */
                                width: 100vw;  /* Full width of the viewport */
                              }
                              iframe {
                                width: 100%;  /* Full width inside the container */
                                height: 100%; /* Full height inside the container */
                                border: 0;    /* Remove border */
                              }
                            </style>
                          </head>
                          <body>
                            <div class='mapouter'>
                              <div class='gmap_canvas'>
                                <iframe 
                                  id='gmap_canvas' 
                                  src='https://maps.google.com/maps?q=Kawempe%2C+Kampala%2C+Uganda&t=k&z=13&ie=UTF8&iwloc=&output=embed' 
                                  frameborder='0' 
                                  scrolling='no' 
                                  marginheight='0' 
                                  marginwidth='0'>
                                </iframe>
                              </div>
                            </div>
                          </body>
                        </html>";

        }
        async Task ExecuteExploreServicePointCommand()
        {
            try
            {
                if (ActiveServicePoint.created_at == null)
                {
                    MessagingCenter.Send<MainPlasticsViewModel, String>(this, "message", "No service point has been detected near you, make sure you have location services enabled.");
                    return;
                }

                await Shell.Current.GoToAsync($"{nameof(ServiceCenterInfoPage)}?{nameof(ServiceCenterInfoViewModel.ServicePointId)}={ActiveServicePoint.id}");
            }
            catch (Exception ex)
            {
                MessagingCenter.Send<MainPlasticsViewModel, String>(this, "message", "No service point has been detected near you, make sure you have location services enabled.");
                Debug.WriteLine("MainPlasticsViewModel: " + ex);
            }
        }
        async Task ExecuteFetchNearestServicePointCommand()
        {
            try
            {
                IsBusy = true;
                OnPropertyChanged("IsBusy");
                var location = await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    try
                    {
                        var serializer = new JsonSerializer();
                        serializer.Error += (sender, args) =>
                        {
                            if (args.ErrorContext.Error is JsonException)
                            {
                                args.ErrorContext.Handled = true;
                            }
                        };

                        systemSettings = await SystemSettings.fetchSystemSettings();
                        Debug.WriteLine("MainPlasticsViewModel: " + systemSettings.currency);
                        OnPropertyChanged("systemSettings");

                        JObject jobsServerObj = await ServicePoint.FetchServicePoints(location.Latitude, location.Longitude);
                        ServicePoints.Clear();
                        JArray jobsArray = (JArray)jobsServerObj.GetValue("data");

                        foreach (JToken token in jobsArray)
                        {
                            JObject chatObj = (JObject)token;
                            ServicePoint servicePoint = chatObj.ToObject<ServicePoint>(serializer);
                            //servicePoint.position= new Position(servicePoint.latitude, servicePoint.longitude);
                            Debug.WriteLine("MainPlasticsViewModel: " + servicePoint.label);

                            //Pin pin = new Pin
                            //{
                            //    AutomationId= servicePoint.id+"",
                            //    Label = servicePoint.label,
                            //    Address = servicePoint.address,
                            //    Type = PinType.Place,
                            //    Position = new Position(servicePoint.latitude, servicePoint.longitude)
                            //};
                            //pin.InfoWindowClicked += async (s, args) =>
                            //{
                            //    string servicePointId = ((Pin)s).AutomationId;
                            //    Debug.WriteLine("InfoWindowClicked: " + servicePointId);
                            //};
                            //ServicePointMap.Pins.Add(pin);
                            ServicePoints.Add(servicePoint);
                        }

                        if (ServicePoints.Count > 0)
                        {
                            ActiveServicePoint = ServicePoints[0];
                            //ServicePointMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(ActiveServicePoint.latitude, ActiveServicePoint.longitude), Distance.FromMiles(10)));
                        }
                        OnPropertyChanged("ServicePoints");
                        OnPropertyChanged("ActiveServicePoint");
                        IsBusy = false;
                        OnPropertyChanged("IsBusy");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("MainJobsViewModel: " + ex);
                        IsBusy = false;
                        OnPropertyChanged("IsBusy");
                    }
                    finally
                    {
                        IsBusy = false;
                        OnPropertyChanged("JobCategories");
                        OnPropertyChanged("JobModeles");
                        OnPropertyChanged("IsBusy");
                        OnPropertyChanged("ServicePointMap");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("JobDetailsViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
        }
        public void OnAppearing()
        {
            UpdateAuthStatus();
            Debug.WriteLine("MainPlasticsViewModel: OnAppearing()");
            FetchNearestServicePointCommand.Execute(this);
        }
    }
}
