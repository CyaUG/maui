using System;
using Youth.Models;
using Youth.ViewModels;
using Youth.Views.ReferralProgram;
using Youth.Views.Account;

using Youth.Utils;
using Youth.Views.Auth;
using Youth.Views.Shopping;
using Youth.ViewModels.Interfaces;
using System.Diagnostics;

namespace Youth.Views.Main
{

    public partial class SettingsPage : ContentPage
    {
        ISettingsViewModel _viewModel;
        public SettingsPage(ISettingsViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void BtnUserAccount(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(MyAccountPage)}");
        }

        private void BtnSignOut(object sender, EventArgs e)
        {
            try
            {
                SecureStorage.RemoveAll();
                _viewModel.OnAppearing();
            }
            catch (Exception ex)
            {
                // An error occurred while trying to remove the data
            }
        }
        private async void BtnReferralProgram(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(MainReferralPage)}");
        }
        private async void BtnChangePassword(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(ChangePasswordPage)}");
        }

        [Obsolete]
        private async void BtnPrivacyPolicy(object sender, EventArgs e)
        {
            var uri = new Uri(Constants.appDomain + "privacy_policy");

            await Browser.OpenAsync(uri, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
            });

        }

        [Obsolete]
        private async void BtnTermsOfService(object sender, EventArgs e)
        {
            var uri = new Uri(Constants.appDomain + "terms_and_conditions");

            await Browser.OpenAsync(uri, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
            });
        }
        private async void BtnShareApp(object sender, EventArgs e)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = "Hello, I want to share this cool app with you " + Constants.appDomain,
                Title = Constants.appName
            });

        }
        private async void BtnRateUs(object sender, EventArgs e)
        {
            /*
            var appStoreUri = new Uri(string.Format("itms-apps://itunes.apple.com/app/id{0}?action=write-review", your_app_id));
            Device.OpenUri(appStoreUri);

            var playStoreUri = new Uri(string.Format("market://details?id={0}", your_package_name));
            Device.OpenUri(playStoreUri);
            */

        }
        private void RadioButton_CheckedChanged(object sender, ToggledEventArgs e)
        {
            try
            {
                // e.Value will be true if the switch is toggled on and false if it's toggled off
                bool isSwitchOn = e.Value;

                // Perform your action based on the switch's state (isSwitchOn)
                if (isSwitchOn)
                {
                    // Switch is toggled on, perform action for "on" state
                    App.Current.UserAppTheme = AppTheme.Dark;
#if ANDROID
                    var colorResource = (Color)Application.Current.Resources["Background_Dark"];
                    Android.Graphics.Color androidColor = ToAndroidColor(colorResource);
                    Platform.CurrentActivity.Window.SetStatusBarColor(androidColor);
#endif
                }
                else
                {
                    // Switch is toggled off, perform action for "off" state
                    App.Current.UserAppTheme = AppTheme.Light;


#if ANDROID
                    var colorResource = (Color)Application.Current.Resources["MidGray"];
                    Android.Graphics.Color androidColor = ToAndroidColor(colorResource);
                    Platform.CurrentActivity.Window.SetStatusBarColor(androidColor);
#endif
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("RadioButton_CheckedChanged: " + ex);
            }
        }

#if ANDROID
        private Android.Graphics.Color ToAndroidColor(Microsoft.Maui.Graphics.Color color)
        {
            return Android.Graphics.Color.Argb(
                (int)(color.Alpha * 255),
                (int)(color.Red * 255),
                (int)(color.Green * 255),
                (int)(color.Blue * 255));
        }
#endif
    }
}