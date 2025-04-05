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
    internal class EventConfigViewModel : BaseViewModel, IEventConfigViewModel
    {
        private int eventId;
        public SystemSettings systemSettings { get; set; }
        public UserAccount userAccount { get; set; }
        public Event EventDetails { get; set; }
        public ObservableCollection<UserAccount> eventAttendees { get; }
        //public Command EventDetailsLoadCommand { get; }
        public Command OpenEventApplicationPage { get; }
        public Command<Event> EventAddressTap { get; }
        public Command OpemEventMgmtPageCommand { get; }
        public Command OpemEventPosPageCommand { get; }

        public EventConfigViewModel()
        {
            Title = "Event Info";
            eventAttendees = new ObservableCollection<UserAccount>();
            //EventDetailsLoadCommand = new Command(async () => LoadEventDetails(eventId));
            OpenEventApplicationPage = new Command(async () => OnOpenEventApplicationPage());
            EventAddressTap = new Command<Event>(OnEventAddressSelected);
            OpemEventMgmtPageCommand = new Command(async () => OnOpemEventMgmtPage());
            OpemEventPosPageCommand = new Command(async () => OnOpemEventPosPage());
            LoadEventDetails(eventId);
        }

        async void OnEventAddressSelected(Event mEvent)
        {
            if (mEvent == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            Debug.WriteLine("JobDetailsViewModel: OnEventAddressSelected()");
            await Map.OpenAsync(mEvent.event_latitude, mEvent.event_longitude, new MapLaunchOptions { Name = mEvent.event_address });
        }

        async void OnOpenEventApplicationPage()
        {
            await Shell.Current.GoToAsync($"{nameof(EventApplicationPage)}?{nameof(EventApplicationViewModel.EventId)}={EventId}");
        }

        async void OnOpemEventMgmtPage()
        {
            await Shell.Current.GoToAsync($"{nameof(EventMgmtPage)}?{nameof(EventMgmtViewModel.EventId)}={EventId}");
        }

        async void OnOpemEventPosPage()
        {
            await Shell.Current.GoToAsync($"{nameof(EventPosPage)}?{nameof(EventPosViewModel.EventId)}={EventId}");
        }

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

                JObject serverObj = await Event.fetchEventAttendees(mEventId);
                JArray categoriesArray = (JArray)serverObj.GetValue("data");
                eventAttendees.Clear();

                foreach (JToken token in categoriesArray)
                {
                    JObject attendeeObj = (JObject)token;
                    UserAccount userAccount = attendeeObj.ToObject<UserAccount>(serializer);
                    eventAttendees.Add(userAccount);
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
