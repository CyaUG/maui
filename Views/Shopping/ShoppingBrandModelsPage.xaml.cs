using Youth.ViewModels;

namespace Youth.Views.Shopping
{

    public partial class ShoppingBrandModelsPage : ContentPage
    {
        ShoppingBrandModelsViewModel _viewModel;
        public ShoppingBrandModelsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ShoppingBrandModelsViewModel();
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