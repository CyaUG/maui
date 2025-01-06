using Youth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Youth.Models;
using Youth.Views.ReferralProgram;
using Youth.Views.Account;

namespace Youth.Views.Events
{

    public partial class CreateEventPage : ContentPage
    {
        CreateEventViewModel _viewModel;
        public CreateEventPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CreateEventViewModel();

            MessagingCenter.Subscribe<EventCategoryProviderViewModel, EventCategory>(this, "eventCategory", (sender, eventCategory) =>
            {
                // update health center
                _viewModel.mEventCategory = eventCategory;
            });

            MessagingCenter.Subscribe<ScheduleApointmentViewModel, String>(this, "dateAndTime", (sender, dateAndTime) =>
            {
                // update health center
                _viewModel.RemindDate = dateAndTime;
            });

            MessagingCenter.Subscribe<CreateEventViewModel, string>(this, "message", (sender, message) =>
            {
                // Do something with the parameter, "arg" in this case
                DisplayFinalMessage(message);
            });

            MessagingCenter.Subscribe<CreateEventViewModel, bool>(this, "isSeekerRunning", (sender, isSeekerRunning) =>
            {
                // Do something with the parameter, "arg" in this case
                CloseImage.IsVisible = !isSeekerRunning;
                ActivityIndicator.IsVisible = isSeekerRunning;
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
        public async void Handle_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(LocationPickerPage)}");
            }
            catch (Exception ex) { }
        }
        private async void GoToEventCategoryProviderPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(EventCategoryProviderPage)}");
        }
        private async void GoToScheduleApointmentPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(ScheduleApointmentPage)}");
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}