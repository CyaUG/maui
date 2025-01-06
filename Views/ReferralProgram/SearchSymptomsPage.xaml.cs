using Youth.ViewModels;
using Youth.ViewModels.Interfaces;

namespace Youth.Views.ReferralProgram
{

    public partial class SearchSymptomsPage : ContentPage
    {
        ISearchSymptomsViewModel _viewModel;
        public SearchSymptomsPage(ISearchSymptomsViewModel viewModel)
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