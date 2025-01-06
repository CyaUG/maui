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
    internal class EventOrderTicetsViewModel : BaseViewModel, IEventOrderTicetsViewModel
    {
        private int _eventId;
        public SystemSettings systemSettings { get; set; }
        public UserAccount userAccount { get; set; }
        public ObservableCollection<EventTicketToken> EventTicketTokens { get; }
        public Command EventOrderTicetsLoadCommand { get; }
        public Command<EventTicketToken> EventTicketTokenTapCommand { get; }

        public EventOrderTicetsViewModel()
        {
            Title = "Order Tickets";
            EventTicketTokens = new ObservableCollection<EventTicketToken>();
            EventOrderTicetsLoadCommand = new Command(async () => LoadEventTickets(EventId));
            EventTicketTokenTapCommand = new Command<EventTicketToken>(OnEventSelected);
        }

        async void OnEventSelected(EventTicketToken mEventTicketToken)
        {
            if (mEventTicketToken == null)
                return;

            // This will push the TiketDetailsPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(TicketDetailsPage)}?{nameof(TicketDetailsViewModel.TicketCode)}={mEventTicketToken.tocken}&{nameof(TicketDetailsViewModel.EventId)}={EventId}");
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
                LoadEventTickets(value);
            }
        }

        public async void LoadEventTickets(int eventId)
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

                JObject tockensObj = await EventTicketToken.fetchEventTiketTockets(eventId);
                JArray tokensArray = (JArray)tockensObj.GetValue("data");
                EventTicketTokens.Clear();

                foreach (JToken token in tokensArray)
                {
                    JObject attendeeObj = (JObject)token;
                    EventTicketToken eventTicketToken = attendeeObj.ToObject<EventTicketToken>(serializer);
                    eventTicketToken.currency = systemSettings.currency;
                    eventTicketToken.offerDiscount = !eventTicketToken.onDiscount;
                    EventTicketTokens.Add(eventTicketToken);
                }
                OnPropertyChanged("EventTicketTokens");

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
