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
    [QueryProperty(nameof(TicketCode), nameof(TicketCode))]
    [QueryProperty(nameof(EventId), nameof(EventId))]
    internal class TicketDetailsViewModel : BaseViewModel, ITicketDetailsViewModel
    {
        public EventTicketToken EventTiket { get; set; }
        public Event EventDetails { get; set; }
        public SystemSettings systemSettings { get; set; }
        public TicketDetailsViewModel()
        {
            Title = "Ticket Details";
        }

        public string _ticketCode;
        public string TicketCode
        {
            get
            {
                return _ticketCode;
            }
            set
            {
                _ticketCode = value;
                OnPropertyChanged("TicketCode");
            }
        }

        public int _eventId;
        public int EventId
        {
            get
            {
                return _eventId;
            }
            set
            {
                _eventId = value;
                OnPropertyChanged("EventId");
                LoadTicketDetails(value);
            }
        }

        public async void LoadTicketDetails(int eventId)
        {
            Debug.WriteLine("TicketDetailsViewModel: LoadTicketDetails()");
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

                JObject serverObj = await EventTicketToken.fetchEventTicketInfo(TicketCode, eventId);
                JObject EventTicketTokenObj = (JObject)serverObj.GetValue("data");
                EventTiket = EventTicketTokenObj.ToObject<EventTicketToken>(serializer);
                EventTiket.offerDiscount = !EventTiket.onDiscount;
                EventTiket.currency = systemSettings.currency;
                OnPropertyChanged("EventTiket");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("TicketDetailsViewModel: " + ex);
            }
        }
        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
        }
    }
}
