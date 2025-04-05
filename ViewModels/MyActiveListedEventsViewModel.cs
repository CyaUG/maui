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
    internal class MyActiveListedEventsViewModel : BaseViewModel, IMyActiveListedEventsViewModel
    {
        public DashboardSummary dashboardSummary { get; set; }
        public ObservableCollection<Event> Events { get; set; }
        //public Command LoadEventsCommand { get; }
        public UserAccount userAccount { get; set; }
        public SystemSettings systemSettings { get; set; }
        public Command<Event> EventNavTap { get; }
        public Command SearchGridTappedCommand { get; }
        public MyActiveListedEventsViewModel()
        {
            Title = " My Listed Events";
            Events = new ObservableCollection<Event>();
            //LoadEventsCommand = new Command(async () => await ExecuteLoadEventsCommand());
            EventNavTap = new Command<Event>(OnEventSelected);
            SearchGridTappedCommand = new Command(async () => await ExecuteSearchUiCommand());
            _ = ExecuteLoadEventsCommand();
        }

        public Event selectedEvent;
        public Event SelectedEvent
        {
            get => selectedEvent;
            set
            {
                Debug.WriteLine("ChatViewModel: SelectedChat");
                SetProperty(ref selectedEvent, value);
                OnEventSelected(value);
            }
        }

        async void OnEventSelected(Event nEvent)
        {
            if (nEvent == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(EventConfigPage)}?{nameof(EventConfigViewModel.EventId)}={nEvent.id}");
        }

        async Task ExecuteSearchUiCommand() { }

        async Task ExecuteLoadEventsCommand()
        {
            Debug.WriteLine("MainEventsViewModel: ExecuteLoadEventsCommand()");
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

                JObject accObj = await UserAccount.LoadMyProfileAsync();
                userAccount = accObj.ToObject<UserAccount>(serializer);
                OnPropertyChanged("userAccount");

                JObject dashObj = await DashboardSummary.LoadUserDashboardSummary();
                dashboardSummary = dashObj["data"].ToObject<DashboardSummary>(serializer);
                OnPropertyChanged("dashboardSummary");

                JObject sventsServerObj = await Event.fetchMyListedEvents();
                Events.Clear();
                JArray eventssArray = (JArray)sventsServerObj.GetValue("data");

                foreach (JToken token in eventssArray)
                {
                    JObject chatObj = (JObject)token;
                    Event mEvent = chatObj.ToObject<Event>(serializer);
                    Debug.WriteLine("MainEventsViewModel: " + mEvent.eventTitle);
                    Events.Add(mEvent);
                }
                OnPropertyChanged("Events");
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MainEventsViewModel: " + ex);
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
