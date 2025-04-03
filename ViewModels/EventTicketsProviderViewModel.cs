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
    internal class EventTicketsProviderViewModel : BaseViewModel, IEventTicketsProviderViewModel
    {
        private int eventId;
        public SystemSettings systemSettings { get; set; }
        public UserAccount userAccount { get; set; }
        public Event EventDetails { get; set; }
        public ObservableCollection<EventTicketModule> EventTickets { get; }
        public Command<EventTicketModule> EventTicketsTappedCommand { get; }
        //public Command EventDetailsLoadCommand { get; }

        public EventTicketsProviderViewModel()
        {
            Title = "Event Tickets";
            EventTickets = new ObservableCollection<EventTicketModule>();
            //EventDetailsLoadCommand = new Command(async () => LoadEventDetails(eventId));
            EventTicketsTappedCommand = new Command<EventTicketModule>(ExecuteEventTicketsTappedCommand);
            LoadEventDetails(eventId);
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
        public async void ExecuteEventTicketsTappedCommand(EventTicketModule eventTicketModule)
        {
            if (eventTicketModule == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            MessagingCenter.Send<EventTicketsProviderViewModel, EventTicketModule>(this, "eventTicketModule", eventTicketModule);
            GoBack();
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
        public async void LoadEventDetails(int mEventId)
        {
            Debug.WriteLine("EventTicketsProviderViewModel: ExecuteShoppingCategoryCommand()");
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
                Debug.WriteLine("EventTicketsProviderViewModel: " + systemSettings.currency);
                OnPropertyChanged("systemSettings");

                JObject EventObj = await Event.fetchEventDetails(mEventId);
                JObject productObj = (JObject)EventObj.GetValue("data");
                EventDetails = productObj.ToObject<Event>(serializer);
                OnPropertyChanged("EventDetails");

                JObject serverObj = await EventTicketModule.fetchEventTickets(mEventId);
                JArray categoriesArray = (JArray)serverObj.GetValue("data");
                EventTickets.Clear();

                foreach (JToken token in categoriesArray)
                {
                    JObject attendeeObj = (JObject)token;
                    EventTicketModule eventTicket = attendeeObj.ToObject<EventTicketModule>(serializer);
                    eventTicket.offerDiscount = !eventTicket.onDiscount;
                    eventTicket.currency = systemSettings.currency;
                    EventTickets.Add(eventTicket);
                }
                OnPropertyChanged("EventTickets");

                JObject accObj = await UserAccount.LoadMyProfileAsync();
                userAccount = accObj.ToObject<UserAccount>(serializer);
                OnPropertyChanged("userAccount");

                IsBusy = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EventTicketsProviderViewModel: " + ex);
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
