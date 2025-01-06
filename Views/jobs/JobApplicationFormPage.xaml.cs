using Youth.Models;
using Youth.Utils;
using Youth.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youth.Views.jobs
{

    public partial class JobApplicationFormPage : ContentPage
    {
        JobApplicationFormViewModel _viewModel;
        public JobApplicationFormPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new JobApplicationFormViewModel();

            MessagingCenter.Subscribe<JobAppCustomQnsViewModel, ObservableCollection<JobApplicationQuestion>>(this, "JobApplicationQuestions", (sender, JobApplicationQuestions) =>
            {
                // Do something with the parameter, "arg" in this case
                _viewModel.JobApplicationQuestions = JobApplicationQuestions;
            });

            MessagingCenter.Subscribe<JobApplicationFormViewModel, string>(this, "message", (sender, message) =>
            {
                // Do something with the parameter, "arg" in this case
                DisplayFinalMessage(message);
            });

            MessagingCenter.Subscribe<JobApplicationFormViewModel, bool>(this, "isSeekerRunning", (sender, isSeekerRunning) =>
            {
                // Do something with the parameter, "arg" in this case
                ActivityIndicator.IsRunning = isSeekerRunning;
            });
        }

        private async void DisplayFinalMessage(string message)
        {
            await DisplayAlert("Error", message, "", "OKAY");
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}