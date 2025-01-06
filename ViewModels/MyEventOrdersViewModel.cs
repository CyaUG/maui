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
    internal class MyEventOrdersViewModel : BaseViewModel, IMyEventOrdersViewModel
    {
        public DashboardSummary dashboardSummary { get; set; }
        public ObservableCollection<EventsOrder> EventsOrders { get; set; }
        public Command LoadEventsCommand { get; }
        public UserAccount userAccount { get; set; }
        public SystemSettings systemSettings { get; set; }
        public Command<EventsOrder> EventsOrderNavTap { get; }
        public Command SearchGridTappedCommand { get; }
        public MyEventOrdersViewModel()
        {
            Title = "My Orders";
            EventsOrders = new ObservableCollection<EventsOrder>();
            LoadEventsCommand = new Command(async () => await ExecuteLoadEventsCommand());
            EventsOrderNavTap = new Command<EventsOrder>(OnEventsOrderSelected);
            SearchGridTappedCommand = new Command(async () => await ExecuteSearchUiCommand());
        }

        public EventsOrder selectedEventsOrder;
        public EventsOrder SelectedEvent
        {
            get => selectedEventsOrder;
            set
            {
                SetProperty(ref selectedEventsOrder, value);
                OnEventsOrderSelected(value);
            }
        }

        async void OnEventsOrderSelected(EventsOrder nEventsOrder)
        {
            if (nEventsOrder == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(EventOrderDetailsPage)}?{nameof(EventOrderDetailsViewModel.EventId)}={nEventsOrder.id}");
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

                JObject ordersServerObj = await EventsOrder.fetchMyEventsOrders();
                EventsOrders.Clear();
                JArray ordersArray = (JArray)ordersServerObj.GetValue("data");

                foreach (JToken token in ordersArray)
                {
                    JObject chatObj = (JObject)token;
                    EventsOrder mEventsOrder = chatObj.ToObject<EventsOrder>(serializer);
                    Debug.WriteLine("MySavedEventsViewModel: " + mEventsOrder.eventTitle);
                    EventsOrders.Add(mEventsOrder);
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
