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
    public class ShoppingCategoriesViewModel : BaseViewModel, IShoppingCategoriesViewModel
    {
        public ShoppingProduct selectedShoppingProduct;
        public ShoppingCategory selectedShoppingCategory;
        public ObservableCollection<ShoppingCategory> ShoppingCategories { get; }
        public Command<ShoppingCategory> CategoriesTapCommand { get; }
        public UserAccount userAccount { get; set; }
        public SystemSettings systemSettings { get; set; }
        public Command ShoppingCategoriesLoadCommand { get; }

        public string Title { get; set; }
        public string Categories { get; set; }

        public ShoppingCategoriesViewModel()
        {
            Title = "Shopping";
            ShoppingCategories = new ObservableCollection<ShoppingCategory>();
            ShoppingCategoriesLoadCommand = new Command(async () => await ExecuteShoppingCategoryCommand());
            CategoriesTapCommand = new Command<ShoppingCategory>(OnShoppingSubCategorySelected);
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

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
            ShoppingCategoriesLoadCommand.Execute(this);
        }

        async Task ExecuteShoppingCategoryCommand()
        {
            Debug.WriteLine("ShoppingCategoriesViewModel: ExecuteShoppingCategoryCommand()");
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
                Debug.WriteLine("ShoppingCategoriesViewModel: " + systemSettings.currency);
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

                IsBusy = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ShoppingCategoriesViewModel: " + ex);
                IsBusy = false;
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("shoppingCategories");
                OnPropertyChanged("IsBusy");
            }
        }
    }
}
