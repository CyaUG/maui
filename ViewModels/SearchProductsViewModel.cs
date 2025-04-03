using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views;
using Youth.Views.Shopping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Youth.ViewModels.Base;

namespace Youth.ViewModels
{
    internal class SearchProductsViewModel : BaseViewModel, ISearchProductsViewModel
    {
        public UserAccount userAccount { get; set; }
        //public Command LoadMainCommand { get; }
        public ObservableCollection<ShoppingProduct> ShoppingProducts { get; }
        public SystemSettings systemSettings { get; set; }
        public Command<ShoppingProduct> ProductDetailNavTap { get; }
        public SearchProductsViewModel()
        {
            Title = "Search Products";
            ShoppingProducts = new ObservableCollection<ShoppingProduct>();
            //LoadMainCommand = new Command(async () => await ExecuteLoadMainCommand());
            ProductDetailNavTap = new Command<ShoppingProduct>(OnShoppingProductSelected);
            _ = ExecuteLoadMainCommand();
        }

        async void OnShoppingProductSelected(ShoppingProduct product)
        {
            if (product == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}?{nameof(ProductDetailsViewModel.ProductId)}={product.id}");
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged("SearchText");
                FilterItems();
            }
        }

        async Task FilterItems()
        {
            if (string.IsNullOrWhiteSpace(SearchText)) { return; }

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

                try
                {
                    Debug.WriteLine("MainShoppingViewModel: fetchShoppingProducts()");
                    JObject productsObj = await ShoppingProduct.searchShoppingProducts(SearchText);
                    JArray productsArray = (JArray)productsObj.GetValue("data");
                    ShoppingProducts.Clear();

                    foreach (JToken token in productsArray)
                    {
                        JObject productObj = (JObject)token;
                        ShoppingProduct _productModel = productObj.ToObject<ShoppingProduct>(serializer);
                        _productModel.currency = systemSettings.currency;
                        _productModel.offerDiscount = !_productModel.onDiscount;
                        ShoppingProducts.Add(_productModel);
                        Debug.WriteLine("MainShoppingViewModel: " + _productModel.label);
                    }
                    OnPropertyChanged("PopularProducts");
                }
                catch (Exception e)
                {
                    Debug.WriteLine("MainShoppingViewModel: " + e);
                    IsBusy = false;
                }
                OnPropertyChanged("Symptoms");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MainReferralViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("Symptoms");
                OnPropertyChanged("IsBusy");
            }
        }

        async Task ExecuteLoadMainCommand()
        {
            Debug.WriteLine("SearchProductsViewModel: ExecuteLoadMainCommand()");
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

                try
                {
                    Debug.WriteLine("MainShoppingViewModel: fetchShoppingProducts()");
                    JObject productsObj = await ShoppingProduct.getShoppingPopularProducts();
                    JArray productsArray = (JArray)productsObj.GetValue("data");
                    ShoppingProducts.Clear();

                    foreach (JToken token in productsArray)
                    {
                        JObject productObj = (JObject)token;
                        ShoppingProduct _productModel = productObj.ToObject<ShoppingProduct>(serializer);
                        _productModel.currency = systemSettings.currency;
                        _productModel.offerDiscount = !_productModel.onDiscount;
                        ShoppingProducts.Add(_productModel);
                        Debug.WriteLine("MainShoppingViewModel: " + _productModel.label);
                    }
                    OnPropertyChanged("PopularProducts");
                }
                catch (Exception e)
                {
                    Debug.WriteLine("MainShoppingViewModel: " + e);
                    IsBusy = false;
                }
                IsBusy = false;
                OnPropertyChanged("IsBusy");


            }
            catch (Exception ex)
            {
                Debug.WriteLine("SearchProductsViewModel: " + ex);
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
