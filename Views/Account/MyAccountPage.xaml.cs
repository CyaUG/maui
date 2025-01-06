using Youth.Models;
using Youth.Utils;
using Youth.ViewModels;
using Youth.Views.Auth;
using Youth.Views.ReferralProgram;
using System;




namespace Youth.Views.Account
{

    public partial class MyAccountPage : ContentPage
    {
        MyAccountViewModel _viewModel;
        public MyAccountPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MyAccountViewModel();

            MessagingCenter.Subscribe<MyAccountViewModel, string>(this, "message", (sender, message) =>
            {
                // Do something with the parameter, "arg" in this case
                DisplayFinalMessage(message);
            });

            MessagingCenter.Subscribe<ScheduleApointmentViewModel, String>(this, "dateAndTime", (sender, seletedDateAndTime) =>
            {
                // Do something with the parameter, "arg" in this case
                _viewModel.DateOfBirth = seletedDateAndTime;
            });

            MessagingCenter.Subscribe<LocationPickerPage, LocationModule>(this, "location", (sender, location) =>
            {
                // Do something with the parameter, "arg" in this case
                _viewModel.mLocation = location;
            });

            MessagingCenter.Subscribe<NameEditorPage, String>(this, "name", (sender, name) =>
            {
                // Do something with the parameter, "arg" in this case
                _viewModel.NewName = name;
            });

            MessagingCenter.Subscribe<PhoneEditorViewModel, String>(this, "phone", (sender, phone) =>
            {
                // Do something with the parameter, "arg" in this case
                _viewModel.NewPhone = phone;
            });

            MessagingCenter.Subscribe<LanguageProviderViewModel, Localization>(this, "localization", (sender, localization) =>
            {
                // Do something with the parameter, "arg" in this case
                _viewModel.NewLocalization = localization;
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
        private async void BtnChangePassword(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(ChangePasswordPage)}");
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSelectCoverImage(object sender, EventArgs e)
        {
            try
            {
                FileResult result = await MediaPicker.PickPhotoAsync();

                if (result != null)
                {
                    // Set the selected image as the source of an Image control
                    _viewModel.CoverImage = result;
                    CoverImage.Source = ImageSource.FromStream(() => result.OpenReadAsync().Result);
                }
            }
            catch (Exception ex) { }
        }

        private async void OnSelectProfileImage(object sender, EventArgs e)
        {

            try
            {
                FileResult result = await MediaPicker.PickPhotoAsync();

                if (result != null)
                {
                    // Set the selected image as the source of an Image control
                    _viewModel.ProfilePicture = result;
                    ProfileImage.Source = ImageSource.FromStream(() => result.OpenReadAsync().Result);
                }
            }
            catch (Exception ex) { }
        }

        private async void OnAddNewContact(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(SearchUsersPage)}");
            }
            catch (Exception ex) { }
        }

        private async void OnOpenNameEditor(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(NameEditorPage)}");
            }
            catch (Exception ex) { }
        }

        private async void OnOpenPhoneEditor(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(PhoneEditorPage)}");
            }
            catch (Exception ex) { }
        }

        private async void OnOpenAddressEditor(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(LocationPickerPage)}");
            }
            catch (Exception ex) { }
        }

        private async void OnOpenDateEditor(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(ScheduleApointmentPage)}");
            }
            catch (Exception ex) { }
        }

        private async void OnOpenLanguageEditor(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(LanguageProviderPage)}");
            }
            catch (Exception ex) { }
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
        private async void BtnDeleteAccount(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Message", "Are you sure about deleting your account?.", "OKAY", "CANCEL");

            if (result)
            {
                try
                {
                    await Shell.Current.GoToAsync($"{nameof(DeleteMyAccountPage)}");
                }
                catch (Exception ex) { }
            }
            else
            {
                // The user clicked Cancel
            }

        }
    }
}