using Youth.ViewModels;
using Youth.ViewModels.Interfaces;
using ZXing;
using ZXing.Net.Maui;

namespace Youth.Views.ReferralProgram
{

    public partial class LinkReferralProfilePage : ContentPage
    {
        ILinkReferralProfileViewModel _viewModel;
        public LinkReferralProfilePage(ILinkReferralProfileViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;

            MessagingCenter.Subscribe<LinkReferralProfileViewModel, string>(this, "message", (sender, message) =>
            {
                // Do something with the parameter, "arg" in this case
                DisplayFinalMessage(message);
            });

            MessagingCenter.Subscribe<LinkReferralProfileViewModel, bool>(this, "isSeekerRunning", (sender, isSeekerRunning) =>
            {
                // Do something with the parameter, "arg" in this case
                ActivityIndicator.IsRunning = isSeekerRunning;
            });
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

        private void Handle_OnScanResult(object sender, BarcodeDetectionEventArgs e)
        {
            var result = e.Results?.FirstOrDefault();
            if (result is null) return;

            Dispatcher.DispatchAsync(async () =>
            {
                _viewModel.OnBarcodeDetected(result.Value);
            });
        }

        private async void DisplayFinalMessage(string message)
        {
            await DisplayAlert("Error", message, "", "OKAY");
        }
    }
}