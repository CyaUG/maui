using Youth.ViewModels;

namespace Youth.Views.Shopping
{

    public partial class SearchProductsPage : ContentPage
    {
        SearchProductsViewModel _viewModel;
        public SearchProductsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new SearchProductsViewModel();
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
    }
}