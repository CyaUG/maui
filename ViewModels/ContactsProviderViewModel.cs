using Youth.Models;
using Youth.ViewModels.Interfaces;
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
    internal class ContactsProviderViewModel : BaseViewModel, IContactsProviderViewModel
    {
        public ObservableCollection<ContactModule> userContacts { get; }
        //public Command ContactsLoadCommand { get; }
        public Command<ContactModule> ContactNavTap { get; }
        public SystemSettings systemSettings { get; set; }
        public UserAccount userAccount { get; set; }

        public ContactsProviderViewModel()
        {
            Title = "My Contacts";
            userContacts = new ObservableCollection<ContactModule>();
            //ContactsLoadCommand = new Command(async () => LoadMyContacts());
            ContactNavTap = new Command<ContactModule>(ExecuteContactNavTap);
            _ = LoadMyContacts();
        }

        public void ExecuteContactNavTap(ContactModule contactModule)
        {
            if (contactModule == null)
                return;

            MessagingCenter.Send<ContactsProviderViewModel, ContactModule>(this, "contactModule", contactModule);
            GoBack();
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
        public async Task LoadMyContacts()
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

                JObject serverObj = await ContactModule.LoadUserContacts();
                JArray managementArray = (JArray)serverObj.GetValue("data");
                userContacts.Clear();

                foreach (JToken token in managementArray)
                {
                    JObject attendeeObj = (JObject)token;
                    ContactModule contactModule = attendeeObj.ToObject<ContactModule>(serializer);
                    userContacts.Add(contactModule);
                }
                OnPropertyChanged("userContacts");
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
