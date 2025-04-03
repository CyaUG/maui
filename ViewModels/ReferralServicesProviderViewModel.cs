using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.ReferralProgram;
using Youth.Views.Shopping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Youth.ViewModels.Base;

namespace Youth.ViewModels
{
    [QueryProperty(nameof(ReferralId), nameof(ReferralId))]
    internal class ReferralServicesProviderViewModel : BaseViewModel, IReferralServicesProviderViewModel
    {
        private int _referralId;
        //public Command LoadReferralServicesCommand { get; }
        public ObservableCollection<ReferralService> ReferralServices { get; }
        public ReferralServicesProviderViewModel()
        {
            Title = "Referral Services";
            ReferralServices = new ObservableCollection<ReferralService>();
            //LoadReferralServicesCommand = new Command(async () => await LoadReferralServices(ReferralId));
            _ = LoadReferralServices(ReferralId);
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public int ReferralId
        {
            get
            {
                return _referralId;
            }
            set
            {
                _referralId = value;
                LoadReferralServices(value);
            }
        }

        public string _searchText;
        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                SearchReferralServices(value);
            }
        }

        public ReferralService _referralService;
        public ReferralService mReferralService
        {
            get
            {
                return _referralService;
            }
            set
            {
                _referralService = value;
                SubmitReferralOfferedService(value);
            }
        }

        public async Task SearchReferralServices(string searchText)
        { }

        public async void SubmitReferralOfferedService(ReferralService mReferralService)
        {
            try
            {
                await ReferralService.SubmitReferralOfferedService(mReferralService.active, ReferralId, mReferralService.id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ReferralServicesProviderViewModel: " + ex);
            }
        }

        public async Task LoadReferralServices(int referralId)
        {
            Debug.WriteLine("ReferralServicesProviderViewModel: LoadReferralDetails()");
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

                JObject servicesObj = await ReferralService.fetchAllReferralServices(referralId);
                JArray servicesArray = (JArray)servicesObj.GetValue("data");
                ReferralServices.Clear();

                foreach (JToken token in servicesArray)
                {
                    JObject chatObj = (JObject)token;
                    ReferralService referralService = chatObj.ToObject<ReferralService>(serializer);
                    Debug.WriteLine("ReferralServicesProviderViewModel: " + referralService.label + ", Active: " + referralService.active);
                    ReferralServices.Add(referralService);
                }
                OnPropertyChanged("ReferralServices");

                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ReferralServicesProviderViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
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
