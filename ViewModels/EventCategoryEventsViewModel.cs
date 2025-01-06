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
    [QueryProperty(nameof(CategoryId), nameof(CategoryId))]
    public class EventCategoryEventsViewModel : BaseViewModel, IEventCategoryEventsViewModel
    {
        private int categoryId;
        public ObservableCollection<Event> Events { get; set; }
        public Command<Event> EventNavTap { get; }
        public Command LoadEventsCommand { get; }
        public Event selectedEvent;

        public EventCategoryEventsViewModel()
        {
            Title = "Category Events";
            Events = new ObservableCollection<Event>();
            EventNavTap = new Command<Event>(OnEventSelected);
            LoadEventsCommand = new Command(async () => LoadEventsEvents(categoryId));
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

        public int CategoryId
        {
            get
            {
                return categoryId;
            }
            set
            {
                categoryId = value;
                LoadEventsEvents(value);
            }
        }

        public async void LoadEventsEvents(int mCategoryId)
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

                JObject sventsServerObj = await Event.fetchRelatedEvents(mCategoryId);
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
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MainShoppingViewModel: " + ex);
                IsBusy = false;
            }
            finally
            {
                IsBusy = false;
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
