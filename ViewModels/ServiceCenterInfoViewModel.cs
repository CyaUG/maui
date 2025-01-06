using Youth.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Youth.ViewModels.Interfaces;
using Youth.ViewModels.Base;

namespace Youth.ViewModels
{
    [QueryProperty(nameof(ServicePointId), nameof(ServicePointId))]
    class ServiceCenterInfoViewModel : BaseViewModel, IServiceCenterInfoViewModel
    {
        public ObservableCollection<ServicePointPlasticsPrice> ServicePointPlasticPricing { get; set; }
        public ObservableCollection<ServicePointGallery> mServicePointGallery { get; set; }
        public SystemSettings systemSettings { get; set; }
        public Command ServicePointAddressCommand { get; }
        public Command ServicePointWebsiteCommand { get; }
        public Command ServicePointEmailCommand { get; }
        public Command ServicePointPhoneCommand { get; }

        private int _ServicePointId;
        public int ServicePointId
        {
            get => _ServicePointId;
            set
            {
                SetProperty(ref _ServicePointId, value);
                FetchServicePointDetails(value);
            }
        }

        private Models.ServicePoint _servicePoint;
        public Models.ServicePoint ActiveServicePoint
        {
            get => _servicePoint;
            set
            {
                Debug.WriteLine("ServiceCenterInfoViewModel: ServicePoint");
                SetProperty(ref _servicePoint, value);
            }
        }
        public ServiceCenterInfoViewModel()
        {
            Title = "Service Points";
            ServicePointPlasticPricing = new ObservableCollection<ServicePointPlasticsPrice>();
            mServicePointGallery = new ObservableCollection<ServicePointGallery>();
            ServicePointAddressCommand = new Command(async () => await ExecuteServicePointAddressCommand());
            ServicePointWebsiteCommand = new Command(async () => await ExecuteServicePointWebsiteCommand());
            ServicePointEmailCommand = new Command(async () => await ExecuteServicePointEmailCommand());
            ServicePointPhoneCommand = new Command(async () => await ExecuteServicePointPhoneCommand());
        }
        async Task ExecuteServicePointPhoneCommand()
        {
            if (ActiveServicePoint == null)
                return;

            var phoneCall = new Uri("tel:" + ActiveServicePoint.phone);
            await Launcher.Default.OpenAsync(phoneCall);
        }
        async Task ExecuteServicePointEmailCommand()
        {
            if (ActiveServicePoint == null)
                return;

            var email = new Uri("mailto:" + ActiveServicePoint.email + "?subject=Subject&body=Body");
            await Launcher.Default.OpenAsync(email);
        }
        async Task ExecuteServicePointWebsiteCommand()
        {
            if (ActiveServicePoint == null)
                return;

            var uri = new Uri("https://" + ActiveServicePoint.website);

            await Browser.OpenAsync(uri, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
            });
        }
        async Task ExecuteServicePointAddressCommand()
        {
            if (ActiveServicePoint == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            Debug.WriteLine("JobDetailsViewModel: OnJobAddressSelected()");
            await Map.OpenAsync(ActiveServicePoint.latitude, ActiveServicePoint.longitude, new MapLaunchOptions { Name = ActiveServicePoint.address });
        }
        async Task FetchServicePointDetails(int servicePointId)
        {
            try
            {
                IsBusy = true;
                OnPropertyChanged("IsBusy");

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
                    OnPropertyChanged("systemSettings");

                    JObject serverObj = await Models.ServicePoint.FetchServicePointDetails(servicePointId);
                    JObject ServicePointObj = (JObject)serverObj.GetValue("data");
                    ActiveServicePoint = ServicePointObj.ToObject<Models.ServicePoint>(serializer);

                    JObject galleryObj = await ServicePointGallery.FetchServicePointGallery(ActiveServicePoint.id);
                    JArray galleryArray = (JArray)galleryObj.GetValue("data");

                    foreach (JToken token in galleryArray)
                    {
                        JObject chatObj = (JObject)token;
                        ServicePointGallery servicePointGallery = chatObj.ToObject<ServicePointGallery>(serializer);
                        Debug.WriteLine("ServiceCenterInfoViewModel: " + servicePointGallery.imageUrl);
                        mServicePointGallery.Add(servicePointGallery);
                    }

                    JObject pricingObj = await ServicePointPlasticsPrice.FetchServicePointPlasticsPricing(ActiveServicePoint.id);
                    ServicePointPlasticPricing.Clear();
                    JArray pricingArray = (JArray)pricingObj.GetValue("data");

                    foreach (JToken token in pricingArray)
                    {
                        JObject priceObj = (JObject)token;
                        ServicePointPlasticsPrice servicePointPlasticsPrice = priceObj.ToObject<ServicePointPlasticsPrice>(serializer);
                        Debug.WriteLine("ServiceCenterInfoViewModel: " + servicePointPlasticsPrice.label);
                        servicePointPlasticsPrice.currency = systemSettings.currency;
                        ServicePointPlasticPricing.Add(servicePointPlasticsPrice);
                    }
                    OnPropertyChanged("ServicePointPlasticPricing");
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
                }
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
        public void OnAppearing()
        {
            UpdateAuthStatus();
            Debug.WriteLine("ServiceCenterInfoViewModel: OnAppearing()");
        }
    }
}