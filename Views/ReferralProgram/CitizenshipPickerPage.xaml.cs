using Youth.ViewModels;

namespace Youth.Views.ReferralProgram
{

    public partial class CitizenshipPickerPage : ContentPage
    {
        CitizenshipPickeViewModel _viewModel;
        public CitizenshipPickerPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new CitizenshipPickeViewModel();
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