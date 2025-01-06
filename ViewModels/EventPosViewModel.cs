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
using Youth.ViewModels.Base;
using System.Windows.Input;



namespace Youth.ViewModels
{
    [QueryProperty(nameof(EventId), nameof(EventId))]
    internal class EventPosViewModel : BaseViewModel, IEventPosViewModel
    {
        private int eventId;
        public SystemSettings systemSettings { get; set; }
        public UserAccount userAccount { get; set; }
        public Event EventDetails { get; set; }
        public ObservableCollection<EventTicketToken> eventTicketTokens { get; }
        public Command EventDetailsLoadCommand { get; }
        public Command<EventTicketToken> UseEventTicketCommand { get; }

        public EventPosViewModel()
        {
            Title = "Event POS";
            isTicketOpen = false;
            eventTicketTokens = new ObservableCollection<EventTicketToken>();
            EventDetailsLoadCommand = new Command(async () => LoadEventDetails(eventId));
            UseEventTicketCommand = new Command<EventTicketToken>(ExecuteUseEventTicketCommand);
        }

        public bool _isTicketOpen;
        public bool isTicketOpen
        {
            get
            {
                return _isTicketOpen;
            }
            set
            {
                _isTicketOpen = value;
                OnPropertyChanged("isTicketOpen");
            }
        }

        public bool _isRunning;
        public bool isRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                _isRunning = value;
                OnPropertyChanged("isRunning");
            }
        }

        private string _tocken;
        public string Tocken
        {
            get
            {
                return _tocken;
            }
            set
            {
                _tocken = value;
                if (!isTicketOpen)
                {
                    isTicketOpen = true;
                    LoadTicketDetails(value);
                }
            }
        }
        private EventTicketToken _eventTicketToken;
        public EventTicketToken mEventTicketToken
        {
            get
            {
                return _eventTicketToken;
            }
            set
            {
                _eventTicketToken = value;
                OnPropertyChanged("mEventTicketToken");
                MessagingCenter.Send<EventPosViewModel, bool>(this, "showTicket", true);
            }
        }

        public async void LoadTicketDetails(string token)
        {
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

                isTicketOpen = true;
                isRunning = true;
                JObject serverObj = await EventTicketToken.fetchEventTicketInfo(token, EventId);
                JObject EventTicketTokenObj = (JObject)serverObj.GetValue("data");
                mEventTicketToken = EventTicketTokenObj.ToObject<EventTicketToken>(serializer);
                isRunning = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MainShoppingViewModel: " + ex);
            }
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

        public async void ExecuteUseEventTicketCommand(EventTicketToken eventTicketToken)
        {
            Debug.WriteLine("MainShoppingViewModel: ExecuteUseEventTicketCommand()");

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

                isRunning = true;
                bool isTicketUsed = await EventTicketToken.useEventTicket(eventTicketToken.tocken, EventId);
                isRunning = false;
                if (isTicketUsed)
                {
                    MessagingCenter.Send<EventPosViewModel, string>(this, "message", "Ticket has been used");
                }
                else
                {
                    MessagingCenter.Send<EventPosViewModel, string>(this, "message", "Something Went wrong, try again.");
                }

                JObject serverObj = await EventTicketToken.fetchEventTicketInfo(eventTicketToken.tocken, EventId);
                JObject EventTicketTokenObj = (JObject)serverObj.GetValue("data");
                mEventTicketToken = EventTicketTokenObj.ToObject<EventTicketToken>(serializer);
                OnPropertyChanged("mEventTicketToken");
                LoadEventDetails(EventId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MainShoppingViewModel: " + ex);
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

                JObject serverObj = await EventTicketToken.fetchEventScannedTicketTokens(mEventId);
                JArray eventTicketTokenArray = (JArray)serverObj.GetValue("data");
                eventTicketTokens.Clear();

                foreach (JToken token in eventTicketTokenArray)
                {
                    JObject attendeeObj = (JObject)token;
                    EventTicketToken eventTicketToken = attendeeObj.ToObject<EventTicketToken>(serializer);
                    eventTicketToken.currency = systemSettings.currency;
                    eventTicketTokens.Add(eventTicketToken);
                }
                OnPropertyChanged("eventTicketTokens");

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
        public void OnBarcodeDetected(string tocken) {
            Tocken=tocken;
        }
        public void OnTockenStatusUpdate(bool ticketOpen)
        {
            isTicketOpen = ticketOpen;
        }
    }
}
