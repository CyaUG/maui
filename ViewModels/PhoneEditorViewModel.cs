using CommunityToolkit.Mvvm.Input;
using Youth.Utils;
using Youth.ViewModels.Base;
using Youth.ViewModels.Interfaces;
using Youth.Views.Tools;

namespace Youth.ViewModels
{
    public partial class PhoneEditorViewModel : BaseViewModel, IPhoneEditorViewModel
    {

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
            }
        }
        public void OnAppearing()
        {
            UpdateAuthStatus();
        }

        public void OnUpdatePhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        [RelayCommand]
        private async Task OpenCountryCodePickerPage()
        {
            await Shell.Current.GoToAsync($"{nameof(CountryCodePickerPage)}");
        }

        [RelayCommand]
        private async Task SubmitPhone()
        {
            if (!string.IsNullOrEmpty(PhoneNumber) && Utilities.IsValidPhoneNumber(PhoneNumber))
            {
                MessagingCenter.Send<PhoneEditorViewModel, String>(this, "phone", PhoneNumber);
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Shell.Current.DisplayAlert("Message", "Phone is required", "CLOSE");
            }
        }
    }
}
