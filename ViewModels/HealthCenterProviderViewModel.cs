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
    internal class HealthCenterProviderViewModel : BaseViewModel, IHealthCenterProviderViewModel
    {
        //public Command RunReferralHCMainCommand { get; }
        public ObservableCollection<HealthCenter> HealthCenters { get; }
        public Command<HealthCenter> HealthCenterNavTap { get; }
        public HealthCenterProviderViewModel()
        {
            Title = "Health Centers";
            HealthCenters = new ObservableCollection<HealthCenter>();
            //RunReferralHCMainCommand = new Command(async () => await ExecuteReferralHCMainCommand());
            HealthCenterNavTap = new Command<HealthCenter>(OnHealthCenterSelected);
            _ = ExecuteReferralHCMainCommand();
        }

        async void OnHealthCenterSelected(HealthCenter healthCenter)
        {
            if (healthCenter == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            MessagingCenter.Send<HealthCenterProviderViewModel, HealthCenter>(this, "healthCenter", healthCenter);
            GoBack();
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task ExecuteReferralHCMainCommand()
        {
            Debug.WriteLine("HealthCenterProviderViewModel: ExecuteReferralHCMainCommand()");
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

                JObject hCentersObj = await HealthCenter.fetchAllHealthCenters();
                JArray hCentersArray = (JArray)hCentersObj.GetValue("data");
                HealthCenters.Clear();

                foreach (JToken token in hCentersArray)
                {
                    JObject chatObj = (JObject)token;
                    HealthCenter healthCenter = chatObj.ToObject<HealthCenter>(serializer);
                    Debug.WriteLine("HealthCenterProviderViewModel: " + healthCenter.label);
                    HealthCenters.Add(healthCenter);
                }
                OnPropertyChanged("HealthCenters");

                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("HealthCenterProviderViewModel: " + ex);
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
