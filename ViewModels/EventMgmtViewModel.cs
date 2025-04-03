using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.Events;
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
    [QueryProperty(nameof(EventId), nameof(EventId))]
    internal class EventMgmtViewModel : BaseViewModel, IEventMgmtViewModel
    {
        public ObservableCollection<UserAccount> eventManagement { get; }
        //public Command EventDetailsLoadCommand { get; }
        public SystemSettings systemSettings { get; set; }
        public UserAccount userAccount { get; set; }
        public Event EventDetails { get; set; }
        public EventMgmtViewModel()
        {
            Title = "Management";
            eventManagement = new ObservableCollection<UserAccount>();
            //EventDetailsLoadCommand = new Command(async () => LoadEventDetails(eventId));
            LoadEventDetails(eventId);
        }

        private int eventId;
        public int EventId
        {
            get
            {
                return eventId;
            }
            set
            {
                eventId = value;
                LoadEventDetails(value);
            }
        }

        private ContactModule _contactModule;
        public ContactModule mContactModule
        {
            get
            {
                return _contactModule;
            }
            set
            {
                _contactModule = value;
                SubmitNewManager(value);
            }
        }

        public async void SubmitNewManager(ContactModule contactModule)
        {
            //add new contact
            try
            {
                await Event.submitEventManager(contactModule.friendId, EventId);
                IsBusy = true;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MainShoppingViewModel: " + ex);
                IsBusy = false;
            }
        }

        public async void LoadEventDetails(int mEventId)
        {
            Debug.WriteLine("MainShoppingViewModel: ExecuteShoppingCategoryCommand()");
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

                JObject EventObj = await Event.fetchEventDetails(mEventId);
                JObject productObj = (JObject)EventObj.GetValue("data");
                EventDetails = productObj.ToObject<Event>(serializer);
                OnPropertyChanged("EventDetails");

                JObject serverObj = await Event.fetchEventManagement(mEventId);
                JArray managementArray = (JArray)serverObj.GetValue("data");
                eventManagement.Clear();

                foreach (JToken token in managementArray)
                {
                    JObject attendeeObj = (JObject)token;
                    UserAccount userAccount = attendeeObj.ToObject<UserAccount>(serializer);
                    eventManagement.Add(userAccount);
                }
                OnPropertyChanged("eventAttendees");

                JObject accObj = await UserAccount.LoadMyProfileAsync();
                userAccount = accObj.ToObject<UserAccount>(serializer);
                OnPropertyChanged("userAccount");

                IsBusy = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MainShoppingViewModel: " + ex);
                IsBusy = false;
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("PopularProducts");
                OnPropertyChanged("shoppingCategories");
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
