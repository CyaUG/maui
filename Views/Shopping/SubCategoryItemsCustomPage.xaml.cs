using Youth.Models;
using Youth.ViewModels;
using System.Diagnostics;

namespace Youth.Views.Shopping
{

    public partial class SubCategoryItemsCustomPage : ContentPage
    {
        SubCategoryItemsCustomViewModel _viewModel;
        public SubCategoryItemsCustomPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new SubCategoryItemsCustomViewModel();

            MessagingCenter.Subscribe<SubCategoryItemsCustomViewModel, ShoppingSubCategory>(this, "shoppingSubCategory", (sender, shoppingSubCategory) =>
            {
                // Do something with the parameter, "arg" in this case
                Debug.WriteLine("SubCategoryItemsCustomPage: SubCategoryItemsCustomPage() -> " + shoppingSubCategory.label);
                _viewModel.MessagingCenterSelectedShoppingSubCategory = shoppingSubCategory;
            });

            MessagingCenter.Subscribe<ShopingSubCategoryBrandsProviderViewModel, ShoppingBrand>(this, "shoppingBrand", (sender, shoppingBrand) =>
            {
                // Do something with the parameter, "arg" in this case
                Debug.WriteLine("SubCategoryItemsCustomPage: SubCategoryItemsCustomPage() -> " + shoppingBrand.label);
                _viewModel.MessagingCenterSelectedShoppingBrand = shoppingBrand;
            });

            MessagingCenter.Subscribe<ShoppingBrandModelsViewModel, ShoppingBrandModel>(this, "shoppingBrandModel", (sender, shoppingBrandModel) =>
            {
                // Do something with the parameter, "arg" in this case
                Debug.WriteLine("SubCategoryItemsCustomPage: SubCategoryItemsCustomPage() -> " + shoppingBrandModel.label);
                _viewModel.MessagingCenterSelectedShoppingBrandModel = shoppingBrandModel;
            });



        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OpenShoppingCart(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(ShoppingCartPage)}");
        }

        private async void OpenSearchPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(SearchProductsPage)}");
        }
    }
}