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
    internal class MySavedEventsViewModel : BaseViewModel, IMySavedEventsViewModel
    {
        public DashboardSummary dashboardSummary { get; set; }
        public ObservableCollection<Event> Events { get; set; }
        public Command LoadEventsCommand { get; }
        public UserAccount userAccount { get; set; }
        public SystemSettings systemSettings { get; set; }
        public Command<Event> EventNavTap { get; }
        public Command SearchGridTappedCommand { get; }
        public MySavedEventsViewModel()
        {
            Title = " My Saved Events";
            Events = new ObservableCollection<Event>();
            LoadEventsCommand = new Command(async () => await ExecuteLoadEventsCommand());
            EventNavTap = new Command<Event>(OnEventSelected);
            SearchGridTappedCommand = new Command(async () => await ExecuteSearchUiCommand());
        }

        public Event selectedEvent;
        public Event SelectedEvent
        {
            get => selectedEvent;
            set
            {
                Debug.WriteLine("MySavedEventsViewModel: SelectedChat");
                SetProperty(ref selectedEvent, value);
                OnEventSelected(value);
            }
        }

        async void OnEventSelected(Event nEvent)
        {
            if (nEvent == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(EventDetailsPage)}?{nameof(EventDetailsViewModel.EventId)}={nEvent.id}");
        }

        async Task ExecuteSearchUiCommand() { }

        async Task ExecuteLoadEventsCommand()
        {
            Debug.WriteLine("MySavedEventsViewModel: ExecuteLoadEventsCommand()");
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
                Debug.WriteLine("MySavedEventsViewModel: " + systemSettings.currency);
                OnPropertyChanged("systemSettings");

                JObject accObj = await UserAccount.LoadMyProfileAsync();
                userAccount = accObj.ToObject<UserAccount>(serializer);
                OnPropertyChanged("userAccount");

                JObject dashObj = await DashboardSummary.LoadUserDashboardSummary();
                dashboardSummary = dashObj["data"].ToObject<DashboardSummary>(serializer);
                OnPropertyChanged("dashboardSummary");

                JObject sventsServerObj = await Event.fetchMySavedEvents();
                Events.Clear();
                JArray eventssArray = (JArray)sventsServerObj.GetValue("data");

                foreach (JToken token in eventssArray)
                {
                    JObject chatObj = (JObject)token;
                    Event mEvent = chatObj.ToObject<Event>(serializer);
                    Debug.WriteLine("MySavedEventsViewModel: " + mEvent.eventTitle);
                    Events.Add(mEvent);
                }
                OnPropertyChanged("Events");
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MySavedEventsViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("eventCategories");
                OnPropertyChanged("Events");
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
