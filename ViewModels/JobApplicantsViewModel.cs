using Youth.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

using System.Collections.ObjectModel;
using Youth.Views.jobs;
using Youth.ViewModels.Interfaces;
using Youth.ViewModels.Base;

namespace Youth.ViewModels
{
    [QueryProperty(nameof(JobId), nameof(JobId))]
    internal class JobApplicantsViewModel : BaseViewModel, IJobApplicantsViewModel
    {
        public UserAccount myAccount { get; set; }
        public JobModel jobModel { get; set; }
        public CustomerCareChatGroup customerCareChatGroup { get; set; }
        public ObservableCollection<JobApplication> JobApplications { get; set; }
        public Command<JobApplication> JobApplicationNavTap { get; set; }

        public bool _isOwner;
        public bool IsOwner
        {
            get
            {
                return _isOwner;
            }
            set
            {
                _isOwner = value;
                OnPropertyChanged("IsOwner");
            }
        }

        public int jobId;
        public int JobId
        {
            get
            {
                return jobId;
            }
            set
            {
                jobId = value;
                LoadJobDetails(value);
            }
        }

        public JobApplicantsViewModel()
        {
            Title = "Job Applicants";
            JobApplications = new ObservableCollection<JobApplication>();
            JobApplicationNavTap = new Command<JobApplication>(OnJobApplicationSelected);
        }

        async void OnJobApplicationSelected(JobApplication jobApplication)
        {
            if (jobApplication == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(JobApplicationDetailsPage)}?{nameof(JobApplicationDetailsViewModel.ApplicationId)}={jobApplication.id}");
        }

        public async void LoadJobDetails(int jobId)
        {
            Debug.WriteLine("JobApplicantsViewModel: LoadJobDetails()");
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

                JObject mAccObj = await UserAccount.LoadMyProfileAsync();
                myAccount = mAccObj.ToObject<UserAccount>(serializer);
                OnPropertyChanged("myAccount");

                JObject jobObj = await JobModel.fetchJobDetails(jobId);
                JObject jobObjData = (JObject)jobObj.GetValue("data");
                jobModel = jobObjData.ToObject<JobModel>(serializer);
                jobModel.isUnLiked = !jobModel.likedIt;
                OnPropertyChanged("jobModel");

                if (myAccount.id == jobModel.userId)
                {
                    IsOwner = true;
                }
                else { IsOwner = false; }

                JObject jobsServerObj = await JobApplication.getJobApplications(jobId);
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

                JObject ccObj = await CustomerCareChatGroup.getCustomerCareChatGroupId();
                customerCareChatGroup = ccObj.ToObject<CustomerCareChatGroup>(serializer);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("LoadJobDetails: " + ex);
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
