using Youth.ViewModels;

namespace Youth.Views.ReferralProgram
{

    public partial class ReferralAccountCategoryPickerPage : ContentPage
    {
        ReferralAccountCategoryPickerViewModel _viewModel;
        public ReferralAccountCategoryPickerPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ReferralAccountCategoryPickerViewModel();
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