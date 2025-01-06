using Youth.ViewModels;

namespace Youth.Views.ReferralProgram
{

    public partial class ReferralDistrictProviderPage : ContentPage
    {
        ReferralDistrictProviderViewModel _viewModel;
        public ReferralDistrictProviderPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ReferralDistrictProviderViewModel();
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