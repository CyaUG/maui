using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.jobs;
using Youth.Views.Quizzes;
using Youth.Views.SafeSpace;
using Youth.Views.Shopping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Youth.ViewModels.Base;

namespace Youth.ViewModels
{
    internal class DeliveryInfoProviderViewModel : BaseViewModel, IDeliveryInfoProviderViewModel
    {
        public UserAccount userAccount { get; set; }
        public SystemSettings systemSettings { get; set; }

        private LocationModule _Location;
        public LocationModule activeLocation
        {
            get { return _Location; }
            set
            {
                _Location = value;
                OnPropertyChanged("activeLocation");
            }
        }
        public string LocationAddress { get; set; }
        public String SetLocationAddress
        {
            get { return LocationAddress; }
            set
            {
                LocationAddress = value;
                OnPropertyChanged("LocationAddress");
            }
        }

        private string _nameValue;
        public string NameValue
        {
            get { return _nameValue; }
            set
            {
                _nameValue = value;
                OnPropertyChanged("NameValue");
            }
        }

        private string _phoneValue;
        public string PhoneValue
        {
            get { return _phoneValue; }
            set
            {
                _phoneValue = value;
                OnPropertyChanged("PhoneValue");
            }
        }

        private string _DirectionsValue;
        public string DirectionsValue
        {
            get { return _DirectionsValue; }
            set
            {
                _DirectionsValue = value;
                OnPropertyChanged("DirectionsValue");
            }
        }

        public Command SetAddressCommand { get; set; }
        //public Command LoadAccountCommand { get; set; }

        public DeliveryInfoProviderViewModel()
        {
            Title = "Delivery Info";
            LocationAddress = "Tap To Pick Location";
            SetAddressCommand = new Command(async () => await ExecuteSetAddressCommand());
            //LoadAccountCommand = new Command(async () => await ExecuteLoadAccountCommand());
            _ = ExecuteLoadAccountCommand();
        }
        async Task ExecuteSetAddressCommand()
        {
            bool goON = true;
            DeliveryAddress deliveryAddress = new DeliveryAddress();

            if (string.IsNullOrEmpty(NameValue))
            {
                goON = false;
            }

            if (string.IsNullOrEmpty(PhoneValue))
            {
                goON = false;
            }

            if (string.IsNullOrEmpty(DirectionsValue))
            {
                goON = false;
            }

            if (activeLocation == null)
            {
                goON = false;
            }

            if (goON)
            {
                deliveryAddress.latitude = activeLocation.Latitude;
                deliveryAddress.longitude = activeLocation.Longitude;
                deliveryAddress.place_id = "";
                deliveryAddress.locationAddress = SetLocationAddress;
                deliveryAddress.user_address = SetLocationAddress;

                deliveryAddress.country = "";
                deliveryAddress.CountryCode = "";
                deliveryAddress.AdminArea = "";
                deliveryAddress.PostalCode = "";
                deliveryAddress.SubAdminArea = "";
                deliveryAddress.city = "";
                deliveryAddress.SubThoroughfare = "";

                deliveryAddress.fullname = NameValue;
                deliveryAddress.your_phone_number = PhoneValue;
                deliveryAddress.unit_apt = DirectionsValue;

                MessagingCenter.Send<DeliveryInfoProviderViewModel, DeliveryAddress>(this, "deliveryAddress", deliveryAddress);
                GoBack();
            }
            else
            {
                MessagingCenter.Send<DeliveryInfoProviderViewModel, string>(this, "message", "Provide all the details");
            }
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
        async Task ExecuteLoadAccountCommand()
        {
            Debug.WriteLine("DeliveryInfoProviderViewModel: ExecuteLoadAccountCommand()");
            IsBusy = true;

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
                Debug.WriteLine("DeliveryInfoProviderViewModel: " + systemSettings.currency);
                OnPropertyChanged("systemSettings");

                JObject accObj = await UserAccount.LoadMyProfileAsync();
                userAccount = accObj.ToObject<UserAccount>(serializer);
                OnPropertyChanged("userAccount");


                IsBusy = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DeliveryInfoProviderViewModel: " + ex);
                IsBusy = false;
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("systemSettings");
                OnPropertyChanged("userAccount");
                OnPropertyChanged("IsBusy");
            }
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
        }
    }
}
