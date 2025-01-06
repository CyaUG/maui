using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.Shopping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Youth.ViewModels.Base;


namespace Youth.ViewModels
{
    [QueryProperty(nameof(CategoryId), nameof(CategoryId))]
    [QueryProperty(nameof(SubCategoryId), nameof(SubCategoryId))]
    internal class SubCategoryItemsDefaultViewModel : BaseViewModel, ISubCategoryItemsDefaultViewModel
    {
        private int categoryId;
        private int subCategoryId;
        public ObservableCollection<ShoppingBrand> ShoppingBrands { get; }
        public SystemSettings systemSettings { get; set; }
        public ObservableCollection<ShoppingProduct> ShoppingProducts { get; }
        private ShoppingProduct selectedShoppingProduct;
        private ShoppingBrand _shoppingBrand1;
        public ShoppingBrand shoppingBrand1
        {
            get { return _shoppingBrand1; }
            set
            {
                _shoppingBrand1 = value;
                OnPropertyChanged();
            }
        }
        public ShoppingBrand _shoppingBrand2;
        public ShoppingBrand shoppingBrand2
        {
            get { return _shoppingBrand2; }
            set
            {
                _shoppingBrand2 = value;
                OnPropertyChanged();
            }
        }
        public ShoppingBrand _shoppingBrand3;
        public ShoppingBrand shoppingBrand3
        {
            get { return _shoppingBrand3; }
            set
            {
                _shoppingBrand3 = value;
                OnPropertyChanged();
            }
        }
        public ShoppingBrand _shoppingBrand4;
        public ShoppingBrand shoppingBrand4
        {
            get { return _shoppingBrand4; }
            set
            {
                _shoppingBrand4 = value;
                OnPropertyChanged();
            }
        }
        public ShoppingBrand _shoppingBrand5;
        public ShoppingBrand shoppingBrand5
        {
            get { return _shoppingBrand5; }
            set
            {
                _shoppingBrand5 = value;
                OnPropertyChanged();
            }
        }
        public Command<ShoppingProduct> ShoppingProductTapCommand { get; }
        public Command OpenMoreShoppingCategoriesLoadCommand { get; }

        public SubCategoryItemsDefaultViewModel()
        {
            Title = "Sub Categories";
            ShoppingBrands = new ObservableCollection<ShoppingBrand>();
            ShoppingProducts = new ObservableCollection<ShoppingProduct>();
            ShoppingProductTapCommand = new Command<ShoppingProduct>(OnShoppingSubCategorySelected);
            OpenMoreShoppingCategoriesLoadCommand = new Command(async () => await OpenMoreShoppingCategoriesLoad());
        }

        async Task OpenMoreShoppingCategoriesLoad()
        {
            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ShoppingCategoriesPage)}");
        }

        public ShoppingProduct SelectedShoppingProduct
        {
            get => selectedShoppingProduct;
            set
            {
                Debug.WriteLine("ChatViewModel: SelectedChat");
                SetProperty(ref selectedShoppingProduct, value);
                OnShoppingSubCategorySelected(value);
            }
        }

        async void OnShoppingSubCategorySelected(ShoppingProduct shoppingProduct)
        {
            if (shoppingProduct == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}?{nameof(ProductDetailsViewModel.ProductId)}={shoppingProduct.id}");
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            /*Set defaults*/
        }

        public int CategoryId
        {
            get
            {
                return categoryId;
            }
            set
            {
                categoryId = value;
                LoadShoppingBrands(value);
            }
        }

        public int SubCategoryId
        {
            get
            {
                return subCategoryId;
            }
            set
            {
                subCategoryId = value;
                LoadSubCategoryShoppingProducts(value);
            }
        }

        public async void LoadShoppingBrands(int categoryId)
        {
            try
            {
                Debug.WriteLine("SubCategoryItemsDefaultViewModel: LoadShoppingBrands(), productId: " + categoryId);
                var serializer = new JsonSerializer();
                serializer.Error += (sender, args) =>
                {
                    if (args.ErrorContext.Error is JsonException)
                    {
                        args.ErrorContext.Handled = true;
                    }
                };

                JObject serverObj = await ShoppingBrand.fetchShoppingBrands(categoryId);
                JArray brandsArray = (JArray)serverObj.GetValue("data");
                ShoppingBrands.Clear();

                foreach (JToken token in brandsArray)
                {
                    JObject chatObj = (JObject)token;
                    ShoppingBrand brandsModel = chatObj.ToObject<ShoppingBrand>(serializer);
                    Debug.WriteLine("SubCategoryItemsDefaultViewModel: " + brandsModel.label);
                    ShoppingBrands.Add(brandsModel);
                }

                shoppingBrand1 = ShoppingBrands[0];
                _shoppingBrand2 = ShoppingBrands[1];
                _shoppingBrand3 = ShoppingBrands[2];
                _shoppingBrand4 = ShoppingBrands[3];
                _shoppingBrand5 = ShoppingBrands[4];
                Debug.WriteLine("SubCategoryItemsDefaultViewModel: " + shoppingBrand1.label);
                OnPropertyChanged("shoppingSubCategories");
                IsBusy = false;
            }
            catch (Exception e)
            {
                IsBusy = false;
                Debug.WriteLine("SubCategoryItemsDefaultViewModel: " + e);
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("shoppingProduct");
                OnPropertyChanged("shoppingBrand1");
                OnPropertyChanged("shoppingBrand2");
                OnPropertyChanged("shoppingBrand3");
                OnPropertyChanged("shoppingBrand4");
                OnPropertyChanged("shoppingBrand5");
                OnPropertyChanged("IsBusy");
            }
        }

        public async void LoadSubCategoryShoppingProducts(int subCategoryId)
        {
            try
            {
                Debug.WriteLine("SubCategoryItemsDefaultViewModel: LoadSubCategoryShoppingProducts(), productId: " + subCategoryId);
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

                JObject serverObj = await ShoppingProduct.fetchSubCategoryShoppingProducts(subCategoryId);
                JArray chatsArray = (JArray)serverObj.GetValue("data");
                ShoppingProducts.Clear();

                foreach (JToken token in chatsArray)
                {
                    JObject chatObj = (JObject)token;
                    ShoppingProduct ShoppingProductModel = chatObj.ToObject<ShoppingProduct>(serializer);
                    Debug.WriteLine("SubCategoryItemsDefaultViewModel: " + ShoppingProductModel.label);
                    ShoppingProductModel.currency = systemSettings.currency;
                    ShoppingProducts.Add(ShoppingProductModel);
                }
                OnPropertyChanged("ShoppingProducts");
                IsBusy = false;
            }
            catch (Exception e)
            {
                IsBusy = false;
                Debug.WriteLine("SubCategoryItemsDefaultViewModel: Error: " + e);
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("ShoppingProducts");
                OnPropertyChanged("IsBusy");
            }
        }
    }
}
