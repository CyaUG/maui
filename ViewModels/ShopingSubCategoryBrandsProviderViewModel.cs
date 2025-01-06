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
    public class ShopingSubCategoryBrandsProviderViewModel : BaseViewModel, IShopingSubCategoryBrandsProviderViewModel
    {
        private int categoryId;
        private int subCategoryId;
        public ShoppingQueryParameter shoppingQueryParameter { get; set; }
        public ObservableCollection<ShoppingBrand> ShoppingBrands { get; }
        public Command<ShoppingBrand> ShoppingBrandTapCommand { get; }
        private ShoppingBrand selectedShoppingBrand;

        public ShopingSubCategoryBrandsProviderViewModel()
        {
            Title = "Brands";
            ShoppingBrands = new ObservableCollection<ShoppingBrand>();
            ShoppingBrandTapCommand = new Command<ShoppingBrand>(OnShoppingBrandSelected);
        }

        public ShoppingBrand SelectedShoppingBrand
        {
            get => selectedShoppingBrand;
            set
            {
                Debug.WriteLine("ChatViewModel: SelectedChat");
                SetProperty(ref selectedShoppingBrand, value);
                OnShoppingBrandSelected(value);
            }
        }

        void OnShoppingBrandSelected(ShoppingBrand shoppingBrand)
        {
            if (shoppingBrand == null)
                return;

            MessagingCenter.Send<ShopingSubCategoryBrandsProviderViewModel, ShoppingBrand>(this, "shoppingBrand", shoppingBrand);
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

                JObject paramObj = await ShoppingQueryParameter.getShoppingQueryParameter(subCategoryId);
                JObject parameterObj = (JObject)paramObj.GetValue("data");
                shoppingQueryParameter = parameterObj.ToObject<ShoppingQueryParameter>(serializer);
                OnPropertyChanged("shoppingQueryParameter");

                JObject serverObj = await ShoppingBrand.getShopingSubCategoryBrands(subCategoryId);
                JArray brandsArray = (JArray)serverObj.GetValue("data");
                ShoppingBrands.Clear();

                foreach (JToken token in brandsArray)
                {
                    JObject chatObj = (JObject)token;
                    ShoppingBrand brandsModel = chatObj.ToObject<ShoppingBrand>(serializer);
                    Debug.WriteLine("SubCategoryItemsCustomViewModel: " + brandsModel.label);
                    ShoppingBrands.Add(brandsModel);
                }
                OnPropertyChanged("ShoppingBrands");

                IsBusy = false;
                OnPropertyChanged("IsBusy");
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
                OnPropertyChanged("ShoppingBrands");
                OnPropertyChanged("IsBusy");
            }
        }
    }
}
