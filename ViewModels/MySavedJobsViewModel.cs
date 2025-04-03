using Youth.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Youth.Views.jobs;
using Youth.ViewModels.Interfaces;
using Youth.ViewModels.Base;

namespace Youth.ViewModels
{
    internal class MySavedJobsViewModel : BaseViewModel, IMySavedJobsViewModel
    {
        public ObservableCollection<JobModel> JobModeles { get; set; }
        //public Command LoadMySavesJobsCommand { get; }
        public SystemSettings systemSettings { get; set; }
        public Command<JobModel> JobModelNavTap { get; }

        public MySavedJobsViewModel()
        {
            Title = "Saved Jobs";
            JobModeles = new ObservableCollection<JobModel>();
            JobModelNavTap = new Command<JobModel>(OnJobModelSelected);
            //LoadMySavesJobsCommand = new Command(async () => await ExecuteLoadMySavesJobsCommand());
            _ = ExecuteLoadMySavesJobsCommand();
        }

        async void OnJobModelSelected(JobModel jobModel)
        {
            if (jobModel == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(JobDetailsPage)}?{nameof(JobDetailsViewModel.JobId)}={jobModel.id}");
        }

        async Task ExecuteLoadMySavesJobsCommand()
        {
            Debug.WriteLine("MySavedJobsViewModel: ExecuteLoadMySavesJobsCommand()");
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
                Debug.WriteLine("MySavedJobsViewModel: " + systemSettings.currency);
                OnPropertyChanged("systemSettings");

                JObject jobsServerObj = await JobModel.fetchMySavedJobs();
                JobModeles.Clear();
                JArray jobsArray = (JArray)jobsServerObj.GetValue("data");

                foreach (JToken token in jobsArray)
                {
                    JObject chatObj = (JObject)token;
                    JobModel jobModel = chatObj.ToObject<JobModel>(serializer);
                    Debug.WriteLine("MySavedJobsViewModel: " + jobModel.jobTitle);
                    JobModeles.Add(jobModel);
                }
                OnPropertyChanged("JobModeles");
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MySavedJobsViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("JobModeles");
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
