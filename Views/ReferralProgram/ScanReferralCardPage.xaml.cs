using Youth.ViewModels;
using ZXing;
using ZXing.Net.Maui;

namespace Youth.Views.ReferralProgram
{

    public partial class ScanReferralCardPage : ContentPage
    {
        ScanReferralCardViewModel _viewModel;
        public ScanReferralCardPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ScanReferralCardViewModel();

            MessagingCenter.Subscribe<ScanReferralCardViewModel, string>(this, "message", (sender, message) =>
            {
                // Do something with the parameter, "arg" in this case
                DisplayFinalMessage(message);
            });

            MessagingCenter.Subscribe<ScanReferralCardViewModel, bool>(this, "isSeekerRunning", (sender, isSeekerRunning) =>
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
                //_viewModel.OnBarcodeDetected(result.Value);
                //_viewModel.Tocken = result.Text;
            });
        }

        private async void DisplayFinalMessage(string message)
        {
            await DisplayAlert("Error", message, "", "OKAY");
        }
    }
}