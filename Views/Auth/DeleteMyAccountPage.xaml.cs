using Youth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;




namespace Youth.Views.Auth
{

    public partial class DeleteMyAccountPage : ContentPage
    {
        public DeleteMyAccountPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void Focused_Password(object sender, EventArgs e)
        {
            oldPasswordFrame.BorderColor = Color.FromRgb(189, 189, 189);
        }
        private async void BtnChangePassword(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(ChangePasswordPage)}");
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void BtnDeleteAccount(object sender, EventArgs e)
        {
            try
            {
                string password = PasswordEntry.Text;

                if (string.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Error", "Provide your password please", "OKAY");
                    return;
                }

                ActivityIndicator.IsRunning = true;
                HttpResponseMessage response = await UserAccount.DeleteUserAccount(password);
                var result = await response.Content.ReadAsStringAsync();
                HttpStatusCode statusCode = response.StatusCode;
                if (statusCode == HttpStatusCode.Created)
                {
                    await DisplayAlert("Message", "Your request is processing...", "OKAY");
                    ActivityIndicator.IsRunning = false;
                    await Shell.Current.GoToAsync("..");
                }
                else
                if (statusCode == HttpStatusCode.Accepted)
                {
                    await DisplayAlert("Message", "Your request was succesifuly submited.", "OKAY");
                    ActivityIndicator.IsRunning = false;
                    await Shell.Current.GoToAsync("..");
                }
                else
                if (statusCode == HttpStatusCode.InternalServerError)
                {
                    //await DisplayAlert("Error", "Failed to send your request, please try again...", "CLOSE");
                    //ActivityIndicator.IsRunning = false;
                    await DisplayAlert("Message", "Your request is processing...", "OKAY");
                    ActivityIndicator.IsRunning = false;
                    await Shell.Current.GoToAsync("..");
                }
                else
                if (statusCode == HttpStatusCode.Unauthorized)
                {
                    await DisplayAlert("Error", "The provided password was wrong...", "CLOSE");
                    ActivityIndicator.IsRunning = false;
                }
                else
                {
                    await DisplayAlert("Error", "Something went wrong, please try again", "CLOSE");
                    ActivityIndicator.IsRunning = false;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", "Enter Your Password To Continue", "CLOSE");
                ActivityIndicator.IsRunning = false;
            }
        }
    }
}