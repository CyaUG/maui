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
    [QueryProperty(nameof(CategoryId), nameof(CategoryId))]
    public class ShoppingSubCategoryViewModel : BaseViewModel, IShoppingSubCategoryViewModel
    {
        private int categoryId;

        public ShoppingSubCategory selectedShoppingSubCategory;
        public ObservableCollection<ShoppingSubCategory> shoppingSubCategories { get; }
        public Command<ShoppingSubCategory> SubCategoriesTapCommand { get; }

        public ShoppingSubCategoryViewModel()
        {
            Title = "Sub Categories";
            shoppingSubCategories = new ObservableCollection<ShoppingSubCategory>();
            SubCategoriesTapCommand = new Command<ShoppingSubCategory>(OnShoppingSubCategorySelected);
        }

        public ShoppingSubCategory SelectedCategory
        {
            get => selectedShoppingSubCategory;
            set
            {
                Debug.WriteLine("ChatViewModel: SelectedChat");
                SetProperty(ref selectedShoppingSubCategory, value);
                OnShoppingSubCategorySelected(value);
            }
        }

        async void OnShoppingSubCategorySelected(ShoppingSubCategory subCategory)
        {
            if (subCategory == null)
                return;

            if (subCategory.isFinal)
            {
                await Shell.Current.GoToAsync($"{nameof(SubCategoryItemsDefaultPage)}?{nameof(SubCategoryItemsDefaultViewModel.SubCategoryId)}={subCategory.id}&{nameof(SubCategoryItemsDefaultViewModel.CategoryId)}={categoryId}");
            }
            else
            {
                await Shell.Current.GoToAsync($"{nameof(SubCategoryItemsCustomPage)}?{nameof(SubCategoryItemsCustomViewModel.SubCategoryId)}={subCategory.id}&{nameof(SubCategoryItemsDefaultViewModel.CategoryId)}={categoryId}");
            }
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
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
                ShoppingSubCategories(value);
            }
        }

        public async void ShoppingSubCategories(int categoryId)
        {
            try
            {
                Debug.WriteLine("ShoppingSubCategoryViewModel: LoadCategory(), productId: " + categoryId);
                var serializer = new JsonSerializer();
                serializer.Error += (sender, args) =>
                {
                    if (args.ErrorContext.Error is JsonException)
                    {
                        args.ErrorContext.Handled = true;
                    }
                };

                JObject serverObj = await ShoppingSubCategory.fetchShoppingSubCategories(categoryId);
                JArray chatsArray = (JArray)serverObj.GetValue("data");
                shoppingSubCategories.Clear();

                foreach (JToken token in chatsArray)
                {
                    JObject chatObj = (JObject)token;
                    ShoppingSubCategory subCategoryModel = chatObj.ToObject<ShoppingSubCategory>(serializer);
                    Debug.WriteLine("ShoppingSubCategoryViewModel: " + subCategoryModel.label);
                    shoppingSubCategories.Add(subCategoryModel);
                }
                OnPropertyChanged("shoppingSubCategories");
                IsBusy = false;
            }
            catch (Exception e)
            {
                IsBusy = false;
                Debug.WriteLine("ShoppingSubCategoryViewModel: Error: " + e);
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("shoppingSubCategories");
                OnPropertyChanged("IsBusy");
            }
        }
    }
}
