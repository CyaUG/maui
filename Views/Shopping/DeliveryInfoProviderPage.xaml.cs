using Youth.Models;
using Youth.ViewModels;
using Youth.Views.Account;

namespace Youth.Views.Shopping
{

    public partial class DeliveryInfoProviderPage : ContentPage
    {
        DeliveryInfoProviderViewModel _viewModel;
        public DeliveryInfoProviderPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new DeliveryInfoProviderViewModel();

            MessagingCenter.Subscribe<DeliveryInfoProviderViewModel, string>(this, "message", (sender, message) =>
            {
                // Do something with the parameter, "arg" in this case
                DisplayFinalMessage(message);
            });

            MessagingCenter.Subscribe<LocationPickerPage, LocationModule>(this, "location", (sender, location) =>
            {
                // Do something with the parameter, "arg" in this case
                _viewModel.activeLocation = location;
                _viewModel.SetLocationAddress = location.Address;
            });
        }

        private async void DisplayFinalMessage(string message)
        {
            await DisplayAlert("Error", message, "", "OKAY");
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

        private async void OpenSearchPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(SearchProductsPage)}");
        }

        private async void GoToCheckOut(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(CheckOutPage)}");
        }

        private async void OpenShoppingCart(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(ShoppingCartPage)}");
        }
        public async void OpenLocationProviderCommand(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(LocationPickerPage)}");
            }
            catch (Exception ex) { }
        }
    }
}