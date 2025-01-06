using Youth.ViewModels;
using Youth.ViewModels.Interfaces;

namespace Youth.Views
{

    public partial class ContactsProviderPage : ContentPage
    {
        IContactsProviderViewModel _viewModel;
        public ContactsProviderPage(IContactsProviderViewModel viewModel)
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

        private async void OnAddNewContact(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(SearchUsersPage)}");
        }
    }
}