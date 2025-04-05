using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.ReferralProgram;
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
    internal class ReferralAccountProviderViewModel : BaseViewModel, IReferralAccountProviderViewModel
    {
        //public Command LoadReferralAccountsCommand { get; set; }
        private string searchText;
        public ObservableCollection<ReferralAccount> ReferralAccounts { get; set; }
        public Command<ReferralAccount> ReferralAccountNavTap { get; }
        public Command ScanReferralCardPageCommand { get; }
        public Command CreateNewReferralAccountCommand { get; }
        public ReferralAccountProviderViewModel()
        {
            Title = "Referral Accounts";
            ReferralAccounts = new ObservableCollection<ReferralAccount>();
            //LoadReferralAccountsCommand = new Command(async () => await FilterReferralAccounts());
            ReferralAccountNavTap = new Command<ReferralAccount>(OnReferralAccountSelected);
            ScanReferralCardPageCommand = new Command(async () => await OpenScanReferralCardPage());
            CreateNewReferralAccountCommand = new Command(async () => await ExecuteCreateNewReferralAccountPage());
            _ = FilterReferralAccounts();
        }

        async Task OpenScanReferralCardPage()
        {
            await Shell.Current.GoToAsync($"{nameof(ScanReferralCardPage)}");
        }

        async Task ExecuteCreateNewReferralAccountPage()
        {
            await Shell.Current.GoToAsync($"{nameof(CreateNewReferralAccountPage)}");
        }

        async void OnReferralAccountSelected(ReferralAccount referralAccount)
        {
            if (referralAccount == null)
                return;
            //open create referral page
            await Shell.Current.GoToAsync($"{nameof(CreateNewReferralPage)}?{nameof(CreateNewReferralViewModel.Token)}={referralAccount.token}");
        }

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged("SearchText");
                FilterReferralAccounts();
            }
        }
        async Task FilterReferralAccounts()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                IsBusy = false;
                OnPropertyChanged("IsBusy");
                return;
            }

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

                JObject sccObj = await ReferralAccount.SearchReferralAccounts(SearchText);
                ReferralAccounts.Clear();
                JArray accArray = (JArray)sccObj.GetValue("data");

                foreach (JToken token in accArray)
                {
                    JObject accoountObj = (JObject)token;
                    ReferralAccount mReferralAccount = accoountObj.ToObject<ReferralAccount>(serializer);
                    Debug.WriteLine("ReferralAccountProviderViewModel: " + mReferralAccount.name);
                    ReferralAccounts.Add(mReferralAccount);
                }
                OnPropertyChanged("ReferralAccounts");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ReferralAccountProviderViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("ReferralAccounts");
                OnPropertyChanged("IsBusy");
            }
        }
        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
            /*Set defaults*/
        }
    }
}
