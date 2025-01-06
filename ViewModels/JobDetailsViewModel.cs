using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.jobs;
using Youth.Views.Main;
using Youth.Views.SafeSpace;
using Youth.Views.Shopping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Youth.ViewModels.Base;



namespace Youth.ViewModels
{
    [QueryProperty(nameof(JobId), nameof(JobId))]
    public class JobDetailsViewModel : BaseViewModel, IJobDetailsViewModel
    {
        private int jobId;
        public Command LoadJobDetailsCommand { get; }
        public ObservableCollection<JobModel> JobModeles { get; set; }
        public JobModel jobModel { get; set; }
        public Command<JobModel> JobAddressTap { get; }
        public Command<JobModel> JobApplyTap { get; }
        public JobModel selectedJobModel;
        public CustomerCareChatGroup customerCareChatGroup { get; set; }
        public UserAccount myAccount { get; set; }
        public Command OpenContactUsPage { get; }
        public Command SaveJobCommand { get; }
        public Command LikeJobCommand { get; }
        public Command UnLikeJobCommand { get; }
        public Command OpenCommentsCommand { get; }
        public Command OpenJobApplicantsPage { get; }

        public JobDetailsViewModel()
        {
            Title = "Job Info";
            JobModeles = new ObservableCollection<JobModel>();
            LoadJobDetailsCommand = new Command(async () => LoadJobDetails(JobId));
            JobAddressTap = new Command<JobModel>(OnJobAddressSelected);
            JobApplyTap = new Command<JobModel>(OnJobApplicationFormRequested);
            OpenContactUsPage = new Command(async () => await ExecuteOpenContactUsPage());
            SaveJobCommand = new Command(async () => await ExecuteSaveJobCommand());
            LikeJobCommand = new Command(async () => await ExecuteLikeJobCommand());
            UnLikeJobCommand = new Command(async () => await ExecuteUnLikeJobCommand());
            OpenCommentsCommand = new Command(async () => await ExecuteOpenCommentsCommand());
            OpenJobApplicantsPage = new Command(async () => await ExecuteOpenJobApplicantsPage());
        }

        public bool _isRunning;
        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                _isRunning = value;
                OnPropertyChanged("IsRunning");
            }
        }

        async Task ExecuteSaveJobCommand()
        {
            IsOwner = true;
            await JobModel.saveJob(JobId);
            IsOwner = false;
            IsBusy = true;
            OnPropertyChanged("IsOwner");
            OnPropertyChanged("IsBusy");
        }

        async Task ExecuteLikeJobCommand()
        {
            IsOwner = true;
            await JobModel.submitJobLike(JobId);
            IsOwner = false;
            IsBusy = true;
            OnPropertyChanged("IsOwner");
            OnPropertyChanged("IsBusy");
        }

        async Task ExecuteUnLikeJobCommand()
        {
            IsOwner = true;
            await JobModel.submitJobLike(JobId);
            IsOwner = false;
            IsBusy = true;
            OnPropertyChanged("IsOwner");
            OnPropertyChanged("IsBusy");
        }

        async Task ExecuteOpenJobApplicantsPage()
        {
            //Open JobApplicantsPage
            try
            {
                await Shell.Current.GoToAsync($"{nameof(JobApplicantsPage)}?{nameof(JobApplicantsViewModel.JobId)}={JobId}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("JobDetailsViewModel: " + ex);
            }
        }

        private async Task ExecuteOpenCommentsCommand()
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(JobDiscussionPage)}?{nameof(JobDiscussionViewModel.JobId)}={JobId}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("JobDetailsViewModel: " + ex);
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
                Debug.WriteLine("JobDetailsViewModel: SelectedChat");
                SetProperty(ref selectedJobModel, value);
                OnJobAddressSelected(value);
            }
        }

        async void OnJobAddressSelected(JobModel jobModel)
        {
            if (jobModel == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            Debug.WriteLine("JobDetailsViewModel: OnJobAddressSelected()");
            await Map.OpenAsync(jobModel.address_latitude, jobModel.address_longitude, new MapLaunchOptions { Name = jobModel.address_label });
        }

        async void OnJobApplicationFormRequested(JobModel jobModel)
        {
            if (jobModel == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack JobApplicationFormPage
            Debug.WriteLine("JobDetailsViewModel: OnJobApplicationFormRequested()");
            await Shell.Current.GoToAsync($"{nameof(JobApplicationFormPage)}?{nameof(JobApplicationFormViewModel.JobId)}={jobModel.id}");
        }

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

        public async void LoadJobDetails(int jobId)
        {
            Debug.WriteLine("JobDetailsViewModel: ExecuteLoadJobCategoriesCommand()");
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

                JObject jobsServerObj = await JobModel.fetchRelatedJobs(jobModel.jobCategoryId);
                JobModeles.Clear();
                JArray jobsArray = (JArray)jobsServerObj.GetValue("data");

                foreach (JToken token in jobsArray)
                {
                    JObject chatObj = (JObject)token;
                    JobModel jobModel = chatObj.ToObject<JobModel>(serializer);
                    Debug.WriteLine("JobDetailsViewModel: " + jobModel.jobTitle);
                    jobModel.isUnLiked = !jobModel.likedIt;
                    JobModeles.Add(jobModel);
                }
                OnPropertyChanged("JobModeles");

                JObject ccObj = await CustomerCareChatGroup.getCustomerCareChatGroupId();
                customerCareChatGroup = ccObj.ToObject<CustomerCareChatGroup>(serializer);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("JobDetailsViewModel: " + ex);
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
