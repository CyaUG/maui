using Youth.Models;
using Youth.ViewModels;

namespace Youth.Views.ReferralProgram
{

    public partial class CreateNewReferralPage : ContentPage
    {
        CreateNewReferralViewModel _viewModel;
        public CreateNewReferralPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new CreateNewReferralViewModel();

            MessagingCenter.Subscribe<HealthCenterProviderViewModel, HealthCenter>(this, "healthCenter", (sender, healthCenter) =>
            {
                // update health center
                _viewModel.mHealthCenter = healthCenter;
            });

            MessagingCenter.Subscribe<CreateNewReferralViewModel, bool>(this, "isSeekerRunning", (sender, isSeekerRunning) =>
            {
                // Do something with the parameter, "arg" in this case
                ActivityIndicator.IsRunning = isSeekerRunning;
            });

            MessagingCenter.Subscribe<CreateNewReferralViewModel, string>(this, "message", (sender, message) =>
            {
                // Do something with the parameter, "arg" in this case
                DisplayFinalMessage(message);
            });
        }

        private async void DisplayFinalMessage(string message)
        {
            await DisplayAlert("Error", message, "CLOSE", "OKAY");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void OnDateSelected(object sender, DateChangedEventArgs args)
        {
            string formattedDate = args.NewDate.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine("Selected date: " + formattedDate);
            _viewModel.RemindDate = formattedDate;
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}