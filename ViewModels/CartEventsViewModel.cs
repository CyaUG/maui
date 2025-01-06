using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Youth.ViewModels.Base;


namespace Youth.ViewModels
{
    internal class CartEventsViewModel : BaseViewModel, ICartEventsViewModel
    {
        public UserAccount userAccount { get; set; }
        public Command LoadMainCommand { get; }
        public SystemSettings systemSettings { get; set; }
        public ObservableCollection<EventCart> EventCarts { get; }

        public double _TotalAmmount;
        public double TotalAmmount
        {
            get
            {
                return _TotalAmmount;
            }
            set
            {
                _TotalAmmount = value;
            }
        }
        public CartEventsViewModel()
        {
            Title = "Cart";
            EventCarts = new ObservableCollection<EventCart>();
            LoadMainCommand = new Command(async () => await ExecuteLoadMainCommand());
        }

        async Task ExecuteLoadMainCommand()
        {
            Debug.WriteLine("CartEventsViewModel: ExecuteLoadMainCommand()");
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
                Debug.WriteLine("CartEventsViewModel: " + systemSettings.currency);
                OnPropertyChanged("systemSettings");

                JObject accObj = await UserAccount.LoadMyProfileAsync();
                userAccount = accObj.ToObject<UserAccount>(serializer);
                OnPropertyChanged("userAccount");

                try
                {
                    Debug.WriteLine("CartEventsViewModel: fetchMyCartEvents()");
                    JObject eventssObj = await EventCart.fetchMyCartEvents();
                    JArray eventssArray = (JArray)eventssObj.GetValue("data");
                    EventCarts.Clear();
                    TotalAmmount = 0;

                    foreach (JToken token in eventssArray)
                    {
                        JObject eventsObj = (JObject)token;
                        EventCart _eventCart = eventsObj.ToObject<EventCart>(serializer);
                        _eventCart.currency = systemSettings.currency;
                        _eventCart.offerDiscount = !_eventCart.onDiscount;

                        if (_eventCart.onDiscount)
                        {
                            TotalAmmount += _eventCart.discountPrice * _eventCart.orderQty;
                        }
                        else
                        {
                            TotalAmmount += _eventCart.amount * _eventCart.orderQty;
                        }

                        EventCarts.Add(_eventCart);
                        Debug.WriteLine("CartEventsViewModel: " + _eventCart.eventTitle);
                    }
                    OnPropertyChanged("EventCarts");
                    OnPropertyChanged("TotalAmmount");
                }
                catch (Exception e)
                {
                    Debug.WriteLine("CartEventsViewModel: " + e);
                    IsBusy = false;
                }
                IsBusy = false;
                OnPropertyChanged("IsBusy");


            }
            catch (Exception ex)
            {
                Debug.WriteLine("CartEventsViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
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
