using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.Shopping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Youth.ViewModels.Base;


namespace Youth.ViewModels
{
    [QueryProperty(nameof(SymptomId), nameof(SymptomId))]
    internal class SymptomDetailsViewModel : BaseViewModel, ISymptomDetailsViewModel
    {
        private int _symptomId;
        public SystemSettings systemSettings { get; set; }
        public Symptom symptom { get; set; }
        public UserAccount userAccount { get; set; }
        public ReferralAccount referralAccount { get; set; }
        public Command LoadSymptomDetailsCommand { get; }
        public SymptomDetailsViewModel()
        {
            Title = "Symptom Info";
            LoadSymptomDetailsCommand = new Command(async () => await LoadSymptomDetails(SymptomId));
        }
        public int SymptomId
        {
            get
            {
                return _symptomId;
            }
            set
            {
                _symptomId = value;
                LoadSymptomDetails(value);
            }
        }

        public async Task LoadSymptomDetails(int symptomId)
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

                systemSettings = await SystemSettings.fetchSystemSettings();
                Debug.WriteLine("MainShoppingViewModel: " + systemSettings.currency);
                OnPropertyChanged("systemSettings");

                JObject accObj = await UserAccount.LoadMyProfileAsync();
                userAccount = accObj.ToObject<UserAccount>(serializer);
                OnPropertyChanged("userAccount");

                JObject refAccObj = await ReferralAccount.fetchMyReferralAccount();
                try
                {
                    JObject refAccJsonObj = (JObject)refAccObj.GetValue("data");
                    referralAccount = refAccJsonObj.ToObject<ReferralAccount>(serializer);
                    OnPropertyChanged("referralAccount");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("MainReferralViewModel Err: " + ex);
                }

                JObject symptomObj = await Symptom.getSymptomDetails(symptomId);
                JObject symptomJsonObj = (JObject)symptomObj.GetValue("data");
                symptom = symptomJsonObj.ToObject<Symptom>(serializer);
                OnPropertyChanged("symptom");

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
            /*Set defaults*/
        }
    }
}
