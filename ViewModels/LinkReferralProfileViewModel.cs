using Youth.Models;
using Youth.ViewModels.Interfaces;
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
    internal class LinkReferralProfileViewModel : BaseViewModel, ILinkReferralProfileViewModel
    {
        public ReferralAccount referralAccount { get; set; }
        public bool showAccountDetails { get; set; }
        private string _tocken;
        private string activeToken { get; set; }
        public Command SubmitReferralAccountRequestCommand { get; set; }
        public LinkReferralProfileViewModel()
        {
            Title = "Link Profile";
            showAccountDetails = false;
            SubmitReferralAccountRequestCommand = new Command(async () => await ExecuteSubmitReferralAccountRequestCommand());
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
                LoadSymptomDetails(value);
            }
        }
        async Task ExecuteSubmitReferralAccountRequestCommand()
        {
            MessagingCenter.Send<LinkReferralProfileViewModel, bool>(this, "isSeekerRunning", true);
            HttpResponseMessage response = await ReferralAccount.submitReferralAccountRequest(activeToken);
            int statusCode = (int)response.StatusCode;

            if (statusCode == 200)
            {
                GoBack();
                //MessagingCenter.Send<LinkReferralProfileViewModel, string>(this, "message", "Your Request Is Submitted");
            }
            else if (statusCode == 201)
            {
                MessagingCenter.Send<LinkReferralProfileViewModel, string>(this, "message", "The account you requested is in use");
                MessagingCenter.Send<LinkReferralProfileViewModel, bool>(this, "isSeekerRunning", false);
            }
            else
            {
                MessagingCenter.Send<LinkReferralProfileViewModel, string>(this, "message", "Something went wrong");
                MessagingCenter.Send<LinkReferralProfileViewModel, bool>(this, "isSeekerRunning", false);
            }
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task LoadSymptomDetails(string tocken)
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
        public void OnBarcodeDetected(string tocken)
        {
            Tocken = tocken;
        }
    }
}
