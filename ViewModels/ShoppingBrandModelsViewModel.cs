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
    [QueryProperty(nameof(BrandId), nameof(BrandId))]
    [QueryProperty(nameof(SubCategoryId), nameof(SubCategoryId))]
    public class ShoppingBrandModelsViewModel : BaseViewModel, IShoppingBrandModelsViewModel
    {
        private int brandId;
        private int subCategoryId;
        public ShoppingQueryParameter shoppingQueryParameter { get; set; }
        public ObservableCollection<ShoppingBrandModel> ShoppingBrandModels { get; }
        public Command<ShoppingBrandModel> ShoppingBrandTapCommand { get; }
        private ShoppingBrandModel selectedShoppingBrandModel;

        public ShoppingBrandModelsViewModel()
        {
            Title = "Brands";
            ShoppingBrandModels = new ObservableCollection<ShoppingBrandModel>();
            ShoppingBrandTapCommand = new Command<ShoppingBrandModel>(OnShoppingBrandSelected);
        }

        public ShoppingBrandModel SelectedShoppingBrandModel
        {
            get => selectedShoppingBrandModel;
            set
            {
                Debug.WriteLine("ChatViewModel: SelectedChat");
                SetProperty(ref selectedShoppingBrandModel, value);
                OnShoppingBrandSelected(value);
            }
        }

        void OnShoppingBrandSelected(ShoppingBrandModel shoppingBrandModel)
        {
            if (shoppingBrandModel == null)
                return;

            MessagingCenter.Send<ShoppingBrandModelsViewModel, ShoppingBrandModel>(this, "shoppingBrandModel", shoppingBrandModel);
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

        public int SubCategoryId
        {
            get
            {
                return subCategoryId;
            }
            set
            {
                subCategoryId = value;
            }
        }

        public int BrandId
        {
            get
            {
                return brandId;
            }
            set
            {
                brandId = value;
                LoadShoppingBrandModel(value);
            }
        }

        public async void LoadShoppingBrandModel(int brandId)
        {
            Debug.WriteLine("ShoppingBrandModelsViewModel: LoadShoppingQueryParameter()");
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

                JObject serverObj = await ShoppingBrandModel.fetchShoppingBrandModels(brandId);
                JArray brandsArray = (JArray)serverObj.GetValue("data");
                ShoppingBrandModels.Clear();

                foreach (JToken token in brandsArray)
                {
                    JObject chatObj = (JObject)token;
                    ShoppingBrandModel brandsModel = chatObj.ToObject<ShoppingBrandModel>(serializer);
                    Debug.WriteLine("ShoppingBrandModelsViewModel: " + brandsModel.label);
                    ShoppingBrandModels.Add(brandsModel);
                }
                OnPropertyChanged("ShoppingBrandModels");

                JObject paramObj = await ShoppingQueryParameter.getShoppingQueryParameter(subCategoryId);
                JObject parameterObj = (JObject)paramObj.GetValue("data");
                shoppingQueryParameter = parameterObj.ToObject<ShoppingQueryParameter>(serializer);
                OnPropertyChanged("shoppingQueryParameter");

                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ShoppingBrandModelsViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("shoppingQueryParameter");
                OnPropertyChanged("ShoppingBrandModels");
                OnPropertyChanged("IsBusy");
            }
        }
    }
}
