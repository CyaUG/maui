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
    internal class MyActiveListedJobsViewModel : BaseViewModel, IMyActiveListedJobsViewModel
    {
        public ObservableCollection<JobModel> JobModeles { get; set; }
        //public Command LoadMyListedJobsCommand { get; }
        public SystemSettings systemSettings { get; set; }
        public Command<JobModel> JobModelNavTap { get; }

        public MyActiveListedJobsViewModel()
        {
            Title = "My Listed Jobs";
            JobModeles = new ObservableCollection<JobModel>();
            JobModelNavTap = new Command<JobModel>(OnJobModelSelected);
            //LoadMyListedJobsCommand = new Command(async () => await ExecuteLoadMyListedJobsCommand());
            _ = ExecuteLoadMyListedJobsCommand();
        }

        async void OnJobModelSelected(JobModel jobModel)
        {
            if (jobModel == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(JobDetailsPage)}?{nameof(JobDetailsViewModel.JobId)}={jobModel.id}");
        }

        async Task ExecuteLoadMyListedJobsCommand()
        {
            Debug.WriteLine("MyActiveListedJobsViewModel: ExecuteLoadMyListedJobsCommand()");
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
                Debug.WriteLine("MyActiveListedJobsViewModel: " + systemSettings.currency);
                OnPropertyChanged("systemSettings");

                JObject jobsServerObj = await JobModel.fetchMyListedJobs();
                JobModeles.Clear();
                JArray jobsArray = (JArray)jobsServerObj.GetValue("data");

                foreach (JToken token in jobsArray)
                {
                    JObject chatObj = (JObject)token;
                    JobModel jobModel = chatObj.ToObject<JobModel>(serializer);
                    Debug.WriteLine("MyActiveListedJobsViewModel: " + jobModel.jobTitle);
                    JobModeles.Add(jobModel);
                }
                OnPropertyChanged("JobModeles");
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MyActiveListedJobsViewModel: " + ex);
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
