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
    [QueryProperty(nameof(ProductId), nameof(ProductId))]
    internal class ProductSizeProviderViewModel : BaseViewModel, IProductSizeProviderViewModel
    {
        private int productId;
        public ObservableCollection<ShoppingProductSizeOption> ShoppingProductSizeOptions { get; }
        public Command<ShoppingProductSizeOption> ProductSizeTapCommand { get; }

        public ProductSizeProviderViewModel()
        {
            Title = "Sizes";
            ShoppingProductSizeOptions = new ObservableCollection<ShoppingProductSizeOption>();
            ProductSizeTapCommand = new Command<ShoppingProductSizeOption>(OnProductSizeSelected);
        }

        async void OnProductSizeSelected(ShoppingProductSizeOption shoppingProductSizeOption)
        {
            if (shoppingProductSizeOption == null)
                return;
            //open create referral page
            MessagingCenter.Send<ProductSizeProviderViewModel, ShoppingProductSizeOption>(this, "shoppingProductSizeOption", shoppingProductSizeOption);
            GoBack();
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
        }

        public int ProductId
        {
            get
            {
                return productId;
            }
            set
            {
                productId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(int productId)
        {
            try
            {
                Debug.WriteLine("MessageViewModel: LoadUserAccount(), productId: " + productId);
                var serializer = new JsonSerializer();
                serializer.Error += (sender, args) =>
                {
                    if (args.ErrorContext.Error is JsonException)
                    {
                        args.ErrorContext.Handled = true;
                    }
                };

                JObject sizesObj = await ShoppingProductSizeOption.FetchShoppingProductSizeOptions(productId);
                JArray sizesObjArray = (JArray)sizesObj.GetValue("data");
                ShoppingProductSizeOptions.Clear();

                foreach (JToken token in sizesObjArray)
                {
                    JObject chatObj = (JObject)token;
                    ShoppingProductSizeOption _shoppingProductSizeOption = chatObj.ToObject<ShoppingProductSizeOption>(serializer);
                    ShoppingProductSizeOptions.Add(_shoppingProductSizeOption);
                }
                OnPropertyChanged("ShoppingProductSizeOptions");
                IsBusy = false;
            }
            catch (Exception)
            {
                IsBusy = false;
                Debug.WriteLine("Failed to Load Item");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("shoppingProduct");
                OnPropertyChanged("IsBusy");
            }
        }
    }
}
