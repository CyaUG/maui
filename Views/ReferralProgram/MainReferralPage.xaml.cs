using Youth.ViewModels;
using Youth.ViewModels.Interfaces;

namespace Youth.Views.ReferralProgram
{

    public partial class MainReferralPage : ContentPage
    {
        IMainReferralViewModel _viewModel;
        public MainReferralPage(IMainReferralViewModel viewModel)
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
    }
}