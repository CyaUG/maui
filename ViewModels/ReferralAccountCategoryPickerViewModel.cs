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
    internal class ReferralAccountCategoryPickerViewModel : BaseViewModel, IReferralAccountCategoryPickerViewModel
    {
        //public Command LoadReferralAccountCategories { get; }
        public ObservableCollection<ReferralAccountCategory> ReferralAccountCategories { get; set; }
        public Command<ReferralAccountCategory> ReferralAccountCategoryNavTap { get; }

        public ReferralAccountCategoryPickerViewModel()
        {
            Title = "Account Categories";
            ReferralAccountCategories = new ObservableCollection<ReferralAccountCategory>();
            //LoadReferralAccountCategories = new Command(async () => await ExecuteLoadReferralAccountCategoriesCommand());
            ReferralAccountCategoryNavTap = new Command<ReferralAccountCategory>(OnReferralAccountCategorySelected);
            _ = ExecuteLoadReferralAccountCategoriesCommand();
        }

        async void OnReferralAccountCategorySelected(ReferralAccountCategory referralAccountCategory)
        {
            if (referralAccountCategory == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            MessagingCenter.Send<ReferralAccountCategoryPickerViewModel, ReferralAccountCategory>(this, "referralAccountCategory", referralAccountCategory);
            GoBack();
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        async Task ExecuteLoadReferralAccountCategoriesCommand()
        {
            Debug.WriteLine("ReferralAccountCategoryPickerViewModel: ExecuteLoadShoppingWishListCommand()");
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

                JObject postsServerObj = await ReferralAccountCategory.fetchReferralAccountCategories();
                ReferralAccountCategories.Clear();
                JArray chatsArray = (JArray)postsServerObj.GetValue("data");

                foreach (JToken token in chatsArray)
                {
                    JObject chatObj = (JObject)token;
                    ReferralAccountCategory referral_account_category = chatObj.ToObject<ReferralAccountCategory>(serializer);
                    Debug.WriteLine("ReferralAccountCategoryPickerViewModel: " + referral_account_category.label);
                    ReferralAccountCategories.Add(referral_account_category);
                }
                OnPropertyChanged("ReferralAccountCategories");
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
                OnPropertyChanged("ReferralAccountCategories");
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
