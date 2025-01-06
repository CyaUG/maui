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
    internal class EventApplicationViewModel : BaseViewModel, IEventApplicationViewModel
    {
        private int eventId;
        public int cartCount { get; set; }
        public double priceToPay { get; set; }
        public SystemSettings systemSettings { get; set; }
        public UserAccount userAccount { get; set; }
        public Event EventDetails { get; set; }
        public ObservableCollection<UserAccount> eventAttendees { get; }
        public Command EventDetailsLoadCommand { get; }
        public Command OpenEventTicketsProviderCommand { get; }
        public Command IncrementCartCommand { get; }
        public Command DecrementCartCommand { get; }
        public Command BuyMowCommand { get; }
        public Command AddToCartCommand { get; }

        public EventApplicationViewModel()
        {
            Title = "Apply Now";
            eventAttendees = new ObservableCollection<UserAccount>();
            EventDetailsLoadCommand = new Command(async () => LoadEventDetails(eventId));
            OpenEventTicketsProviderCommand = new Command(async () => OnOpenEventTicketsProviderCommand());
            IncrementCartCommand = new Command(async () => ExecuteIncrementCartCommand());
            DecrementCartCommand = new Command(async () => ExecuteDecrementCartCommand());
            BuyMowCommand = new Command(async () => ExecuteBuyMowCommand());
            AddToCartCommand = new Command(async () => ExecuteAddToCartCommand());
        }

        async void OnOpenEventTicketsProviderCommand()
        {
            //open create referral page
            await Shell.Current.GoToAsync($"{nameof(EventTicketsProviderPage)}?{nameof(EventTicketsProviderViewModel.EventId)}={EventId}");
        }

        async Task ExecuteBuyMowCommand()
        {
            int ticketId = 0;

            if (mEventTicketModule != null)
            {
                ticketId = mEventTicketModule.id;
            }
            await EventCart.saveToEventCart(ticketId, EventId, cartCount);
            await Shell.Current.GoToAsync($"{nameof(CartEventsPage)}");
        }

        async Task ExecuteAddToCartCommand()
        {
            int ticketId = 0;

            if (mEventTicketModule != null)
            {
                ticketId = mEventTicketModule.id;
            }
            await EventCart.saveToEventCart(ticketId, EventId, cartCount);
            GoBack();
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
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

        public EventTicketModule _eventTicketModule;
        public EventTicketModule mEventTicketModule
        {
            get
            {
                return _eventTicketModule;
            }
            set
            {
                _eventTicketModule = value;
                OnPropertyChanged("mEventTicketModule");

                cartCount = _eventTicketModule.minOrder;
                OnPropertyChanged("cartCount");

                if (_eventTicketModule.onDiscount)
                {
                    priceToPay = _eventTicketModule.discountPrice * cartCount;
                }
                else
                {
                    priceToPay = _eventTicketModule.amount * cartCount;
                }
                OnPropertyChanged("priceToPay");
            }
        }

        async Task ExecuteDecrementCartCommand()
        {
            if (mEventTicketModule == null)
                return;

            if (mEventTicketModule.minOrder < cartCount)
            {
                cartCount--;
                OnPropertyChanged("cartCount");

                if (mEventTicketModule.onDiscount)
                {
                    priceToPay = mEventTicketModule.discountPrice * cartCount;
                }
                else
                {
                    priceToPay = mEventTicketModule.amount * cartCount;
                }
                OnPropertyChanged("priceToPay");
            }
        }

        async Task ExecuteIncrementCartCommand()
        {
            if (mEventTicketModule == null)
                return;

            if (mEventTicketModule.maxOrder > cartCount)
            {
                cartCount++;
                OnPropertyChanged("cartCount");

                if (mEventTicketModule.onDiscount)
                {
                    priceToPay = mEventTicketModule.discountPrice * cartCount;
                }
                else
                {
                    priceToPay = mEventTicketModule.amount * cartCount;
                }
                OnPropertyChanged("priceToPay");
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

                try
                {
                    if (mEventTicketModule == null)
                    {
                        JObject serverTicketObj = await EventTicketModule.fetchEventTickets(mEventId);
                        JArray ticketsArray = (JArray)serverTicketObj.GetValue("data");
                        EventTicketModule eventTicket = ticketsArray[0].ToObject<EventTicketModule>(serializer);
                        mEventTicketModule = eventTicket;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("MainShoppingViewModel: " + ex);
                }

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
