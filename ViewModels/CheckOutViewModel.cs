using Youth.Models;
using Youth.Utils;
using Youth.ViewModels.Interfaces;
using Youth.Views.jobs;
using Youth.Views.Quizzes;
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
    internal class CheckOutViewModel : BaseViewModel, ICheckOutViewModel
    {
        public UserAccount userAccount { get; set; }
        public DeliveryFee mDeliveryFee { get; set; }
        //public Command LoadMainCommand { get; }
        public SystemSettings systemSettings { get; set; }
        public ObservableCollection<ShoppingCart> ShoppingCarts { get; }

        public double _TotalSum;
        public double TotalSum
        {
            get
            {
                return _TotalSum;
            }
            set
            {
                _TotalSum = value;
            }
        }

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

        private Location _Location;
        public Location activeLocation
        {
            get { return _Location; }
            set
            {
                _Location = value;
                OnPropertyChanged("activeLocation");
            }
        }

        private DeliveryAddress _deliveryAddress;
        public DeliveryAddress deliveryAddress
        {
            get { return _deliveryAddress; }
            set
            {
                _deliveryAddress = value;
                LocationAddress = _deliveryAddress.locationAddress;
                OnPropertyChanged("LocationAddress");
                OnPropertyChanged("deliveryAddress");
                ComputeDeliveryFee(value);
            }
        }
        public string LocationAddress { get; set; }
        public String SetLocationAddress
        {
            get { return LocationAddress; }
            set
            {
                LocationAddress = value;
                OnPropertyChanged("LocationAddress");
            }
        }
        public Command PayOnDeliveryCommand { get; }
        public Command PayWithExchangeCommand { get; }
        public Command OpenDeliveryInfoProviderPageCommand { get; }

        public CheckOutViewModel()
        {
            Title = "Check Out";
            LocationAddress = "Pick Delivery Info";
            ShoppingCarts = new ObservableCollection<ShoppingCart>();
            //LoadMainCommand = new Command(async () => await ExecuteLoadMainCommand());
            PayOnDeliveryCommand = new Command(async () => await ExecutePayOnDeliveryCommand());
            PayWithExchangeCommand = new Command(async () => await ExecutePayWithExchangeCommand());
            OpenDeliveryInfoProviderPageCommand = new Command(async () => await ExecuteOpenDeliveryInfoProviderPageCommand());
            _ = ExecuteLoadMainCommand();
        }

        async Task ExecuteOpenDeliveryInfoProviderPageCommand()
        {
            await Shell.Current.GoToAsync($"{nameof(DeliveryInfoProviderPage)}");
        }

        async Task ComputeDeliveryFee(DeliveryAddress deliveryAddress)
        {
            if (deliveryAddress == null)
                return;

            Debug.WriteLine("CheckOutViewModel: ComputeDeliveryFee()");

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
                Debug.WriteLine("CheckOutViewModel: " + deliveryAddress.locationAddress);

                JObject serverObj = await DeliveryFee.computeDeliveryFee(deliveryAddress);
                Debug.WriteLine("CheckOutViewModel: " + serverObj);
                JObject deliveryFeeObj = (JObject)serverObj.GetValue("data");
                mDeliveryFee = deliveryFeeObj.ToObject<DeliveryFee>(serializer);

                TotalSum = mDeliveryFee.deliveryFee + TotalAmmount;
                OnPropertyChanged("mDeliveryFee");
                OnPropertyChanged("TotalSum");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CheckOutViewModel::: " + ex);
            }
        }

        async Task ExecutePayOnDeliveryCommand()
        {
            if (deliveryAddress == null)
                return;

            Debug.WriteLine("CheckOutViewModel: ComputeDeliveryFee()");

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
                Debug.WriteLine("CheckOutViewModel: " + deliveryAddress.locationAddress);

                JObject serverObj = await ShoppingCart.CheckoutPayOnDelivery(deliveryAddress);
                Debug.WriteLine("CheckOutViewModel: " + serverObj);
                String message = (String)serverObj.GetValue("message");
                MessagingCenter.Send<CheckOutViewModel, string>(this, "message", message + "");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CheckOutViewModel::: " + ex);
            }
        }

        async Task ExecutePayWithExchangeCommand()
        {
            if (deliveryAddress == null)
                return;

            Debug.WriteLine("CheckOutViewModel: ComputeDeliveryFee()");

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
                Debug.WriteLine("CheckOutViewModel: " + deliveryAddress.locationAddress);

                JObject serverObj = await ShoppingCart.CheckoutPayWithNsiimbi(deliveryAddress);
                Debug.WriteLine("CheckOutViewModel: " + serverObj);
                String checkOutUrl = (String)serverObj.GetValue("checkOutUrl");
                Constants.OpenUri(checkOutUrl);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CheckOutViewModel::: " + ex);
            }
        }

        async Task ExecuteLoadMainCommand()
        {
            Debug.WriteLine("ShoppingCartViewModel: ExecuteLoadMainCommand()");
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
                Debug.WriteLine("ShoppingCartViewModel: " + systemSettings.currency);
                OnPropertyChanged("systemSettings");

                JObject accObj = await UserAccount.LoadMyProfileAsync();
                userAccount = accObj.ToObject<UserAccount>(serializer);
                OnPropertyChanged("userAccount");

                try
                {
                    Debug.WriteLine("ShoppingCartViewModel: fetchShoppingProducts()");
                    JObject productsObj = await ShoppingCart.getShoppingCart();
                    JArray productsArray = (JArray)productsObj.GetValue("data");
                    ShoppingCarts.Clear();
                    TotalAmmount = 0;

                    foreach (JToken token in productsArray)
                    {
                        JObject productObj = (JObject)token;
                        ShoppingCart _shoppingCart = productObj.ToObject<ShoppingCart>(serializer);
                        _shoppingCart.currency = systemSettings.currency;
                        _shoppingCart.offerDiscount = !_shoppingCart.onDiscount;

                        if (_shoppingCart.onDiscount)
                        {
                            TotalAmmount += _shoppingCart.discountPrice * _shoppingCart.orderQty;
                        }
                        else
                        {
                            TotalAmmount += _shoppingCart.sellPrice * _shoppingCart.orderQty;
                        }

                        ShoppingCarts.Add(_shoppingCart);
                        Debug.WriteLine("ShoppingCartViewModel: " + _shoppingCart.label);
                    }
                    OnPropertyChanged("ShoppingCarts");
                    OnPropertyChanged("TotalAmmount");
                }
                catch (Exception e)
                {
                    Debug.WriteLine("ShoppingCartViewModel: " + e);
                    IsBusy = false;
                }
                IsBusy = false;
                OnPropertyChanged("IsBusy");


            }
            catch (Exception ex)
            {
                Debug.WriteLine("ShoppingCartViewModel: " + ex);
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
