using Youth.Models;
using Youth.ViewModels;

namespace Youth.Views.Shopping
{

    public partial class AddToCartPage : ContentPage
    {
        AddToCartViewModel _viewModel;
        public AddToCartPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new AddToCartViewModel();

            MessagingCenter.Subscribe<ProductColorProviderViewModel, ShoppingProductColorOption>(this, "shoppingProductColorOption", (sender, shoppingProductColorOption) =>
            {
                // Do something with the parameter, "arg" in this case
                _viewModel.mShoppingProductColorOption = shoppingProductColorOption;
            });

            MessagingCenter.Subscribe<ProductSizeProviderViewModel, ShoppingProductSizeOption>(this, "shoppingProductSizeOption", (sender, shoppingProductSizeOption) =>
            {
                // Do something with the parameter, "arg" in this case
                _viewModel.mShoppingProductSizeOption = shoppingProductSizeOption;
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
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