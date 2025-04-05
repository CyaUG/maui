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
    internal class ShoppingCartViewModel : BaseViewModel, IShoppingCartViewModel
    {
        public UserAccount userAccount { get; set; }
        //public Command LoadMainCommand { get; }
        public SystemSettings systemSettings { get; set; }
        public ObservableCollection<ShoppingCart> ShoppingCarts { get; }

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
        public ShoppingCartViewModel()
        {
            Title = "Cart";
            ShoppingCarts = new ObservableCollection<ShoppingCart>();
            //LoadMainCommand = new Command(async () => await ExecuteLoadMainCommand());
            _ = ExecuteLoadMainCommand();
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
