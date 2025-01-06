using Youth.Helpers.EmailValidation;
using Youth.Models;
using Youth.Utils;
using Youth.ViewModels;
using Youth.Views.Events;
using Youth.Views.Main;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Youth.Views.Auth
{

    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void Btn_SignIn(object sender, EventArgs e)
        {
            try
            {
                // In your code-behind or view model
                bool isChecked = agreementCheckBox.IsChecked;
                if (isChecked)
                {
                    // Do something if the CheckBox is checked
                    EmailFrame.BorderColor = Color.FromRgb(189, 189, 189);
                    passwordFrame.BorderColor = Color.FromRgb(189, 189, 189);

                    if (string.IsNullOrEmpty(EmailEntry.Text) || string.IsNullOrEmpty(PasswordEntry.Text))
                    {
                        if (string.IsNullOrEmpty(EmailEntry.Text))
                        {
                            EmailFrame.BorderColor = Color.FromRgb(244, 67, 54);
                        }

                        if (string.IsNullOrEmpty(PasswordEntry.Text))
                        {
                            passwordFrame.BorderColor = Color.FromRgb(244, 67, 54);

                        }
                        await DisplayAlert("Error", "Missing Fields", "OKAY");
                        return;
                    }

                    if (!ValidateEmail.IsValidEmail(EmailEntry.Text))
                    {
                        await DisplayAlert("Error", "The email address is badly formatted.", "OKAY");
                        return;
                    }

                    ActivityIndicator.IsRunning = true;

                    JObject obj = await UserAccount.LoginAsync(EmailEntry.Text, PasswordEntry.Text);
                    UserAccount userAccount = obj.ToObject<UserAccount>();
                    Console.WriteLine(userAccount.access_token);

                    if (userAccount == null || userAccount.access_token == null)
                    {
                        ActivityIndicator.IsRunning = false;
                        await DisplayAlert("Error", "Wrong password OR Email", "OKAY");
                        return;
                    }
                    await SecureStorage.SetAsync(Constants.AUTH_TOCKEN_REF, userAccount.access_token);

                    await Shell.Current.GoToAsync("../..");
                    ActivityIndicator.IsRunning = false;
                }
                else
                {
                    // Do something else if the CheckBox is not checked
                    await DisplayAlert("Error", "You must agree to our terms of service.", "OKAY");
                    return;
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Something Went Wrong, Try Again", "OKAY");
                ActivityIndicator.IsRunning = false;
            }
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void Btn_ResetPass(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(ResetPassPage)}");
            ActivityIndicator.IsRunning = false;
        }

        private void Focused_Email(object sender, EventArgs e)
        {
            EmailFrame.BorderColor = Color.FromRgb(189, 189, 189);
        }

        private void Focused_Password(object sender, EventArgs e)
        {
            passwordFrame.BorderColor = Color.FromRgb(189, 189, 189);
        }

        private void Btn_SignInWithGoogle(object sender, EventArgs e)
        {
            // Future Code Here...
        }

        private void Btn_SignInWithFB(object sender, EventArgs e)
        {
            // Future Code Here...
        }
        private async void BtnPrivacyPolicy(object sender, EventArgs e)
        {
            var uri = new Uri(Constants.appDomain + "privacy_policy");

            await Browser.OpenAsync(uri, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,


            });

        }
    }
}