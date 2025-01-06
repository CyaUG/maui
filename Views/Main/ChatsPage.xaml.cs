using Youth.ViewModels;
using Youth.Models;
using Youth.Views.Account;
using Youth.ViewModels.Interfaces;

namespace Youth.Views.Main
{

    public partial class ChatsPage : ContentPage
    {
        IChatViewModel _viewModel;

        public ChatsPage(IChatViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;

            MessagingCenter.Subscribe<ContactsProviderViewModel, ContactModule>(this, "contactModule", (sender, contactModule) =>
            {
                //_viewModel.mContactModule = contactModule;
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void OpenAccountProvider(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(ContactsProviderPage)}");
        }
    }
}