using Youth.ViewModels;

namespace Youth.Views.Shopping
{

    public partial class ProductDetailsPage : ContentPage
    {
        ProductDetailsViewModel _viewModel;
        public ProductDetailsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ProductDetailsViewModel();
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