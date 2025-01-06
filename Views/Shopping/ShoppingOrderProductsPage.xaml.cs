using Youth.ViewModels;

namespace Youth.Views.Shopping
{

    public partial class ShoppingOrderProductsPage : ContentPage
    {
        ShoppingOrderProductsViewModel _viewModel;
        public ShoppingOrderProductsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ShoppingOrderProductsViewModel();
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