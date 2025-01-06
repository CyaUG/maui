using Youth.ViewModels;

namespace Youth.Views.ReferralProgram
{

    public partial class SymptomDetailsPage : ContentPage
    {
        SymptomDetailsViewModel _viewModel;
        public SymptomDetailsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new SymptomDetailsViewModel();
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