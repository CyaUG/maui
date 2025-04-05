using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.Events;
using Youth.Views.jobs;
using Youth.Views.Main;
using Youth.Views.SafeSpace;
using Youth.Views.Shopping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Input;
using Youth.ViewModels.Base;

namespace Youth.ViewModels
{
    [QueryProperty(nameof(EventId), nameof(EventId))]
    public class EventDetailsViewModel : BaseViewModel, IEventDetailsViewModel
    {
        private int eventId;
        public SystemSettings systemSettings { get; set; }
        public UserAccount userAccount { get; set; }
        public Event EventDetails { get; set; }
        public ObservableCollection<UserAccount> eventAttendees { get; }
        //public Command EventDetailsLoadCommand { get; }
        public Command OpenEventApplicationPage { get; }
        public Command<Event> EventAddressTap { get; }
        public CustomerCareChatGroup customerCareChatGroup { get; set; }
        public Command OpenContactUsPage { get; }
        public Command SaveEventCommand { get; }
        public Command LikeEventCommand { get; }
        public Command UnLikeEventCommand { get; }
        public Command OpenCommentsCommand { get; }

        public bool _IsOwner;
        public bool IsOwner
        {
            get
            {
                return _IsOwner;
            }
            set
            {
                _IsOwner = value;
            }
        }

        public EventDetailsViewModel()
        {
            Title = "Event Info";
            eventAttendees = new ObservableCollection<UserAccount>();
            //EventDetailsLoadCommand = new Command(async () => LoadEventDetails(eventId));
            OpenEventApplicationPage = new Command(async () => OnOpenEventApplicationPage());
            EventAddressTap = new Command<Event>(OnEventAddressSelected);

            OpenContactUsPage = new Command(async () => await ExecuteOpenContactUsPage());
            SaveEventCommand = new Command(async () => await ExecuteSaveEventCommand());
            LikeEventCommand = new Command(async () => await ExecuteLikeEventCommand());
            UnLikeEventCommand = new Command(async () => await ExecuteUnLikeEventCommand());
            OpenCommentsCommand = new Command(async () => await ExecuteOpenCommentsCommand());
            LoadEventDetails(eventId);
        }

        async Task ExecuteSaveEventCommand()
        {
            IsOwner = true;
            await Event.saveEvent(EventId);
            IsOwner = false;
            IsBusy = true;
            OnPropertyChanged("IsOwner");
            OnPropertyChanged("IsBusy");
        }

        async Task ExecuteLikeEventCommand()
        {
            IsOwner = true;
            await Event.submitEventLike(EventId);
            IsOwner = false;
            IsBusy = true;
            OnPropertyChanged("IsOwner");
            OnPropertyChanged("IsBusy");
        }

        async Task ExecuteUnLikeEventCommand()
        {
            IsOwner = true;
            await Event.submitEventLike(EventId);
            IsOwner = false;
            IsBusy = true;
            OnPropertyChanged("IsOwner");
            OnPropertyChanged("IsBusy");
        }

        private async Task ExecuteOpenCommentsCommand()
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(EventDiscussionPage)}?{nameof(EventDiscussionViewModel.EventId)}={EventId}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EventDetailsViewModel: " + ex);
            }
        }

        async Task ExecuteOpenContactUsPage()
        {
            if (customerCareChatGroup == null)
                return;

            //open contact us page
            await Shell.Current.GoToAsync($"{nameof(MessagesPage)}?{nameof(MessageViewModel.ChatId)}={customerCareChatGroup.id}&{nameof(MessageViewModel.UserId)}={userAccount.id}");
        }

        async void OnEventAddressSelected(Event mEvent)
        {
            if (mEvent == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            Debug.WriteLine("EventDetailsViewModel: OnEventAddressSelected()");
            await Map.OpenAsync(mEvent.event_latitude, mEvent.event_longitude, new MapLaunchOptions { Name = mEvent.event_address });
        }

        async void OnOpenEventApplicationPage()
        {
            await Shell.Current.GoToAsync($"{nameof(EventApplicationPage)}?{nameof(EventApplicationViewModel.EventId)}={EventId}");
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

        public async void LoadEventDetails(int mEventId)
        {
            Debug.WriteLine("EventDetailsViewModel: ExecuteShoppingCategoryCommand()");
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
                EventDetails.isUnLiked = !EventDetails.likedIt;
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

                JObject ccObj = await CustomerCareChatGroup.getCustomerCareChatGroupId();
                customerCareChatGroup = ccObj.ToObject<CustomerCareChatGroup>(serializer);
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
