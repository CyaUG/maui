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
    internal class EventOrderDetailsViewModel : BaseViewModel, IEventOrderDetailsViewModel
    {
        private int _eventId;
        public SystemSettings systemSettings { get; set; }
        public UserAccount userAccount { get; set; }
        public EventsOrder eventsOrder { get; set; }
        public Event EventDetails { get; set; }
        public ObservableCollection<UserAccount> eventAttendees { get; }
        public ObservableCollection<EventTicketToken> EventTicketTokens { get; }
        public Command EventDetailsLoadCommand { get; }
        public Command LoadEventOrderTicetsCommand { get; }
        public Command<Event> EventAddressTap { get; }

        public EventOrderDetailsViewModel()
        {
            Title = "Event Info";
            eventAttendees = new ObservableCollection<UserAccount>();
            EventTicketTokens = new ObservableCollection<EventTicketToken>();
            EventDetailsLoadCommand = new Command(async () => LoadEventDetails(EventId));
            LoadEventOrderTicetsCommand = new Command(async () => LoadEventOrderTicetsPage());
            EventAddressTap = new Command<Event>(OnEventAddressSelected);
        }

        async void OnEventAddressSelected(Event mEvent)
        {
            if (mEvent == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            Debug.WriteLine("EventOrderDetailsViewModel: OnEventAddressSelected()");
            await Map.OpenAsync(mEvent.event_latitude, mEvent.event_longitude, new MapLaunchOptions { Name = mEvent.event_address });
        }

        public int EventId
        {
            get
            {
                return _eventId;
            }
            set
            {
                _eventId = value;
                LoadEventDetails(value);
            }
        }

        public async void LoadEventOrderTicetsPage()
        {
            await Shell.Current.GoToAsync($"{nameof(EventOrderTicetsPage)}?{nameof(EventOrderTicetsViewModel.EventId)}={EventId}");
        }

        public async void LoadEventDetails(int eventId)
        {
            Debug.WriteLine("EventOrderDetailsViewModel: ExecuteShoppingCategoryCommand()");
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
                Debug.WriteLine("EventOrderDetailsViewModel: " + systemSettings.currency);
                OnPropertyChanged("systemSettings");

                JObject EventObj = await Event.fetchEventDetails(eventId);
                JObject productObj = (JObject)EventObj.GetValue("data");
                EventDetails = productObj.ToObject<Event>(serializer);
                OnPropertyChanged("EventDetails");

                JObject tockensObj = await EventTicketToken.fetchEventTiketTockets(eventId);
                JArray tokensArray = (JArray)tockensObj.GetValue("data");
                EventTicketTokens.Clear();

                foreach (JToken token in tokensArray)
                {
                    JObject attendeeObj = (JObject)token;
                    EventTicketToken eventTicketToken = attendeeObj.ToObject<EventTicketToken>(serializer);
                    EventTicketTokens.Add(eventTicketToken);
                }
                OnPropertyChanged("EventTicketTokens");

                JObject serverObj = await Event.fetchEventAttendees(eventId);
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
                Debug.WriteLine("EventOrderDetailsViewModel: " + ex);
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
