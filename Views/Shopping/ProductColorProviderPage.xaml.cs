using Youth.ViewModels;

namespace Youth.Views.Shopping
{

    public partial class ProductColorProviderPage : ContentPage
    {
        ProductColorProviderViewModel _viewModel;
        public ProductColorProviderPage()
        {
            //ProductColorProviderViewModel
            InitializeComponent();
            BindingContext = _viewModel = new ProductColorProviderViewModel();
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