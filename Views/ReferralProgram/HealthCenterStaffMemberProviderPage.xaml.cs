using Youth.ViewModels;

namespace Youth.Views.ReferralProgram
{

    public partial class HealthCenterStaffMemberProviderPage : ContentPage
    {
        HealthCenterStaffMemberProviderViewModel _viewModel;
        public HealthCenterStaffMemberProviderPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new HealthCenterStaffMemberProviderViewModel();
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