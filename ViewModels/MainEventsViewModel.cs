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
    internal class MainEventsViewModel : BaseViewModel, IMainEventsViewModel
    {
        public DashboardSummary dashboardSummary { get; set; }
        public ObservableCollection<EventCategory> eventCategories { get; set; }
        public ObservableCollection<Event> Events { get; set; }
        //public Command LoadEventCategoriesCommand { get; }
        public UserAccount userAccount { get; set; }
        public EventCategory selectedEventCategory;
        public Event selectedEvent;
        public SystemSettings systemSettings { get; set; }
        public Command<EventCategory> EventCategoryNavTap { get; }
        public Command<Event> EventNavTap { get; }
        public Command SearchGridTappedCommand { get; }

        public MainEventsViewModel()
        {
            Title = "Events";
            eventCategories = new ObservableCollection<EventCategory>();
            Events = new ObservableCollection<Event>();
            //LoadEventCategoriesCommand = new Command(async () => await ExecuteLoadEventCategoriesCommand());
            EventCategoryNavTap = new Command<EventCategory>(OnEventCategorySelected);
            EventNavTap = new Command<Event>(OnEventSelected);
            SearchGridTappedCommand = new Command(async () => await ExecuteSearchUiCommand());
            _ = ExecuteLoadEventCategoriesCommand();
        }

        public EventCategory SelectedEventCategory
        {
            get => selectedEventCategory;
            set
            {
                Debug.WriteLine("ChatViewModel: SelectedChat");
                SetProperty(ref selectedEventCategory, value);
                OnEventCategorySelected(value);
            }
        }

        async void OnEventCategorySelected(EventCategory eventCategory)
        {
            if (eventCategory == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(EventCategoryEventsPage)}?{nameof(EventCategoryEventsViewModel.CategoryId)}={eventCategory.id}");
        }

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
            await Shell.Current.GoToAsync($"{nameof(EventDetailsPage)}?{nameof(EventDetailsViewModel.EventId)}={nEvent.id}");
        }

        async Task ExecuteSearchUiCommand() { }

        async Task ExecuteLoadEventCategoriesCommand()
        {
            Debug.WriteLine("MainEventsViewModel: ExecuteLoadEventCategoriesCommand()");
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

                JObject categoriesObj = await EventCategory.fetchMyEventCategories();
                eventCategories.Clear();
                JArray categoriesArray = (JArray)categoriesObj.GetValue("data");

                foreach (JToken token in categoriesArray)
                {
                    JObject chatObj = (JObject)token;
                    EventCategory mEventCategory = chatObj.ToObject<EventCategory>(serializer);
                    Debug.WriteLine("MainEventsViewModel: " + mEventCategory.label);
                    eventCategories.Add(mEventCategory);
                }
                OnPropertyChanged("eventCategories");

                JObject sventsServerObj = await Event.fetchEvents();
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
