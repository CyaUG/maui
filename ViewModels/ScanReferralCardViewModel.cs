using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.ReferralProgram;
using Youth.Views.Shopping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Youth.ViewModels.Base;



namespace Youth.ViewModels
{
    public class ScanReferralCardViewModel : BaseViewModel, IScanReferralCardViewModel
    {
        public ReferralAccount referralAccount { get; set; }
        public bool showAccountDetails { get; set; }
        private string _tocken;
        private string activeToken { get; set; }
        public Command ChooseReferralAccountCommand { get; set; }
        public ScanReferralCardViewModel()
        {
            Title = "Scan Referral Card";
            showAccountDetails = false;
            ChooseReferralAccountCommand = new Command(async () => ExecuteChooseReferralAccountCommand());
        }
        public string Tocken
        {
            get
            {
                return _tocken;
            }
            set
            {
                _tocken = value;
                LoadAccountDetails(value);
            }
        }
        async void ExecuteChooseReferralAccountCommand()
        {
            if (activeToken == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(CreateNewReferralPage)}?{nameof(CreateNewReferralViewModel.Token)}={activeToken}");
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task LoadAccountDetails(string tocken)
        {
            Debug.WriteLine("ShoppingOrderProductsViewModel: ExecuteLoadShoppingOrdersCommand()");
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

                JObject serverObj = await ReferralAccount.fetchReferralAccountByTocken(tocken);
                try
                {
                    JObject refAccJsonObj = (JObject)serverObj.GetValue("data");
                    referralAccount = refAccJsonObj.ToObject<ReferralAccount>(serializer);
                    showAccountDetails = true;
                    OnPropertyChanged("referralAccount");
                    OnPropertyChanged("showAccountDetails");
                    activeToken = tocken;
                    OnPropertyChanged("activeToken");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("MainReferralViewModel Err: " + ex);
                }

                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ShoppingOrderProductsViewModel: " + ex);
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
