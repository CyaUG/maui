using Youth.ViewModels;

namespace Youth.Views.Shopping
{

    public partial class ShopingSubCategoryBrandsProviderPage : ContentPage
    {
        ShopingSubCategoryBrandsProviderViewModel _viewModel;
        public ShopingSubCategoryBrandsProviderPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ShopingSubCategoryBrandsProviderViewModel();
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