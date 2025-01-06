using Youth.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using Youth.Views.jobs;
using Youth.ViewModels.Interfaces;
using Youth.ViewModels.Base;

namespace Youth.ViewModels
{
    internal class MyJobApplicationsViewModel : BaseViewModel, IMyJobApplicationsViewModel
    {
        public Command LoadMyJobApplicationsCommand { get; set; }
        public SystemSettings systemSettings { get; set; }
        public ObservableCollection<JobApplication> JobApplications { get; set; }
        public Command<JobApplication> JobApplicationNavTap { get; set; }

        public MyJobApplicationsViewModel()
        {
            Title = "Job Applications";
            JobApplicationNavTap = new Command<JobApplication>(OnJobApplicationSelected);
            JobApplications = new ObservableCollection<JobApplication>();
            LoadMyJobApplicationsCommand = new Command(async () => await ExecuteLoadMyJobApplicationsCommand());
        }

        async void OnJobApplicationSelected(JobApplication jobApplication)
        {
            if (jobApplication == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(JobApplicationDetailsPage)}?{nameof(JobApplicationDetailsViewModel.ApplicationId)}={jobApplication.id}");
        }

        async Task ExecuteLoadMyJobApplicationsCommand()
        {
            Debug.WriteLine("MyJobApplicationsViewModel: ExecuteLoadMyJobApplicationsCommand()");
            IsBusy = true;

            try
            {
                var serializer = new JsonSerializer();
                serializer.Error += (sender, args) =>
                {
                    if (args.ErrorContext.Error is JsonException)
                    {
                        args.ErrorContext.Handled = true;
                    }
                };

                systemSettings = await SystemSettings.fetchSystemSettings();
                Debug.WriteLine("MyJobApplicationsViewModel: " + systemSettings.currency);
                OnPropertyChanged("systemSettings");

                JObject jobsServerObj = await JobApplication.getMyApplications();
                JobApplications.Clear();
                JArray jobsArray = (JArray)jobsServerObj.GetValue("data");

                foreach (JToken token in jobsArray)
                {
                    JObject chatObj = (JObject)token;
                    JobApplication jobApplication = chatObj.ToObject<JobApplication>(serializer);
                    Debug.WriteLine("MyJobApplicationsViewModel: " + jobApplication.jobTitle);
                    JobApplications.Add(jobApplication);
                }
                OnPropertyChanged("JobApplications");
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MyJobApplicationsViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("JobApplications");
                OnPropertyChanged("IsBusy");
            }
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
        }
    }
}
