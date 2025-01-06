using Youth.ViewModels;

namespace Youth.Views.ReferralProgram
{

    public partial class ReferralAccountProviderPage : ContentPage
    {
        ReferralAccountProviderViewModel _viewModel;
        public ReferralAccountProviderPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ReferralAccountProviderViewModel();
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