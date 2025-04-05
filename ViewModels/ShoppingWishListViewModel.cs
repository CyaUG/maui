using Youth.Models;
using Youth.ViewModels.Interfaces;
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
    public class ShoppingWishListViewModel : BaseViewModel, IShoppingWishListViewModel
    {
        //public Command LoadShoppingWishListCommand { get; }
        public ObservableCollection<ShoppingProduct> ShoppingProducts { get; }
        public Command<ShoppingProduct> ProductDetailNavTap { get; }
        public ShoppingProduct selectedShoppingProduct;
        public SystemSettings systemSettings { get; set; }

        public ShoppingWishListViewModel()
        {
            Title = "WishList";
            ShoppingProducts = new ObservableCollection<ShoppingProduct>();
            //LoadShoppingWishListCommand = new Command(async () => await ExecuteLoadShoppingWishListCommand());
            ProductDetailNavTap = new Command<ShoppingProduct>(OnShoppingProductSelected);
            _ = ExecuteLoadShoppingWishListCommand();
        }

        public ShoppingProduct SelectedProduct
        {
            get => selectedShoppingProduct;
            set
            {
                Debug.WriteLine("ShoppingWishListViewModel: ShoppingProduct");
                SetProperty(ref selectedShoppingProduct, value);
                OnShoppingProductSelected(value);
            }
        }

        async void OnShoppingProductSelected(ShoppingProduct product)
        {
            if (product == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}?{nameof(ProductDetailsViewModel.ProductId)}={product.id}");
        }

        async Task ExecuteLoadShoppingWishListCommand()
        {
            Debug.WriteLine("ShoppingWishListViewModel: ExecuteLoadShoppingWishListCommand()");
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

                JObject prodServerObj = await ShoppingProduct.fetchShoppingWishList();
                JArray chatsArray = (JArray)prodServerObj.GetValue("data");
                ShoppingProducts.Clear();

                foreach (JToken token in chatsArray)
                {
                    JObject chatObj = (JObject)token;
                    ShoppingProduct ShoppingProductModel = chatObj.ToObject<ShoppingProduct>(serializer);
                    Debug.WriteLine("ShoppingWishListViewModel: " + ShoppingProductModel.label);
                    ShoppingProductModel.currency = systemSettings.currency;
                    ShoppingProductModel.offerDiscount = !ShoppingProductModel.onDiscount;
                    ShoppingProducts.Add(ShoppingProductModel);
                }
                OnPropertyChanged("ShoppingProducts");

                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ShoppingWishListViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("ShoppingProducts");
                OnPropertyChanged("IsBusy");
            }
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
            /*Set defaults*/
        }
    }
}
