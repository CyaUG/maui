using Youth.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Youth.Views.Auth
{

    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
            OpenAuth();
        }

        private bool IsUserAuthenticated()
        {
            var valueTask = Constants.GetAuthTocken();
            valueTask.Wait();
            var access_token = valueTask.Result;

            if (access_token != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async void OpenAuth()
        {
            if (IsUserAuthenticated())
            {
                //Navigation.PushAsync(new WelcomePage());
                //await Shell.Current.GoToAsync($"{nameof(MainPage)}");
            }
            else
            {
                MyScrollView.IsVisible = true;
                ActivityIndicator.IsVisible = false;
                ActivityIndicator.IsEnabled = false;
            }
        }

        protected override void OnAppearing()
        {
            OpenAuth();
            base.OnAppearing();
        }

        private async void Btn_SignIn(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }

        private async void Btn_Register(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
        }

        private async void Btn_Share(object sender, EventArgs e)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = "Hello, I want to share this cool app with you " + Constants.appDomain,
                Title = Constants.appName
            });
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

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}