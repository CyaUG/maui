using Youth.ViewModels;
using Youth.ViewModels.Interfaces;

namespace Youth.Views.Shopping
{

    public partial class ShoppingWishListPage : ContentPage
    {
        IShoppingWishListViewModel _viewModel;
        public ShoppingWishListPage()
        {
            InitializeComponent();
            // Resolve or set a default view model
            _viewModel = DependencyService.Get<IShoppingWishListViewModel>() ?? new ShoppingWishListViewModel();
            BindingContext = _viewModel;
        }
        public ShoppingWishListPage(IShoppingWishListViewModel viewModel)
        {
            InitializeComponent();
            //ShoppingWishListViewModel

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