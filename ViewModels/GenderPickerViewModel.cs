using Youth.Models;
using Youth.ViewModels.Interfaces;
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
    internal class GenderPickerViewModel : BaseViewModel, IGenderPickerViewModel
    {
        //public Command LoadGenders { get; }
        public ObservableCollection<Gender> Genders { get; set; }
        public Command<Gender> GenderNavTap { get; }

        public GenderPickerViewModel()
        {
            Title = "Genders";
            Genders = new ObservableCollection<Gender>();
            //LoadGenders = new Command(async () => await ExecuteLoadReferralAccountCategoriesCommand());
            GenderNavTap = new Command<Gender>(OnGenderSelected);
            _ = ExecuteLoadReferralAccountCategoriesCommand();
        }

        async void OnGenderSelected(Gender gender)
        {
            if (gender == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            MessagingCenter.Send<GenderPickerViewModel, Gender>(this, "gender", gender);
            GoBack();
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        async Task ExecuteLoadReferralAccountCategoriesCommand()
        {
            Debug.WriteLine("GenderPickerViewModel: ExecuteLoadShoppingWishListCommand()");
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

                JObject postsServerObj = await Gender.fetchGenders();
                Genders.Clear();
                JArray chatsArray = (JArray)postsServerObj.GetValue("data");

                foreach (JToken token in chatsArray)
                {
                    JObject chatObj = (JObject)token;
                    Gender referral_account_category = chatObj.ToObject<Gender>(serializer);
                    Debug.WriteLine("GenderPickerViewModel: " + referral_account_category.label);
                    Genders.Add(referral_account_category);
                }
                OnPropertyChanged("Genders");
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GenderPickerViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("Genders");
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
