using Youth.Models;
using Youth.Views.jobs;
using Youth.Views.Main;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Youth.ViewModels.Base;
using Youth.Utils;
using Youth.ViewModels.Interfaces;

namespace Youth.ViewModels
{
    [QueryProperty(nameof(ApplicationId), nameof(ApplicationId))]
    internal class JobApplicationDetailsViewModel : BaseViewModel, IJobApplicationDetailsViewModel
    {
        //public Command LoadJobDetailsCommand { get; }
        public ObservableCollection<JobQuestionAnswer> JobQuestionAnswers { get; set; }
        public JobModel jobModel { get; set; }
        public JobApplication mJobApplication { get; set; }
        public Command<JobModel> JobAddressTap { get; }
        public JobModel selectedJobModel;
        public CustomerCareChatGroup customerCareChatGroup { get; set; }
        public UserAccount myAccount { get; set; }
        public Command OpenContactUsPage { get; }
        public Command SaveJobCommand { get; }
        public Command LikeJobCommand { get; }
        public Command UnLikeJobCommand { get; }
        public Command OpenCommentsCommand { get; }
        public Command OpenJobApplicantsPage { get; }
        public Command WorkExperienceLoadCommand { get; }
        public Command CollegeLoadCommand { get; }
        public Command HighSchoolLoadCommand { get; }
        public Command<JobModel> JobModelNavTap { get; }

        public JobApplicationDetailsViewModel()
        {
            Title = "Application Info";
            JobQuestionAnswers = new ObservableCollection<JobQuestionAnswer>();
            //LoadJobDetailsCommand = new Command(async () => LoadJobApplicationDetails(ApplicationId));
            JobAddressTap = new Command<JobModel>(OnJobAddressSelected);
            OpenContactUsPage = new Command(async () => await ExecuteOpenContactUsPage());
            SaveJobCommand = new Command(async () => await ExecuteSaveJobCommand());
            LikeJobCommand = new Command(async () => await ExecuteLikeJobCommand());
            UnLikeJobCommand = new Command(async () => await ExecuteUnLikeJobCommand());
            OpenCommentsCommand = new Command(async () => await ExecuteOpenCommentsCommand());
            OpenJobApplicantsPage = new Command(async () => await ExecuteOpenJobApplicantsPage());
            WorkExperienceLoadCommand = new Command(async () => await ExecuteWorkExperienceLoadCommand());
            CollegeLoadCommand = new Command(async () => await ExecuteCollegeLoadCommand());
            HighSchoolLoadCommand = new Command(async () => await ExecuteHighSchoolLoadCommand());
            JobModelNavTap = new Command<JobModel>(OnJobModelSelected);
            LoadJobApplicationDetails(ApplicationId);
        }

        async void OnJobModelSelected(JobModel jobModel)
        {
            if (jobModel == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(JobDetailsPage)}?{nameof(JobDetailsViewModel.JobId)}={jobModel.id}");
        }

        async Task ExecuteWorkExperienceLoadCommand()
        {
            if (mJobApplication == null)
                return;

            var uri = new Uri("http://docs.google.com/viewer?url=" + Constants.appDomain + mJobApplication.workExperienceFile + "&embedded=true");

            await Browser.OpenAsync(uri, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
            });
        }

        async Task ExecuteCollegeLoadCommand()
        {
            if (mJobApplication == null)
                return;

            var uri = new Uri("http://docs.google.com/viewer?url=" + Constants.appDomain + mJobApplication.collegeFile + "&embedded=true");

            await Browser.OpenAsync(uri, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
            });
        }

        async Task ExecuteHighSchoolLoadCommand()
        {
            if (mJobApplication == null)
                return;

            var uri = new Uri("http://docs.google.com/viewer?url=" + Constants.appDomain + mJobApplication.highSchoolFile + "&embedded=true");

            await Browser.OpenAsync(uri, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
            });
        }

        async Task ExecuteSaveJobCommand()
        {
            if (mJobApplication == null)
                return;

            IsOwner = true;
            await JobModel.saveJob(mJobApplication.jobId);
            IsOwner = false;
            IsBusy = true;
            OnPropertyChanged("IsOwner");
            OnPropertyChanged("IsBusy");
        }

        async Task ExecuteLikeJobCommand()
        {
            if (mJobApplication == null)
                return;

            IsOwner = true;
            await JobModel.submitJobLike(mJobApplication.jobId);
            IsOwner = false;
            IsBusy = true;
            OnPropertyChanged("IsOwner");
            OnPropertyChanged("IsBusy");
        }

        async Task ExecuteUnLikeJobCommand()
        {
            if (mJobApplication == null)
                return;

            IsOwner = true;
            await JobModel.submitJobLike(mJobApplication.jobId);
            IsOwner = false;
            IsBusy = true;
            OnPropertyChanged("IsOwner");
            OnPropertyChanged("IsBusy");
        }

        async Task ExecuteOpenJobApplicantsPage()
        {
            //Open JobApplicantsPage
        }

        private async Task ExecuteOpenCommentsCommand()
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(JobDiscussionPage)}?{nameof(JobDiscussionViewModel.JobId)}={mJobApplication.jobId}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("JobApplicationDetailsViewModel: " + ex);
            }
        }

        async Task ExecuteOpenContactUsPage()
        {
            if (customerCareChatGroup == null)
                return;

            //open contact us page
            await Shell.Current.GoToAsync($"{nameof(MessagesPage)}?{nameof(MessageViewModel.ChatId)}={customerCareChatGroup.id}&{nameof(MessageViewModel.UserId)}={myAccount.id}");
        }

        public JobModel SelectedJobModel
        {
            get => selectedJobModel;
            set
            {
                Debug.WriteLine("JobApplicationDetailsViewModel: SelectedChat");
                SetProperty(ref selectedJobModel, value);
                OnJobAddressSelected(value);
            }
        }

        async void OnJobAddressSelected(JobModel jobModel)
        {
            if (jobModel == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            Debug.WriteLine("JobApplicationDetailsViewModel: OnJobAddressSelected()");
            await Map.OpenAsync(jobModel.address_latitude, jobModel.address_longitude, new MapLaunchOptions { Name = jobModel.address_label });
        }

        private int applicationId;
        public int ApplicationId
        {
            get
            {
                return applicationId;
            }
            set
            {
                applicationId = value;
                LoadJobApplicationDetails(value);
            }
        }

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

        public async void LoadJobApplicationDetails(int applicationId)
        {
            Debug.WriteLine("JobApplicationDetailsViewModel: ExecuteLoadJobCategoriesCommand()");
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

                JObject jobApplicationObj = await JobApplication.fetchJobApplicationDetails(applicationId);
                JObject jobApplicationObjData = (JObject)jobApplicationObj.GetValue("data");
                mJobApplication = jobApplicationObjData.ToObject<JobApplication>(serializer);
                OnPropertyChanged("mJobApplication");

                JObject jobObj = await JobModel.fetchJobDetails(mJobApplication.jobId);
                JObject jobObjData = (JObject)jobObj.GetValue("data");
                jobModel = jobObjData.ToObject<JobModel>(serializer);
                jobModel.isUnLiked = !jobModel.likedIt;
                OnPropertyChanged("jobModel");

                if (myAccount.id == jobModel.userId)
                {
                    IsOwner = true;
                }
                else { IsOwner = false; }

                JObject jobsServerObj = await JobQuestionAnswer.fetchJobQuestionAnswers(applicationId);
                JobQuestionAnswers.Clear();
                JArray jobsArray = (JArray)jobsServerObj.GetValue("data");

                foreach (JToken token in jobsArray)
                {
                    JObject chatObj = (JObject)token;
                    JobQuestionAnswer jobQuestionAnswer = chatObj.ToObject<JobQuestionAnswer>(serializer);
                    Debug.WriteLine("JobApplicationDetailsViewModel: " + jobQuestionAnswer.answer);
                    JobQuestionAnswers.Add(jobQuestionAnswer);
                }
                OnPropertyChanged("JobQuestionAnswers");

                JObject ccObj = await CustomerCareChatGroup.getCustomerCareChatGroupId();
                customerCareChatGroup = ccObj.ToObject<CustomerCareChatGroup>(serializer);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("JobApplicationDetailsViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("JobQuestionAnswers");
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
