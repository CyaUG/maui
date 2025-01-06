using Youth.Models;
using Youth.ViewModels;
using System.Diagnostics;

namespace Youth.Views.Shopping
{

    public partial class ChildSubCategoriesPage : ContentPage
    {
        SubCategoryItemsCustomViewModel _viewModel;
        public ChildSubCategoriesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new SubCategoryItemsCustomViewModel();
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

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //var productId = _viewModel.SelectedShoppingSubCategory.id;
            Debug.WriteLine("ChildSubCategoriesPage: OnDisappearing");
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