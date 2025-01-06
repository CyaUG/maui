using Youth.Models;
using Youth.Views.jobs;
using Youth.Views.ReferralProgram;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Youth.ViewModels.Base;


using Youth.Utils;
using Youth.ViewModels.Interfaces;

namespace Youth.ViewModels
{
    internal class CreateJobViewModel : BaseViewModel, ICreateJobViewModel
    {
        public Command PickJobCategoryCommand { get; }
        public Command PickJobTypeCommand { get; }
        public Command PickCoverPictureCommand { get; }
        public Command LoadEssentialsCommand { get; }
        public Command AddCustomQuestionCommand { get; }
        public Command PickQuestionSuggestionsCommand { get; }
        public Command PostNewJobCommand { get; }
        public string LocationAddress { get; set; }
        public bool isJobTypeSelected { get; set; }
        public string JobTypeLabel { get; set; }
        public string coverPictureName { get; set; }
        public string coverPicturePath { get; set; }
        public bool coverPicSelected { get; set; }
        public Command<JobSugestionQuestion> JobSugestionQuestionNavTap { get; }
        public Command<JobSugestionQuestion> DeleteJobSugestionQuestionNavTap { get; }
        public ObservableCollection<JobSugestionQuestion> JobSugestionQuestions { get; set; }
        public ObservableCollection<JobSugestionQuestion> selectedCustomQuestions { get; set; }
        public SystemSettings systemSettings { get; set; }

        private FileResult mCoverPicture;
        public FileResult CoverPicture
        {
            get { return mCoverPicture; }
            set
            {
                mCoverPicture = value;
                OnPropertyChanged("CoverPicture");
            }
        }

        private string _JobTitleValue;
        public string JobTitleValue
        {
            get { return _JobTitleValue; }
            set
            {
                _JobTitleValue = value;
                OnPropertyChanged("JobTitleValue");
            }
        }

        private string _BusinessNameValue;
        public string BusinessNameValue
        {
            get { return _BusinessNameValue; }
            set
            {
                _BusinessNameValue = value;
                OnPropertyChanged("BusinessNameValue");
            }
        }

        private string _JobDescriptionValue;
        public string JobDescriptionValue
        {
            get { return _JobDescriptionValue; }
            set
            {
                _JobDescriptionValue = value;
                OnPropertyChanged("JobDescriptionValue");
            }
        }

        private string _MinSalaryValue;
        public string MinSalaryValue
        {
            get { return _MinSalaryValue; }
            set
            {
                _MinSalaryValue = value;
                OnPropertyChanged("MinSalaryValue");
            }
        }

        private string _MaxSalaryValue;
        public string MaxSalaryValue
        {
            get { return _MaxSalaryValue; }
            set
            {
                _MaxSalaryValue = value;
                OnPropertyChanged("MaxSalaryValue");
            }
        }
        public String SetLocationAddress
        {
            get { return LocationAddress; }
            set
            {
                LocationAddress = value;
                OnPropertyChanged("LocationAddress");
            }
        }

        private LocationModule _Location;
        public LocationModule activeLocation
        {
            get { return _Location; }
            set
            {
                _Location = value;
                OnPropertyChanged();
            }
        }

        private String _ApplicationDeadline;
        public String ApplicationDeadline
        {
            get { return _ApplicationDeadline; }
            set
            {
                _ApplicationDeadline = value;
                OnPropertyChanged("ApplicationDeadline");
            }
        }

        public CreateJobViewModel()
        {
            Title = "Create Job";
            JobSugestionQuestions = new ObservableCollection<JobSugestionQuestion>();
            selectedCustomQuestions = new ObservableCollection<JobSugestionQuestion>();
            JobSugestionQuestionNavTap = new Command<JobSugestionQuestion>(OnJobSugestionQuestionSelected);
            DeleteJobSugestionQuestionNavTap = new Command<JobSugestionQuestion>(OnDeleteJobSugestionQuestionSelected);
            PickCoverPictureCommand = new Command(async () => await PickCoverPicture());
            LoadEssentialsCommand = new Command(async () => await ExecuteLoadEssentialsCommand());
            PickJobCategoryCommand = new Command(async () => await OpenJobCategoryPickerPage());
            PickJobTypeCommand = new Command(async () => await OpenPickJobTypePage());
            AddCustomQuestionCommand = new Command(async () => await OpenAddCustomQuestionPage());
            PickQuestionSuggestionsCommand = new Command(async () => await OpenQuestionSuggestionsPage());
            PostNewJobCommand = new Command(async () => await ExecutePostNewJobCommand());
            jobCategory = new JobCategory();
            jobCategory.label = "Job Category";
            LocationAddress = "Current Address";
            JobTypeLabel = "Job Type";
            ApplicationDeadline = "Pick Application Deadline";
        }

        public void OnJobSugestionQuestionSelected(JobSugestionQuestion jobSugestionQuestion)
        {
            if (jobSugestionQuestion == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            selectedCustomQuestions.Add(jobSugestionQuestion);
            OnPropertyChanged("selectedCustomQuestions");
        }

        public void OnDeleteJobSugestionQuestionSelected(JobSugestionQuestion jobSugestionQuestion)
        {
            if (jobSugestionQuestion == null)
                return;

            for (int i = 0; i < selectedCustomQuestions.Count; i++)
            {
                JobSugestionQuestion mJobSugestionQuestion = selectedCustomQuestions[i];
                if (mJobSugestionQuestion.question == jobSugestionQuestion.question)
                {
                    selectedCustomQuestions.Remove(mJobSugestionQuestion);
                    break;
                }
            }
            OnPropertyChanged("selectedCustomQuestions");
        }

        private JobCategory _jobCategory;
        public JobCategory jobCategory
        {
            get { return _jobCategory; }
            set
            {
                _jobCategory = value;
                OnPropertyChanged();
            }
        }

        private String _JobType;
        public String JobType
        {
            get { return _JobType; }
            set
            {
                _JobType = value;
                OnPropertyChanged("JobType");

                if (value == "PART_TIME")
                    JobTypeLabel = "Part Time";

                if (value == "FULL_TIME")
                    JobTypeLabel = "Full Time";

                if (value == "REMOTE")
                    JobTypeLabel = "Remote";

                if (value == "CONTRACT")
                    JobTypeLabel = "Contract";


                OnPropertyChanged("JobTypeLabel");
            }
        }

        private bool _isRunning;
        public bool isRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                OnPropertyChanged();
            }
        }

        private bool _RequireResume;
        public bool RequireResume
        {
            get { return _RequireResume; }
            set
            {
                _RequireResume = value;
                OnPropertyChanged();
            }
        }
        async Task OpenJobCategoryPickerPage()
        {
            await Shell.Current.GoToAsync($"{nameof(JobCategoryPickerPage)}");
        }
        async Task OpenPickJobTypePage()
        {
            await Shell.Current.GoToAsync($"{nameof(PickJobTypePage)}");
        }
        async Task OpenAddCustomQuestionPage()
        {
            await Shell.Current.GoToAsync($"{nameof(AddCustomQuestionPage)}");
        }
        async Task OpenQuestionSuggestionsPage()
        {
            await Shell.Current.GoToAsync($"{nameof(QuestionSuggestionsPage)}");
        }
        async Task ExecutePostNewJobCommand()
        {
            try
            {
                bool goOn = true;

                if (string.IsNullOrEmpty(JobTitleValue))
                {
                    goOn = false;
                }

                if (string.IsNullOrEmpty(BusinessNameValue))
                {
                    goOn = false;
                }

                if (string.IsNullOrEmpty(JobDescriptionValue))
                {
                    goOn = false;
                }

                if (string.IsNullOrEmpty(MinSalaryValue))
                {
                    goOn = false;
                }

                if (string.IsNullOrEmpty(MaxSalaryValue))
                {
                    goOn = false;
                }

                if (string.IsNullOrEmpty(ApplicationDeadline))
                {
                    goOn = false;
                }

                if (string.IsNullOrEmpty(JobType))
                {
                    goOn = false;
                }

                if (activeLocation == null)
                {
                    goOn = false;
                }

                if (jobCategory == null)
                {
                    goOn = false;
                }

                if (CoverPicture == null)
                {
                    goOn = false;
                }

                if (goOn)
                {
                    JobModel jobModel = new JobModel();
                    jobModel.jobTitle = JobTitleValue;
                    jobModel.businessName = BusinessNameValue;
                    jobModel.jobDescription = JobDescriptionValue;
                    jobModel.minSalary = Double.Parse(MinSalaryValue);
                    jobModel.maxSalary = Double.Parse(MaxSalaryValue);
                    jobModel.app_deadline = ApplicationDeadline;
                    jobModel.address_label = LocationAddress;
                    jobModel.address_latitude = activeLocation.Latitude;
                    jobModel.address_longitude = activeLocation.Longitude;
                    jobModel.job_type = JobType;
                    jobModel.jobCategoryId = jobCategory.id;
                    jobModel.resumeRequired = RequireResume;

                    ObservableCollection<String> jobQuestions = new ObservableCollection<String>();
                    for (int i = 0; i < selectedCustomQuestions.Count; i++)
                    {
                        JobSugestionQuestion mJobSugestionQuestion = selectedCustomQuestions[i];
                        jobQuestions.Add(mJobSugestionQuestion.question);
                    }

                    isRunning = true;
                    await JobModel.submitJob(jobModel, CoverPicture, jobQuestions);
                    GoBack();
                }
                else
                {
                    MessagingCenter.Send<CreateJobViewModel, string>(this, "message", "Provide all the details to apply");
                    isRunning = false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("CreateJobViewModel:" + e);
                MessagingCenter.Send<CreateJobViewModel, string>(this, "message", "Something went wrong, try again");
                isRunning = false;
            }
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        async Task PickCoverPicture()
        {
            try
            {
                FileResult result = await MediaPicker.PickPhotoAsync();

                if (result != null)
                {

                    coverPictureName = result.FileName;
                    coverPicturePath = result.FullPath;
                    CoverPicture = result;
                    coverPicSelected = true;
                    OnPropertyChanged("CoverPicture");
                    OnPropertyChanged("coverPictureName");
                    OnPropertyChanged("coverPicturePath");
                    OnPropertyChanged("coverPicSelected");
                }
            }
            catch (Exception ex) { }
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
        }

        async Task ExecuteLoadEssentialsCommand()
        {
            Debug.WriteLine("CreateJobViewModel: ExecuteLoadEssentialsCommand()");
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
                Debug.WriteLine("CreateJobViewModel: " + systemSettings.currency);
                OnPropertyChanged("systemSettings");

                JObject jobsServerObj = await JobSugestionQuestion.fetchDefaultSugestionQuestions();
                JobSugestionQuestions.Clear();
                JArray jobsArray = (JArray)jobsServerObj.GetValue("data");

                foreach (JToken token in jobsArray)
                {
                    JObject chatObj = (JObject)token;
                    JobSugestionQuestion jobSugestionQuestion = chatObj.ToObject<JobSugestionQuestion>(serializer);
                    Debug.WriteLine("CreateJobViewModel: " + jobSugestionQuestion.question);
                    JobSugestionQuestions.Add(jobSugestionQuestion);
                }
                OnPropertyChanged("JobSugestionQuestions");
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("QuestionSuggestionsViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("JobSugestionQuestions");
                OnPropertyChanged("IsBusy");
            }
        }
    }
}
