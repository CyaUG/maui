using Youth.ViewModels;

namespace Youth.Views.ReferralProgram
{

    public partial class ReferralAccountQrCodePage : ContentPage
    {
        ReferralAccountQrCodeViewModel _viewModel;
        public ReferralAccountQrCodePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ReferralAccountQrCodeViewModel();
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
    }
}