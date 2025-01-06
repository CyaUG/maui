using Youth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Youth.Views.Account
{

    public partial class UserProfilePage : ContentPage
    {
        UserProfileViewModel _viewModel;
        public UserProfilePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new UserProfileViewModel();

            MessagingCenter.Subscribe<UserProfileViewModel, string>(this, "message", (sender, message) =>
            {
                // Do something with the parameter, "arg" in this case
                DisplayFinalMessage(message);
            });
        }

        private async void DisplayFinalMessage(string message)
        {
            await DisplayAlert("Message", message, "OKAY");
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