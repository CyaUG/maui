using Youth.ViewModels;

namespace Youth.Views
{

    public partial class SearchUsersPage : ContentPage
    {
        SearchUsersViewModel _viewModel;
        public SearchUsersPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new SearchUsersViewModel();
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

        private async void OnAddNewContact(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(SearchUsersPage)}");
        }
    }
}