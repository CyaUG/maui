using Youth.Helpers.EmailValidation;
using Youth.Models;
using Youth.Utils;
using Youth.ViewModels;
using Newtonsoft.Json.Linq;

namespace Youth.Views.Auth
{

    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        [Obsolete]
        private async void Btn_SignUp(object sender, EventArgs e)
        {
            try
            {

                // In your code-behind or view model
                bool isChecked = agreementCheckBox.IsChecked;
                if (isChecked)
                {
                    // Do something if the CheckBox is checked

                    bool goOn = true;
                    String name = FullNameEntry.Text;
                    String email = EmailEntry.Text;
                    String password = PasswordEntry.Text;

                    if (string.IsNullOrEmpty(name))
                    {
                        goOn = false;
                    }

                    if (string.IsNullOrEmpty(email))
                    {
                        goOn = false;
                    }

                    if (string.IsNullOrEmpty(password))
                    {
                        goOn = false;
                    }

                    if (password.Length < 8)
                    {
                        await DisplayAlert("Error", "The password must be at least 8 characters.", "OKAY");
                        return;
                    }

                    if (!ValidateEmail.IsValidEmail(EmailEntry.Text))
                    {
                        await DisplayAlert("Error", "The email address is badly formatted.", "OKAY");
                        return;
                    }

                    if (goOn)
                    {
                        ActivityIndicator.IsRunning = true;
                        UserAccount userAccount = new UserAccount();
                        userAccount.name = name;
                        userAccount.email = email;
                        userAccount.password = password;
                        JObject obj = await UserAccount.AddProfile(userAccount);
                        AccountRequestResponse mAccountRequestResponse = obj.ToObject<AccountRequestResponse>();
                        Console.WriteLine(mAccountRequestResponse.access_token);

                        if (mAccountRequestResponse == null || mAccountRequestResponse.access_token == null)
                        {
                            ActivityIndicator.IsRunning = false;
                            await DisplayAlert("Error", "Invalid Information, Try Again", "OKAY");
                            return;
                        }
                        await SecureStorage.SetAsync(Constants.AUTH_TOCKEN_REF, mAccountRequestResponse.access_token);

                        await Shell.Current.GoToAsync("../..");
                        ActivityIndicator.IsRunning = false;
                    }
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

        private void Focused_full_name(object sender, EventArgs e)
        {
        }

        private void Focused_Email(object sender, EventArgs e)
        {
        }

        private void Focused_Password(object sender, EventArgs e)
        {
        }

        private void Focused_ConfirmPassword(object sender, EventArgs e)
        {
        }

        private void Btn_SignUpWithGoogle(object sender, EventArgs e)
        {
            // Future Code Here...
        }

        private void Btn_SignUpWithFB(object sender, EventArgs e)
        {
            // Future Code Here...
        }

        private void toggleVisibility(object sender, EventArgs e)
        {
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