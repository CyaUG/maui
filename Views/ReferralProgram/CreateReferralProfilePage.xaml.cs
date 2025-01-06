using Youth.CustomRenderers;
using Youth.Models;
using Youth.ViewModels;
using Youth.Views.Account;
using System.Diagnostics;

namespace Youth.Views.ReferralProgram
{

    public partial class CreateReferralProfilePage : ContentPage
    {
        CreateReferralProfileViewModel _viewModel;
        public CreateReferralProfilePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new CreateReferralProfileViewModel();

            MessagingCenter.Subscribe<ReferralAccountCategoryPickerViewModel, ReferralAccountCategory>(this, "referralAccountCategory", (sender, referralAccountCategory) =>
            {
                // Do something with the parameter, "arg" in this case
                Debug.WriteLine("CreateReferralProfilePage: CreateReferralProfilePage() -> " + referralAccountCategory.label);
                _viewModel.activeReferralAccountCategory = referralAccountCategory;
            });

            MessagingCenter.Subscribe<GenderPickerViewModel, Gender>(this, "gender", (sender, gender) =>
            {
                // Do something with the parameter, "arg" in this case
                Debug.WriteLine("CreateReferralProfilePage: CreateReferralProfilePage() -> " + gender.label);
                _viewModel.isGenderSelected = true;
                _viewModel.activeGender = gender;
            });

            MessagingCenter.Subscribe<CitizenshipPickeViewModel, ReferralAccountCitizenship>(this, "mReferralAccountCitizenship", (sender, mReferralAccountCitizenship) =>
            {
                // Do something with the parameter, "arg" in this case
                Debug.WriteLine("CreateReferralProfilePage: CreateReferralProfilePage() -> " + mReferralAccountCitizenship.label);
                _viewModel.isReferralAccountCitizenshipSelected = true;
                _viewModel.activeReferralAccountCitizenship = mReferralAccountCitizenship;
            });

            MessagingCenter.Subscribe<ReferralDistrictProviderViewModel, ReferralDistrict>(this, "referralDistrict", (sender, referralDistrict) =>
            {
                // Do something with the parameter, "arg" in this case
                Debug.WriteLine("CreateReferralProfilePage: CreateReferralProfilePage() -> " + referralDistrict.label);
                _viewModel.activeReferralDistrict = referralDistrict;
            });

            MessagingCenter.Subscribe<CreateReferralProfileViewModel, string>(this, "message", (sender, message) =>
            {
                // Do something with the parameter, "arg" in this case
                DisplayFinalMessage(message);
            });

            MessagingCenter.Subscribe<CreateReferralProfileViewModel, bool>(this, "isSeekerRunning", (sender, isSeekerRunning) =>
            {
                // Do something with the parameter, "arg" in this case
                ActivityIndicator.IsRunning = isSeekerRunning;
            });

            MessagingCenter.Subscribe<LocationPickerPage, LocationModule>(this, "location", (sender, location) =>
            {
                // Do something with the parameter, "arg" in this case
                _viewModel.activeLocation = location;
                _viewModel.SetLocationAddress = location.Address;
            });
        }

        private async void DisplayFinalMessage(string message)
        {
            await DisplayAlert("Error", message, "", "OKAY");
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

        private async void OnDateSelected(object sender, DateChangedEventArgs args)
        {
            string formattedDate = args.NewDate.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine("Selected date: " + formattedDate);
            _viewModel.DobValue = formattedDate;
        }

        private async void openBottomSheetPage(object sender, EventArgs e)
        {
            //var bottomSheetPage = new BottomSheetPage();
            //await Navigation.PushModalAsync(bottomSheetPage);
        }
        public async void Handle_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(LocationPickerPage)}");
            }
            catch (Exception ex) { }
        }
    }
}