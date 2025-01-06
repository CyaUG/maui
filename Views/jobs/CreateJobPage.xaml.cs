using Youth.Models;
using Youth.ViewModels;
using Youth.Views.Account;
using Youth.Views.ReferralProgram;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youth.Views.jobs
{

    public partial class CreateJobPage : ContentPage
    {
        CreateJobViewModel _viewModel;
        public CreateJobPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new CreateJobViewModel();

            MessagingCenter.Subscribe<LocationPickerPage, LocationModule>(this, "location", (sender, location) =>
            {
                // Do something with the parameter, "arg" in this case
                _viewModel.activeLocation = location;
                _viewModel.SetLocationAddress = location.Address;
            });

            MessagingCenter.Subscribe<ScheduleApointmentViewModel, String>(this, "dateAndTime", (sender, seletedDateAndTime) =>
            {
                // Do something with the parameter, "arg" in this case
                _viewModel.ApplicationDeadline = seletedDateAndTime;
            });

            MessagingCenter.Subscribe<QuestionSuggestionsViewModel, JobSugestionQuestion>(this, "jobSugestionQuestion", (sender, jobSugestionQuestion) =>
            {
                // Do something with the parameter, "arg" in this case
                _viewModel.OnJobSugestionQuestionSelected(jobSugestionQuestion);
            });

            MessagingCenter.Subscribe<AddCustomQuestionViewModel, JobSugestionQuestion>(this, "jobSugestionQuestion", (sender, jobSugestionQuestion) =>
            {
                // Do something with the parameter, "arg" in this case
                _viewModel.OnJobSugestionQuestionSelected(jobSugestionQuestion);
            });

            MessagingCenter.Subscribe<JobCategoryPickerViewModel, JobCategory>(this, "jobCategory", (sender, jobCategory) =>
            {
                // Do something with the parameter, "arg" in this case
                _viewModel.jobCategory = jobCategory;
            });

            MessagingCenter.Subscribe<PickJobTypeViewModel, String>(this, "jobType", (sender, jobType) =>
            {
                // Do something with the parameter, "arg" in this case
                _viewModel.JobType = jobType;
            });

            MessagingCenter.Subscribe<CreateJobViewModel, String>(this, "message", (sender, message) =>
            {
                DisplayMessage(message);
            });
        }

        protected async void DisplayMessage(String message)
        {
            await DisplayAlert("Error", message, "OKAY");
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

        private async void OnOpenDateEditor(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(ScheduleApointmentPage)}");
            }
            catch (Exception ex) { }
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}