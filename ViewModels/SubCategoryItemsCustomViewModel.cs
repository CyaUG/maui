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
    public class SubCategoryItemsCustomViewModel : BaseViewModel, ISubCategoryItemsCustomViewModel
    {
        private int categoryId;
        private int subCategoryId;
        public ShoppingQueryParameter shoppingQueryParameter { get; set; }
        public string FormattedLabel { get; set; }
        public string FormattedBrandLabel { get; set; }
        public string FormattedBrandModelLabel { get; set; }
        public SystemSettings systemSettings { get; set; }
        public ObservableCollection<ShoppingSubCategory> ShoppingSubCategories { get; }
        public ObservableCollection<ShoppingProduct> ShoppingProducts { get; }
        private ShoppingProduct selectedShoppingProduct;
        private ShoppingSubCategory selectedShoppingSubCategory;
        private ShoppingBrand selectedShoppingBrand;
        private ShoppingBrandModel selectedShoppingBrandModel;
        private ShoppingSubCategory _shoppingSubCategory1;
        public ShoppingSubCategory shoppingSubCategory1
        {
            get { return _shoppingSubCategory1; }
            set
            {
                _shoppingSubCategory1 = value;
                OnPropertyChanged();
            }
        }
        public ShoppingSubCategory _shoppingSubCategory2;
        public ShoppingSubCategory shoppingSubCategory2
        {
            get { return _shoppingSubCategory2; }
            set
            {
                _shoppingSubCategory2 = value;
                OnPropertyChanged();
            }
        }
        public ShoppingSubCategory _shoppingSubCategory3;
        public ShoppingSubCategory shoppingSubCategory3
        {
            get { return _shoppingSubCategory3; }
            set
            {
                _shoppingSubCategory3 = value;
                OnPropertyChanged();
            }
        }
        public ShoppingSubCategory _shoppingSubCategory4;
        public ShoppingSubCategory shoppingSubCategory4
        {
            get { return _shoppingSubCategory4; }
            set
            {
                _shoppingSubCategory4 = value;
                OnPropertyChanged();
            }
        }
        public ShoppingSubCategory _shoppingSubCategory5;
        public ShoppingSubCategory shoppingSubCategory5
        {
            get { return _shoppingSubCategory5; }
            set
            {
                _shoppingSubCategory5 = value;
                OnPropertyChanged();
            }
        }
        public Command<ShoppingProduct> ShoppingProductTapCommand { get; }
        public Command<ShoppingSubCategory> SubCategoriesTapCommand { get; }
        public Command<ShoppingSubCategory> SetSubCategoriesTapCommand { get; }
        public Command OpenMoreShoppingSubCategoriesLoadCommand { get; }
        public Command OpenShopingSubCategoryBrandsProviderCommand { get; }
        public Command OpenShoppingBrandModelsCommand { get; }

        public SubCategoryItemsCustomViewModel()
        {
            Title = "Sub Categories";
            ShoppingProducts = new ObservableCollection<ShoppingProduct>();
            ShoppingSubCategories = new ObservableCollection<ShoppingSubCategory>();
            ShoppingProductTapCommand = new Command<ShoppingProduct>(OnShoppingProductSelected);
            SubCategoriesTapCommand = new Command<ShoppingSubCategory>(OnShoppingSubCategorySelected);
            SetSubCategoriesTapCommand = new Command<ShoppingSubCategory>(OnSetShoppingSubCategorySelected);
            OpenMoreShoppingSubCategoriesLoadCommand = new Command(async () => await OpenMoreShoppingSubCategoriesLoad());
            OpenShopingSubCategoryBrandsProviderCommand = new Command(async () => await OpenShopingSubCategoryBrandsProviderCommandLoad());
            OpenShoppingBrandModelsCommand = new Command(async () => await OpenShoppingBrandModelsCommandLoad());
        }

        async Task OpenMoreShoppingSubCategoriesLoad()
        {
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ChildSubCategoriesPage)}?{nameof(SubCategoryItemsCustomViewModel.SubCategoryId)}={subCategoryId}&{nameof(SubCategoryItemsCustomViewModel.CategoryId)}={categoryId}");
        }

        async Task OpenShopingSubCategoryBrandsProviderCommandLoad()
        {
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ShopingSubCategoryBrandsProviderPage)}?{nameof(ShopingSubCategoryBrandsProviderViewModel.SubCategoryId)}={subCategoryId}&{nameof(ShopingSubCategoryBrandsProviderViewModel.CategoryId)}={categoryId}");
        }

        async Task OpenShoppingBrandModelsCommandLoad()
        {
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ShoppingBrandModelsPage)}?{nameof(ShoppingBrandModelsViewModel.BrandId)}={selectedShoppingBrand.id}&{nameof(ShoppingBrandModelsViewModel.SubCategoryId)}={subCategoryId}");
        }

        public ShoppingProduct SelectedShoppingProduct
        {
            get => selectedShoppingProduct;
            set
            {
                Debug.WriteLine("ChatViewModel: SelectedChat");
                SetProperty(ref selectedShoppingProduct, value);
                OnShoppingProductSelected(value);
            }
        }

        async void OnShoppingProductSelected(ShoppingProduct shoppingProduct)
        {
            if (shoppingProduct == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}?{nameof(ProductDetailsViewModel.ProductId)}={shoppingProduct.id}");
        }

        public ShoppingSubCategory MessagingCenterSelectedShoppingSubCategory
        {
            get => selectedShoppingSubCategory;
            set
            {
                SetProperty(ref selectedShoppingSubCategory, value);
                Debug.WriteLine("SubCategoryItemsCustomPage: MessagingCenter()" + selectedShoppingSubCategory.label);

                FormattedLabel = selectedShoppingSubCategory.label;
                OnPropertyChanged("FormattedLabel");
                filterSubCategoryShoppingProducts();
            }
        }

        public ShoppingBrand MessagingCenterSelectedShoppingBrand
        {
            get => selectedShoppingBrand;
            set
            {
                SetProperty(ref selectedShoppingBrand, value);
                Debug.WriteLine("SubCategoryItemsCustomPage: MessagingCenter()" + selectedShoppingBrand.label);

                FormattedBrandLabel = selectedShoppingBrand.label;
                OnPropertyChanged("FormattedBrandLabel");
                filterSubCategoryShoppingProducts();
            }
        }

        public ShoppingBrandModel MessagingCenterSelectedShoppingBrandModel
        {
            get => selectedShoppingBrandModel;
            set
            {
                SetProperty(ref selectedShoppingBrandModel, value);
                Debug.WriteLine("SubCategoryItemsCustomPage: MessagingCenter()" + selectedShoppingBrandModel.label);

                FormattedBrandModelLabel = selectedShoppingBrandModel.label;
                OnPropertyChanged("FormattedBrandModelLabel");
                filterSubCategoryShoppingProducts();
            }
        }

        public ShoppingSubCategory SelectedShoppingSubCategory
        {
            get => selectedShoppingSubCategory;
            set
            {
                Debug.WriteLine("ChatViewModel: SelectedChat");
                SetProperty(ref selectedShoppingSubCategory, value);
                OnShoppingSubCategorySelected(value);
                OnSetShoppingSubCategorySelected(value);
            }
        }

        void OnSetShoppingSubCategorySelected(ShoppingSubCategory shoppingSubCategory)
        {
            if (shoppingSubCategory == null)
                return;
            MessagingCenter.Send<SubCategoryItemsCustomViewModel, ShoppingSubCategory>(this, "shoppingSubCategory", shoppingSubCategory);
        }

        void OnShoppingSubCategorySelected(ShoppingSubCategory shoppingSubCategory)
        {
            if (shoppingSubCategory == null)
                return;

            MessagingCenter.Send<SubCategoryItemsCustomViewModel, ShoppingSubCategory>(this, "shoppingSubCategory", shoppingSubCategory);
            GoBack();

        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
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
                LoadShoppingQueryParameter(value);
            }
        }

        public async void LoadShoppingBrands(int categoryId)
        {

        }

        public async void LoadShoppingQueryParameter(int subCategoryId)
        {
            Debug.WriteLine("SubCategoryItemsCustomViewModel: LoadShoppingQueryParameter()");
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

                JObject paramObj = await ShoppingQueryParameter.getShoppingQueryParameter(subCategoryId);
                JObject parameterObj = (JObject)paramObj.GetValue("data");
                shoppingQueryParameter = parameterObj.ToObject<ShoppingQueryParameter>(serializer);

                FormattedLabel = selectedShoppingSubCategory == null ? shoppingQueryParameter.label : selectedShoppingSubCategory.label;
                FormattedBrandLabel = selectedShoppingBrand == null ? shoppingQueryParameter.brandLabel : selectedShoppingBrand.label;
                FormattedBrandModelLabel = selectedShoppingBrandModel == null ? shoppingQueryParameter.brandModelLabel : selectedShoppingBrandModel.label;
                OnPropertyChanged("FormattedLabel");
                OnPropertyChanged("FormattedBrandLabel");
                OnPropertyChanged("FormattedBrandModelLabel");


                JObject serverObj = await ShoppingSubCategory.getShoppingChildSubCategories(subCategoryId);
                JArray brandsArray = (JArray)serverObj.GetValue("data");
                ShoppingSubCategories.Clear();

                foreach (JToken token in brandsArray)
                {
                    JObject chatObj = (JObject)token;
                    ShoppingSubCategory brandsModel = chatObj.ToObject<ShoppingSubCategory>(serializer);
                    Debug.WriteLine("SubCategoryItemsCustomViewModel: " + brandsModel.label);
                    ShoppingSubCategories.Add(brandsModel);
                }
                OnPropertyChanged("ShoppingSubCategories");

                _shoppingSubCategory1 = ShoppingSubCategories[0];
                _shoppingSubCategory2 = ShoppingSubCategories[1];
                _shoppingSubCategory3 = ShoppingSubCategories[2];
                _shoppingSubCategory4 = ShoppingSubCategories[3];
                _shoppingSubCategory5 = ShoppingSubCategories[4];

                JObject prodServerObj = await ShoppingProduct.fetchSubCategoryShoppingProducts(subCategoryId);
                JArray chatsArray = (JArray)prodServerObj.GetValue("data");
                ShoppingProducts.Clear();

                foreach (JToken token in chatsArray)
                {
                    JObject chatObj = (JObject)token;
                    ShoppingProduct ShoppingProductModel = chatObj.ToObject<ShoppingProduct>(serializer);
                    Debug.WriteLine("SubCategoryItemsCustomViewModel: " + ShoppingProductModel.label);
                    ShoppingProductModel.currency = systemSettings.currency;
                    ShoppingProductModel.offerDiscount = !ShoppingProductModel.onDiscount;
                    ShoppingProducts.Add(ShoppingProductModel);
                }
                OnPropertyChanged("ShoppingProducts");

                IsBusy = false;
                OnPropertyChanged("IsBusy");
                if (selectedShoppingSubCategory != null) { filterSubCategoryShoppingProducts(); }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SubCategoryItemsCustomViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("shoppingQueryParameter");
                OnPropertyChanged("ShoppingSubCategories");
                OnPropertyChanged("ShoppingProducts");
                OnPropertyChanged("shoppingSubCategory1");
                OnPropertyChanged("shoppingSubCategory2");
                OnPropertyChanged("shoppingSubCategory3");
                OnPropertyChanged("shoppingSubCategory4");
                OnPropertyChanged("shoppingSubCategory5");
                OnPropertyChanged("IsBusy");
                OnPropertyChanged("FormattedLabel");
                OnPropertyChanged("FormattedBrandLabel");
                OnPropertyChanged("FormattedBrandModelLabel");
            }
        }

        public async void filterSubCategoryShoppingProducts()
        {
            Debug.WriteLine("SubCategoryItemsCustomViewModel: LoadShoppingQueryParameter()");
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

                JObject prodServerObj = await ShoppingProduct.filterSubCategoryShoppingProducts(
                    selectedShoppingSubCategory != null ? selectedShoppingSubCategory.id + "" : "",
                        selectedShoppingBrand != null ? selectedShoppingBrand.id + "" : "",
                        selectedShoppingBrandModel != null ? selectedShoppingBrandModel.id + "" : "");

                JArray chatsArray = (JArray)prodServerObj.GetValue("data");
                ShoppingProducts.Clear();

                foreach (JToken token in chatsArray)
                {
                    JObject chatObj = (JObject)token;
                    ShoppingProduct ShoppingProductModel = chatObj.ToObject<ShoppingProduct>(serializer);
                    Debug.WriteLine("SubCategoryItemsCustomViewModel: " + ShoppingProductModel.label);
                    ShoppingProductModel.currency = systemSettings.currency;
                    ShoppingProductModel.offerDiscount = !ShoppingProductModel.onDiscount;
                    ShoppingProducts.Add(ShoppingProductModel);
                }
                OnPropertyChanged("ShoppingProducts");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SubCategoryItemsCustomViewModel: " + ex);
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
    }
}
