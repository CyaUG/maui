using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.Account;
using Youth.Views.jobs;
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
    internal class SearchUsersViewModel : BaseViewModel, ISearchUsersViewModel
    {
        public ObservableCollection<UserAccount> UserAccounts { get; }
        public Command UsersLoadCommand { get; }
        public Command<UserAccount> AccountNavTap { get; }
        public SystemSettings systemSettings { get; set; }
        public UserAccount userAccount { get; set; }

        public SearchUsersViewModel()
        {
            Title = "Search Users";
            UserAccounts = new ObservableCollection<UserAccount>();
            UsersLoadCommand = new Command(async () => FilterUsers());
            AccountNavTap = new Command<UserAccount>(ExecuteContactNavTap);
        }

        private async void ExecuteContactNavTap(UserAccount userAccount)
        {
            if (userAccount == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(UserProfilePage)}?{nameof(UserProfileViewModel.UserId)}={userAccount.id}");
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged("SearchText");
                FilterUsers();
            }
        }
        public async void FilterUsers()
        {
            Debug.WriteLine("ContactsProviderViewModel: LoadMyContacts()");
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
                Debug.WriteLine("ContactsProviderViewModel: " + systemSettings.currency);
                OnPropertyChanged("systemSettings");

                JObject accObj = await UserAccount.LoadMyProfileAsync();
                userAccount = accObj.ToObject<UserAccount>(serializer);
                OnPropertyChanged("userAccount");

                if (SearchText.Length > 1)
                {
                    JObject serverObj = await UserAccount.SearchUsers(SearchText);
                    JArray managementArray = (JArray)serverObj.GetValue("data");
                    UserAccounts.Clear();

                    foreach (JToken token in managementArray)
                    {
                        JObject attendeeObj = (JObject)token;
                        UserAccount contactModule = attendeeObj.ToObject<UserAccount>(serializer);
                        UserAccounts.Add(contactModule);
                    }
                    OnPropertyChanged("userContacts");
                }
                else
                {
                    JObject serverObj = await UserAccount.getContactsUsers();
                    JArray managementArray = (JArray)serverObj.GetValue("data");
                    UserAccounts.Clear();

                    foreach (JToken token in managementArray)
                    {
                        JObject attendeeObj = (JObject)token;
                        UserAccount contactModule = attendeeObj.ToObject<UserAccount>(serializer);
                        UserAccounts.Add(contactModule);
                    }
                    OnPropertyChanged("userContacts");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ContactsProviderViewModel: " + ex);
                IsBusy = false;
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
