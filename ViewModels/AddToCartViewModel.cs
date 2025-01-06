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
    internal class AddToCartViewModel : BaseViewModel, IAddToCartViewModel
    {
        private int productId;
        public ShoppingProduct shoppingProduct { get; set; }
        public ObservableCollection<ShoppingProductColorOption> ShoppingProductColorOptions { get; set; }
        public ObservableCollection<ShoppingProductSizeOption> ShoppingProductSizeOptions { get; }
        public ObservableCollection<ShoppingProductSpecification> ShoppingProductSpecifications { get; }
        public ObservableCollection<ShoppingProductGallery> ShoppingProductGalleryImages { get; }
        public Command<ShoppingProductColorOption> ColorOptionTapped { get; }
        public Command<ShoppingProductSizeOption> SizeOptionTapped { get; }
        public SystemSettings systemSettings { get; set; }
        public int cartCount { get; set; }
        public double priceToPay { get; set; }
        public bool isColorOptionsVisible { get; set; }
        public bool isSizeOptionsVisible { get; set; }
        public Command OpenSizeColorProviderCommand { get; }
        public Command OpenProductColorProviderCommand { get; }
        public Command IncrementCartCommand { get; }
        public Command DecrementCartCommand { get; }
        public Command BuyMowCommand { get; }
        public Command AddToCartCommand { get; }

        public AddToCartViewModel()
        {
            Title = "Product";
            ShoppingProductColorOptions = new ObservableCollection<ShoppingProductColorOption>();
            ShoppingProductSizeOptions = new ObservableCollection<ShoppingProductSizeOption>();
            ShoppingProductSpecifications = new ObservableCollection<ShoppingProductSpecification>();
            ShoppingProductGalleryImages = new ObservableCollection<ShoppingProductGallery>();
            isColorOptionsVisible = false;
            isSizeOptionsVisible = false;
            OpenSizeColorProviderCommand = new Command(async () => OnOpenSizeColorProvider());
            OpenProductColorProviderCommand = new Command(async () => OnOpenProductColorProvider());
            IncrementCartCommand = new Command(async () => ExecuteIncrementCartCommand());
            DecrementCartCommand = new Command(async () => ExecuteDecrementCartCommand());
            BuyMowCommand = new Command(async () => ExecuteBuyMowCommand());
            AddToCartCommand = new Command(async () => ExecuteAddToCartCommand());
        }
        async void OnOpenSizeColorProvider()
        {
            if (shoppingProduct == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ProductSizeProviderPage)}?{nameof(ProductSizeProviderViewModel.ProductId)}={shoppingProduct.id}");
        }

        async void OnOpenProductColorProvider()
        {
            if (shoppingProduct == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ProductColorProviderPage)}?{nameof(ProductColorProviderViewModel.ProductId)}={shoppingProduct.id}");
        }

        async Task ExecuteIncrementCartCommand()
        {
            if (shoppingProduct == null)
                return;

            if (shoppingProduct.maxOrder > cartCount)
            {
                cartCount++;
                OnPropertyChanged("cartCount");

                if (shoppingProduct.onDiscount)
                {
                    priceToPay = shoppingProduct.discountPrice * cartCount;
                }
                else
                {
                    priceToPay = shoppingProduct.sellPrice * cartCount;
                }
                OnPropertyChanged("priceToPay");
            }
        }

        async Task ExecuteBuyMowCommand()
        {
            int colorId = 0;
            int sizeId = 0;

            if (mShoppingProductColorOption != null)
            {
                colorId = mShoppingProductColorOption.id;
            }

            if (mShoppingProductSizeOption != null)
            {
                sizeId = mShoppingProductSizeOption.id;
            }
            await ShoppingCart.SubmitShoppingCartBuyNow(ProductId, cartCount, colorId, sizeId);
            await Shell.Current.GoToAsync($"{nameof(ShoppingCartPage)}");
        }

        async Task ExecuteAddToCartCommand()
        {
            int colorId = 0;
            int sizeId = 0;

            if (mShoppingProductColorOption != null)
            {
                colorId = mShoppingProductColorOption.id;
            }

            if (mShoppingProductSizeOption != null)
            {
                sizeId = mShoppingProductSizeOption.id;
            }
            await ShoppingCart.SubmitShoppingCartBuyNow(ProductId, cartCount, colorId, sizeId);
            GoBack();
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        async Task ExecuteDecrementCartCommand()
        {
            if (shoppingProduct == null)
                return;

            if (shoppingProduct.minOrder < cartCount)
            {
                cartCount--;
                OnPropertyChanged("cartCount");

                if (shoppingProduct.onDiscount)
                {
                    priceToPay = shoppingProduct.discountPrice * cartCount;
                }
                else
                {
                    priceToPay = shoppingProduct.sellPrice * cartCount;
                }
                OnPropertyChanged("priceToPay");
            }
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
        private ShoppingProductColorOption _shoppingProductColorOption;
        public ShoppingProductColorOption mShoppingProductColorOption
        {
            get
            {
                return _shoppingProductColorOption;
            }
            set
            {
                _shoppingProductColorOption = value;
                OnPropertyChanged("mShoppingProductColorOption");
            }
        }
        private ShoppingProductSizeOption _shoppingProductSizeOption;
        public ShoppingProductSizeOption mShoppingProductSizeOption
        {
            get
            {
                return _shoppingProductSizeOption;
            }
            set
            {
                _shoppingProductSizeOption = value;
                OnPropertyChanged("mShoppingProductSizeOption");
            }
        }
        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
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

                systemSettings = await SystemSettings.fetchSystemSettings();
                Debug.WriteLine("MessageViewModel: " + systemSettings.currency);
                OnPropertyChanged("systemSettings");

                JObject serverObj = await ShoppingProduct.fetchShoppingProduct(productId);
                JObject productObj = (JObject)serverObj.GetValue("data");
                shoppingProduct = productObj.ToObject<ShoppingProduct>(serializer);
                shoppingProduct.currency = systemSettings.currency;
                OnPropertyChanged("shoppingProduct");

                cartCount = shoppingProduct.minOrder;
                OnPropertyChanged("cartCount");

                if (shoppingProduct.onDiscount)
                {
                    priceToPay = shoppingProduct.discountPrice * cartCount;
                }
                else
                {
                    priceToPay = shoppingProduct.sellPrice * cartCount;
                }
                OnPropertyChanged("priceToPay");

                JObject colorsObj = await ShoppingProductColorOption.FetchShoppingProductColorOptions(productId);
                JArray colorsObjArray = (JArray)colorsObj.GetValue("data");
                ShoppingProductColorOptions.Clear();

                foreach (JToken token in colorsObjArray)
                {
                    JObject chatObj = (JObject)token;
                    ShoppingProductColorOption _shoppingProductColorOption = chatObj.ToObject<ShoppingProductColorOption>(serializer);
                    ShoppingProductColorOptions.Add(_shoppingProductColorOption);

                    if (_shoppingProductColorOption.isDefault && mShoppingProductColorOption == null)
                    {
                        mShoppingProductColorOption = _shoppingProductColorOption;
                    }
                }
                if (ShoppingProductColorOptions.Count > 0)
                {
                    isColorOptionsVisible = true;
                }
                OnPropertyChanged("isColorOptionsVisible");
                OnPropertyChanged("ShoppingProductColorOptions");

                JObject sizesObj = await ShoppingProductSizeOption.FetchShoppingProductSizeOptions(productId);
                JArray sizesObjArray = (JArray)sizesObj.GetValue("data");
                ShoppingProductSizeOptions.Clear();

                foreach (JToken token in sizesObjArray)
                {
                    JObject chatObj = (JObject)token;
                    ShoppingProductSizeOption _shoppingProductSizeOption = chatObj.ToObject<ShoppingProductSizeOption>(serializer);
                    ShoppingProductSizeOptions.Add(_shoppingProductSizeOption);

                    if (_shoppingProductSizeOption.isDefault && mShoppingProductSizeOption == null)
                    {
                        mShoppingProductSizeOption = _shoppingProductSizeOption;
                    }
                }
                if (ShoppingProductSizeOptions.Count > 0)
                {
                    isSizeOptionsVisible = true;
                }
                OnPropertyChanged("isSizeOptionsVisible");
                OnPropertyChanged("ShoppingProductSizeOptions");

                JObject specsObj = await ShoppingProductSpecification.FetchShoppingProductSpecifications(productId);
                JArray specsObjArray = (JArray)specsObj.GetValue("data");
                ShoppingProductSpecifications.Clear();

                foreach (JToken token in specsObjArray)
                {
                    JObject chatObj = (JObject)token;
                    ShoppingProductSpecification _shoppingProductSpecification = chatObj.ToObject<ShoppingProductSpecification>(serializer);
                    ShoppingProductSpecifications.Add(_shoppingProductSpecification);
                }
                OnPropertyChanged("ShoppingProductSpecifications");

                JObject galleryObj = await ShoppingProductGallery.FetchShoppingProductGallery(productId);
                JArray galleryObjArray = (JArray)galleryObj.GetValue("data");
                ShoppingProductGalleryImages.Clear();

                foreach (JToken token in galleryObjArray)
                {
                    JObject chatObj = (JObject)token;
                    ShoppingProductGallery _shoppingProductGallery = chatObj.ToObject<ShoppingProductGallery>(serializer);
                    ShoppingProductGalleryImages.Add(_shoppingProductGallery);
                }
                OnPropertyChanged("ShoppingProductGalleryImages");
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
