using Youth.Models;
using Youth.ViewModels;

namespace Youth.Views.Shopping
{

    public partial class CheckOutPage : ContentPage
    {
        CheckOutViewModel _viewModel;
        public CheckOutPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new CheckOutViewModel();

            MessagingCenter.Subscribe<DeliveryInfoProviderViewModel, DeliveryAddress>(this, "deliveryAddress", (sender, deliveryAddress) =>
            {
                // Do something with the parameter, "arg" in this case
                _viewModel.deliveryAddress = deliveryAddress;
            });

            MessagingCenter.Subscribe<CheckOutViewModel, string>(this, "message", (sender, message) =>
            {
                // Do something with the parameter, "arg" in this case
                DisplayFinalMessage(message);
            });
        }

        private async void DisplayFinalMessage(string message)
        {
            await DisplayAlert("Success", message, "", "OKAY");

            //var AppShell = new AppShell();
            //var app = Application.Current;
            //app.MainPage = AppShell;
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
    }
}