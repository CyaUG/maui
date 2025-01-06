using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views;
using Youth.Views.Shopping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Youth.ViewModels.Base;


namespace Youth.ViewModels
{
    public class MainShoppingViewModel : BaseViewModel, IMainShoppingViewModel
    {
        public ShoppingProduct selectedShoppingProduct;
        public ShoppingCategory selectedShoppingCategory;
        private ShoppingCategory _shoppingCategory1;
        public ShoppingCategory shoppingCategory1
        {
            get { return _shoppingCategory1; }
            set
            {
                _shoppingCategory1 = value;
                OnPropertyChanged();
            }
        }
        public ShoppingCategory _shoppingCategory2;
        public ShoppingCategory shoppingCategory2
        {
            get { return _shoppingCategory2; }
            set
            {
                _shoppingCategory2 = value;
                OnPropertyChanged();
            }
        }
        public ShoppingCategory _shoppingCategory3;
        public ShoppingCategory shoppingCategory3
        {
            get { return _shoppingCategory3; }
            set
            {
                _shoppingCategory3 = value;
                OnPropertyChanged();
            }
        }
        public ShoppingCategory _shoppingCategory4;
        public ShoppingCategory shoppingCategory4
        {
            get { return _shoppingCategory4; }
            set
            {
                _shoppingCategory4 = value;
                OnPropertyChanged();
            }
        }
        public ShoppingCategory _shoppingCategory5;
        public ShoppingCategory shoppingCategory5
        {
            get { return _shoppingCategory5; }
            set
            {
                _shoppingCategory5 = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ShoppingProduct> PopularProducts { get; }
        public ObservableCollection<ShoppingCategory> ShoppingCategories { get; }
        public Command<ShoppingProduct> ProductDetailNavTap { get; }
        public Command<ShoppingCategory> CategoriesTapCommand { get; }
        public UserAccount userAccount { get; set; }
        public SystemSettings systemSettings { get; set; }
        public Command ShoppingCategoriesLoadCommand { get; }
        public Command OpenMoreShoppingCategoriesLoadCommand { get; }

        public string Title { get; set; }
        public string Categories { get; set; }
        public string MostPopularSubtitle { get; set; }
        public string TodaysDealsSubtitle { get; set; }
        public string LatestTechnologySubtitle { get; set; }
        public string LowerTitle { get; set; }

        public MainShoppingViewModel()
        {
            Title = "Shopping";
            Categories = "Categories";
            MostPopularSubtitle = "Most Popular";
            TodaysDealsSubtitle = "Todays Deals";
            LatestTechnologySubtitle = "Latest Technology";
            LowerTitle = "PreOwned";
            ShoppingCategories = new ObservableCollection<ShoppingCategory>();
            PopularProducts = new ObservableCollection<ShoppingProduct>();
            ShoppingCategoriesLoadCommand = new Command(async () => await ExecuteShoppingCategoryCommand());
            OpenMoreShoppingCategoriesLoadCommand = new Command(async () => await OpenMoreShoppingCategories());
            ProductDetailNavTap = new Command<ShoppingProduct>(OnShoppingProductSelected);
            CategoriesTapCommand = new Command<ShoppingCategory>(OnShoppingSubCategorySelected);
        }

        public ShoppingProduct SelectedProduct
        {
            get => selectedShoppingProduct;
            set
            {
                Debug.WriteLine("ChatViewModel: SelectedChat");
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

        public ShoppingCategory SelectedCategory
        {
            get => selectedShoppingCategory;
            set
            {
                Debug.WriteLine("ChatViewModel: SelectedChat");
                SetProperty(ref selectedShoppingCategory, value);
                OnShoppingSubCategorySelected(value);
            }
        }

        async void OnShoppingSubCategorySelected(ShoppingCategory category)
        {
            if (category == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ShoppingSubCategoriesPage)}?{nameof(ShoppingSubCategoryViewModel.CategoryId)}={category.id}");
        }

        async Task OpenMoreShoppingCategories()
        {
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ShoppingCategoriesPage)}");
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
            SelectedProduct = null;
            ShoppingCategoriesLoadCommand.Execute(this);
        }

        async Task ExecuteShoppingCategoryCommand()
        {
            Debug.WriteLine("MainShoppingViewModel: ExecuteShoppingCategoryCommand()");
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

                JObject serverObj = await ShoppingCategory.fetchShoppingCategories();
                JArray categoriesArray = (JArray)serverObj.GetValue("data");
                ShoppingCategories.Clear();

                foreach (JToken token in categoriesArray)
                {
                    JObject chatObj = (JObject)token;
                    ShoppingCategory _categoryModel = chatObj.ToObject<ShoppingCategory>(serializer);
                    ShoppingCategories.Add(_categoryModel);
                }
                OnPropertyChanged("shoppingCategories");

                try
                {
                    Debug.WriteLine("MainShoppingViewModel: fetchShoppingProducts()");
                    JObject productsObj = await ShoppingProduct.getShoppingPopularProducts();
                    JArray productsArray = (JArray)productsObj.GetValue("data");
                    PopularProducts.Clear();

                    foreach (JToken token in productsArray)
                    {
                        JObject productObj = (JObject)token;
                        ShoppingProduct _productModel = productObj.ToObject<ShoppingProduct>(serializer);
                        _productModel.currency = systemSettings.currency;
                        _productModel.offerDiscount = !_productModel.onDiscount;
                        PopularProducts.Add(_productModel);
                        Debug.WriteLine("MainShoppingViewModel: " + _productModel.label);
                    }
                    OnPropertyChanged("PopularProducts");
                }
                catch (Exception e)
                {
                    Debug.WriteLine("MainShoppingViewModel: " + e);
                    IsBusy = false;
                }

                shoppingCategory1 = ShoppingCategories[0];
                _shoppingCategory2 = ShoppingCategories[1];
                _shoppingCategory3 = ShoppingCategories[2];
                _shoppingCategory4 = ShoppingCategories[3];
                _shoppingCategory5 = ShoppingCategories[4];
                Debug.WriteLine("MainShoppingViewModel: " + shoppingCategory1.label);

                IsBusy = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MainShoppingViewModel: " + ex);
                IsBusy = false;
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("PopularProducts");
                OnPropertyChanged("shoppingCategories");
                OnPropertyChanged("shoppingCategory1");
                OnPropertyChanged("shoppingCategory2");
                OnPropertyChanged("shoppingCategory3");
                OnPropertyChanged("shoppingCategory4");
                OnPropertyChanged("shoppingCategory5");
                OnPropertyChanged("IsBusy");
            }
        }
    }
}
