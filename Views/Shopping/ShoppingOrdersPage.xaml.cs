using Youth.ViewModels;
using Youth.ViewModels.Interfaces;

namespace Youth.Views.Shopping
{

    public partial class ShoppingOrdersPage : ContentPage
    {
        IShoppingOrdersViewModel _viewModel;
        public ShoppingOrdersPage()
        {
            InitializeComponent();
            // Resolve or set a default view model
            _viewModel = DependencyService.Get<IShoppingOrdersViewModel>() ?? new ShoppingOrdersViewModel();
            BindingContext = _viewModel;
        }
        public ShoppingOrdersPage(IShoppingOrdersViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
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