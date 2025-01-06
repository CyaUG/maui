using Youth.Models;
using Youth.Utils;
using Youth.ViewModels.Interfaces;
using Youth.Views.jobs;
using Youth.Views.Main;
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
    [QueryProperty(nameof(ProductId), nameof(ProductId))]
    public class ProductDetailsViewModel : BaseViewModel, IProductDetailsViewModel
    {
        public ShoppingProduct shoppingProduct { get; set; }
        private int productId;
        public ICommand ArrowGoBackCommand { get; set; }
        public Command LoadShoppingProductCommand { get; }
        public Command LoadAddToCartPageCommand { get; }
        public ObservableCollection<ShoppingProductColorOption> ShoppingProductColorOptions { get; set; }
        public ObservableCollection<ShoppingProductSizeOption> ShoppingProductSizeOptions { get; }
        public ObservableCollection<ShoppingProductSpecification> ShoppingProductSpecifications { get; }
        public ObservableCollection<ShoppingProductGallery> ShoppingProductGalleryImages { get; }
        public Command<ShoppingProductColorOption> ColorOptionTapped { get; }
        public Command<ShoppingProductSizeOption> SizeOptionTapped { get; }
        public SystemSettings systemSettings { get; set; }
        public bool isColorOptionsVisible { get; set; }
        public bool isSizeOptionsVisible { get; set; }
        public UserAccount myAccount { get; set; }
        public Command OpenContactUsPage { get; }
        public Command ShareProductCommand { get; }
        public Command OpenCommentsCommand { get; }
        public CustomerCareChatGroup customerCareChatGroup { get; set; }

        public ProductDetailsViewModel()
        {
            Title = "Product";
            ArrowGoBackCommand = new Command(GoBack);
            ShoppingProductColorOptions = new ObservableCollection<ShoppingProductColorOption>();
            ShoppingProductSizeOptions = new ObservableCollection<ShoppingProductSizeOption>();
            ShoppingProductSpecifications = new ObservableCollection<ShoppingProductSpecification>();
            ShoppingProductGalleryImages = new ObservableCollection<ShoppingProductGallery>();
            LoadShoppingProductCommand = new Command(async () => LoadItemId(ProductId));
            LoadAddToCartPageCommand = new Command(async () => OnAddToCartPageSelected());
            ColorOptionTapped = new Command<ShoppingProductColorOption>(ExecuteColorOptionTappedCommand);
            SizeOptionTapped = new Command<ShoppingProductSizeOption>(ExecuteSizeOptionTappedCommand);
            isColorOptionsVisible = false;
            isSizeOptionsVisible = false;
            OpenContactUsPage = new Command(async () => await ExecuteOpenContactUsPage());
            ShareProductCommand = new Command(async () => await ExecuteShareProductCommand());
            OpenCommentsCommand = new Command(async () => await ExecuteOpenCommentsCommand());
        }

        async Task ExecuteShareProductCommand()
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = "Hello, I want to share this product with you " + Constants.appDomain + "shopping/products/" + shoppingProduct.id,
                Title = Constants.appName
            });
        }

        private async Task ExecuteOpenCommentsCommand()
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(ShoppingProductDiscussionPage)}?{nameof(ShoppingProductDiscussionViewModel.ProductId)}={ProductId}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ProductDetailsViewModel: " + ex);
            }
        }

        async Task ExecuteOpenContactUsPage()
        {
            if (customerCareChatGroup == null)
                return;

            //open contact us page
            await Shell.Current.GoToAsync($"{nameof(MessagesPage)}?{nameof(MessageViewModel.ChatId)}={customerCareChatGroup.id}&{nameof(MessageViewModel.UserId)}={myAccount.id}");
        }

        public async void ExecuteColorOptionTappedCommand(ShoppingProductColorOption shoppingProductColorOption)
        {
            if (shoppingProductColorOption == null)
                return;

            for (int x = 0; x < ShoppingProductColorOptions.Count; x++)
            {
                ShoppingProductColorOption mShoppingProductColorOption = ShoppingProductColorOptions[x];
                if (mShoppingProductColorOption.id == shoppingProductColorOption.id)
                {
                    ShoppingProductColorOptions[x].isDefault = true;
                    Debug.WriteLine("ProductDetailsViewModel: ExecuteColorOptionTappedCommand(), productId: " + shoppingProductColorOption.id);
                }
                else
                {
                    ShoppingProductColorOptions[x].isDefault = false;
                }
            }
            OnPropertyChanged("ShoppingProductColorOptions");
        }

        async void OnAddToCartPageSelected()
        {
            if (shoppingProduct == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(AddToCartPage)}?{nameof(AddToCartViewModel.ProductId)}={shoppingProduct.id}");
        }

        public async void ExecuteSizeOptionTappedCommand(ShoppingProductSizeOption shoppingProductSizeOption)
        {
            if (shoppingProductSizeOption == null)
                return;

            for (int x = 0; x < ShoppingProductSizeOptions.Count; x++)
            {
                ShoppingProductSizeOption mShoppingProductSizeOption = ShoppingProductSizeOptions[x];
                if (mShoppingProductSizeOption.id == shoppingProductSizeOption.id)
                {
                    ShoppingProductSizeOptions[x].isDefault = true;
                    Debug.WriteLine("ProductDetailsViewModel: ExecuteColorOptionTappedCommand(), productId: " + shoppingProductSizeOption.id);
                }
                else
                {
                    ShoppingProductSizeOptions[x].isDefault = false;
                }
            }
            OnPropertyChanged("ShoppingProductColorOptions");
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
                Debug.WriteLine("ProductDetailsViewModel: LoadUserAccount(), productId: " + productId);
                var serializer = new JsonSerializer();
                serializer.Error += (sender, args) =>
                {
                    if (args.ErrorContext.Error is JsonException)
                    {
                        args.ErrorContext.Handled = true;
                    }
                };

                JObject mAccObj = await UserAccount.LoadMyProfileAsync();
                myAccount = mAccObj.ToObject<UserAccount>(serializer);
                OnPropertyChanged("myAccount");

                systemSettings = await SystemSettings.fetchSystemSettings();
                Debug.WriteLine("ProductDetailsViewModel: " + systemSettings.currency);
                OnPropertyChanged("systemSettings");

                JObject serverObj = await ShoppingProduct.fetchShoppingProduct(productId);
                JObject productObj = (JObject)serverObj.GetValue("data");
                shoppingProduct = productObj.ToObject<ShoppingProduct>(serializer);
                shoppingProduct.currency = systemSettings.currency;
                OnPropertyChanged("shoppingProduct");

                JObject colorsObj = await ShoppingProductColorOption.FetchShoppingProductColorOptions(productId);
                JArray colorsObjArray = (JArray)colorsObj.GetValue("data");
                ShoppingProductColorOptions.Clear();

                foreach (JToken token in colorsObjArray)
                {
                    JObject chatObj = (JObject)token;
                    ShoppingProductColorOption _shoppingProductColorOption = chatObj.ToObject<ShoppingProductColorOption>(serializer);
                    ShoppingProductColorOptions.Add(_shoppingProductColorOption);
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

                JObject ccObj = await CustomerCareChatGroup.getCustomerCareChatGroupId();
                customerCareChatGroup = ccObj.ToObject<CustomerCareChatGroup>(serializer);
                IsBusy = false;
            }
            catch (Exception e)
            {
                IsBusy = false;
                Debug.WriteLine("ProductDetailsViewModel: " + e);
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
