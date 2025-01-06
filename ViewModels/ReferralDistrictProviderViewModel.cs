using Youth.Models;
using Youth.ViewModels.Interfaces;
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
    internal class ReferralDistrictProviderViewModel : BaseViewModel, IReferralDistrictProviderViewModel
    {
        public Command LoadReferralDistrictsCommand { get; }
        public ObservableCollection<ReferralDistrict> ReferralDistricts { get; set; }
        public Command<ReferralDistrict> ReferralDistrictNavTap { get; }
        public ReferralDistrictProviderViewModel()
        {
            Title = "Referral Districts";
            ReferralDistricts = new ObservableCollection<ReferralDistrict>();
            LoadReferralDistrictsCommand = new Command(async () => await LoadReferralDistrict());
            ReferralDistrictNavTap = new Command<ReferralDistrict>(OnReferralDistrictSelected);
        }

        async void OnReferralDistrictSelected(ReferralDistrict referralDistrict)
        {
            if (referralDistrict == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            MessagingCenter.Send<ReferralDistrictProviderViewModel, ReferralDistrict>(this, "referralDistrict", referralDistrict);
            GoBack();
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task LoadReferralDistrict()
        {
            Debug.WriteLine("ReferralDistrictProviderViewModel: ExecuteLoadShoppingWishListCommand()");
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

                JObject postsServerObj = await ReferralDistrict.fetchAllReferralDistricts();
                ReferralDistricts.Clear();
                JArray chatsArray = (JArray)postsServerObj.GetValue("data");

                foreach (JToken token in chatsArray)
                {
                    JObject chatObj = (JObject)token;
                    ReferralDistrict referralDistrict = chatObj.ToObject<ReferralDistrict>(serializer);
                    Debug.WriteLine("ReferralDistrictProviderViewModel: " + referralDistrict.label);
                    ReferralDistricts.Add(referralDistrict);
                }
                OnPropertyChanged("ReferralDistricts");
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ReferralAccountCategoryPickerViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("ReferralDistricts");
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
