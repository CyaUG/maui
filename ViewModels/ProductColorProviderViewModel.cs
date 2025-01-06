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
    internal class ProductColorProviderViewModel : BaseViewModel, IProductColorProviderViewModel
    {
        private int productId;
        public ObservableCollection<ShoppingProductColorOption> ShoppingProductColorOptions { get; set; }
        public Command<ShoppingProductColorOption> ProductColorTapCommand { get; set; }

        public ProductColorProviderViewModel()
        {
            Title = "Colors";
            ShoppingProductColorOptions = new ObservableCollection<ShoppingProductColorOption>();
            ProductColorTapCommand = new Command<ShoppingProductColorOption>(OnProductColorSelected);
        }

        async void OnProductColorSelected(ShoppingProductColorOption shoppingProductColorOption)
        {
            if (shoppingProductColorOption == null)
                return;
            //open create referral page
            MessagingCenter.Send<ProductColorProviderViewModel, ShoppingProductColorOption>(this, "shoppingProductColorOption", shoppingProductColorOption);
            GoBack();
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
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

                JObject colorsObj = await ShoppingProductColorOption.FetchShoppingProductColorOptions(productId);
                JArray colorsObjArray = (JArray)colorsObj.GetValue("data");
                ShoppingProductColorOptions.Clear();

                foreach (JToken token in colorsObjArray)
                {
                    JObject chatObj = (JObject)token;
                    ShoppingProductColorOption _shoppingProductColorOption = chatObj.ToObject<ShoppingProductColorOption>(serializer);
                    ShoppingProductColorOptions.Add(_shoppingProductColorOption);
                }
                OnPropertyChanged("ShoppingProductColorOptions");
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

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
            /*Set defaults*/
        }
    }
}
