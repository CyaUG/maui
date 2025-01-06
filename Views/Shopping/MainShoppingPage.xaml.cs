using Youth.ViewModels;
using Youth.ViewModels.Interfaces;

namespace Youth.Views.Shopping
{

    public partial class MainShoppingPage : ContentPage
    {
        IMainShoppingViewModel _viewModel;
        // Parameterless constructor for XAML
        public MainShoppingPage()
        {
            InitializeComponent();
            // Resolve or set a default view model
            _viewModel = DependencyService.Get<IMainShoppingViewModel>() ?? new MainShoppingViewModel();
            BindingContext = _viewModel;
        }

        public MainShoppingPage(IMainShoppingViewModel viewModel)
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

        private async void OpenShoppingWishList(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(ShoppingWishListPage)}");
        }

        private async void OpenShoppingOrders(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(ShoppingOrdersPage)}");
        }

        private async void OpenSearchPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(SearchProductsPage)}");
        }
    }
}