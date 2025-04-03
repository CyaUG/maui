using Youth.Models;
using Youth.Views.jobs;
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
using System.IO;
using System.Collections.Generic;
using Youth.ViewModels.Interfaces;

namespace Youth.ViewModels
{
    [QueryProperty(nameof(JobId), nameof(JobId))]
    public class JobApplicationFormViewModel : BaseViewModel, IJobApplicationFormViewModel
    {
        private int jobId;
        public JobModel jobModel { get; set; }
        private ObservableCollection<JobApplicationQuestion> _JobApplicationQuestions;
        //public Command LoadJobDetailsCommand { get; }
        public Command WorkExperienceLoadCommand { get; }
        public Command CollegeLoadCommand { get; }
        public Command HighSchoolLoadCommand { get; }
        public Command OpenJobApplicationQuestions { get; }
        public Command SendJobApplicationForm { get; }
        public string workExperienceFile { get; set; }
        public string collageFile { get; set; }
        public string highSchoolFile { get; set; }
        public bool isQuestionareVisible { get; set; }
        public bool isQuestionareFilled { get; set; }
        public Command<JobApplicationQuestion> JobApplicationQuestionAnsTextChage { get; }

        private string _nameValue;
        public string NameValue
        {
            get { return _nameValue; }
            set
            {
                _nameValue = value;
                OnPropertyChanged("NameValue");
            }
        }

        private string _cityValue;
        public string CityValue
        {
            get { return _cityValue; }
            set
            {
                _cityValue = value;
                OnPropertyChanged("CityValue");
            }
        }

        private string _emailValue;
        public string EmailValue
        {
            get { return _emailValue; }
            set
            {
                _emailValue = value;
                OnPropertyChanged("EmailValue");
            }
        }

        private string _phoneValue;
        public string PhoneValue
        {
            get { return _phoneValue; }
            set
            {
                _phoneValue = value;
                OnPropertyChanged("PhoneValue");
            }
        }

        private FileResult mWorkExperienceFile;
        public FileResult WorkExperienceFile
        {
            get { return mWorkExperienceFile; }
            set
            {
                mWorkExperienceFile = value;
                OnPropertyChanged("WorkExperienceFile");
            }
        }

        private FileResult mCollegeFile;
        public FileResult CollegeFile
        {
            get { return mCollegeFile; }
            set
            {
                mCollegeFile = value;
                OnPropertyChanged("CollegeFile");
            }
        }

        private FileResult mHighSchoolFile;
        public FileResult HighSchoolFile
        {
            get { return mHighSchoolFile; }
            set
            {
                mHighSchoolFile = value;
                OnPropertyChanged("HighSchoolFile");
            }
        }

        public JobApplicationFormViewModel()
        {
            Title = "Application Form";
            //LoadJobDetailsCommand = new Command(async () => LoadJobDetails(JobId));
            WorkExperienceLoadCommand = new Command(async () => await PickWorkExperience());
            CollegeLoadCommand = new Command(async () => await PickCollege());
            HighSchoolLoadCommand = new Command(async () => await PickWorkHighSchool());
            OpenJobApplicationQuestions = new Command(async () => await ExecuteJobApplicationQuestions());
            SendJobApplicationForm = new Command(async () => await ExecuteSendJobApplicationForm());
            workExperienceFile = "...";
            collageFile = "...";
            highSchoolFile = "...";
            JobApplicationQuestionAnsTextChage = new Command<JobApplicationQuestion>(OnJobApplicationQuestionSelected);
            LoadJobDetails(JobId);
        }

        public ObservableCollection<JobApplicationQuestion> JobApplicationQuestions
        {
            get
            {
                return _JobApplicationQuestions;
            }
            set
            {
                _JobApplicationQuestions = value;
                OnPropertyChanged("JobApplicationQuestions");
                isQuestionareFilled = true;
                OnPropertyChanged("isQuestionareFilled");
                foreach (JobApplicationQuestion jobApplicationQuestion in JobApplicationQuestions)
                {
                    Debug.WriteLine("JobAppCustomQnsViewModel: ExecuteOpenNextQuestionCommand() " + jobApplicationQuestion.answer);
                }
            }
        }

        async Task ExecuteSendJobApplicationForm()
        {
            bool goOn = true;
            if (string.IsNullOrEmpty(PhoneValue))
            {
                goOn = false;
                Debug.WriteLine("JobDetailsViewModel: Phone is needed");
            }

            if (string.IsNullOrEmpty(EmailValue))
            {
                goOn = false;
                Debug.WriteLine("JobDetailsViewModel: Email is needed");
            }

            if (string.IsNullOrEmpty(CityValue))
            {
                goOn = false;
                Debug.WriteLine("JobDetailsViewModel: City is needed");
            }

            if (string.IsNullOrEmpty(NameValue))
            {
                goOn = false;
                Debug.WriteLine("JobDetailsViewModel: Name is needed");
            }

            if (WorkExperienceFile == null)
            {
                goOn = false;
                Debug.WriteLine("JobDetailsViewModel: Work experience file not provided");
            }

            if (CollegeFile == null)
            {
                goOn = false;
                Debug.WriteLine("JobDetailsViewModel: Collage file not provided");
            }

            if (HighSchoolFile == null)
            {
                goOn = false;
                Debug.WriteLine("JobDetailsViewModel: High School File not provided");
            }


            if (isQuestionareVisible == true && isQuestionareFilled == false)
            {
                goOn = false;
                Debug.WriteLine("JobDetailsViewModel: You need to fill out the questionare");
            }

            if (goOn)
            {
                MessagingCenter.Send<JobApplicationFormViewModel, bool>(this, "isSeekerRunning", true);
                await JobApplication.SubmitJobApplication(PhoneValue,
                                                EmailValue,
                                                CityValue,
                                                NameValue,
                                                JobId,
                                                WorkExperienceFile,
                                                CollegeFile,
                                                HighSchoolFile,
                                                JobApplicationQuestions);
                GoBack();
            }
            else
            {
                MessagingCenter.Send<JobApplicationFormViewModel, string>(this, "message", "Provide all the details to apply");
            }
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        async Task ExecuteJobApplicationQuestions()
        {
            await Shell.Current.GoToAsync($"{nameof(JobAppCustomQnsPage)}?{nameof(JobAppCustomQnsViewModel.JobId)}={jobModel.id}");
        }

        async void OnJobApplicationQuestionSelected(JobApplicationQuestion jobApplicationQuestion)
        {
            if (jobApplicationQuestion == null)
                return;

        }
        async Task PickWorkExperience()
        {
            try
            {
                var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>> {
                    { DevicePlatform.iOS, new[] { "com.adobe.pdf", "com.microsoft.word.doc", "org.openxmlformats.wordprocessingml.document", "com.microsoft.excel.xls", "org.openxmlformats.spreadsheetml.sheet", "public.plain-text" } },
                    { DevicePlatform.Android, new[] { "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "text/plain" } }
                });

                var options = new PickOptions
                {
                    PickerTitle = "Please select a document",
                    FileTypes = customFileType,
                };

                var fileData = await FilePicker.PickAsync(options);

                if (fileData != null)
                {
                    // Use the fileData.FileName and fileData.Data properties to access the picked file
                    workExperienceFile = fileData.FileName;
                    WorkExperienceFile = fileData;
                    OnPropertyChanged("WorkExperienceFile");
                    OnPropertyChanged("workExperienceFile");
                }
            }
            catch (Exception ex)
            {
                // Handle exception if picking the file fails
            }
        }
        async Task PickCollege()
        {
            try
            {
                var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>> {
                    { DevicePlatform.iOS, new[] { "com.adobe.pdf", "com.microsoft.word.doc", "org.openxmlformats.wordprocessingml.document", "com.microsoft.excel.xls", "org.openxmlformats.spreadsheetml.sheet", "public.plain-text" } },
                    { DevicePlatform.Android, new[] { "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "text/plain" } }
                });

                var options = new PickOptions
                {
                    PickerTitle = "Please select a document",
                    FileTypes = customFileType,
                };

                var fileData = await FilePicker.PickAsync(options);

                if (fileData != null)
                {
                    // Use the fileData.FileName and fileData.Data properties to access the picked file
                    collageFile = fileData.FileName;
                    CollegeFile = fileData;
                    OnPropertyChanged("CollegeFile");
                    OnPropertyChanged("collageFile");
                }
            }
            catch (Exception ex)
            {
                // Handle exception if picking the file fails
            }
        }
        async Task PickWorkHighSchool()
        {
            try
            {
                var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>> {
                    { DevicePlatform.iOS, new[] { "com.adobe.pdf", "com.microsoft.word.doc", "org.openxmlformats.wordprocessingml.document", "com.microsoft.excel.xls", "org.openxmlformats.spreadsheetml.sheet", "public.plain-text" } },
                    { DevicePlatform.Android, new[] { "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "text/plain" } }
                });

                var options = new PickOptions
                {
                    PickerTitle = "Please select a document",
                    FileTypes = customFileType,
                };

                var fileData = await FilePicker.PickAsync(options);

                if (fileData != null)
                {
                    // Use the fileData.FileName and fileData.Data properties to access the picked file
                    highSchoolFile = fileData.FileName;
                    HighSchoolFile = fileData;
                    OnPropertyChanged("HighSchoolFile");
                    OnPropertyChanged("highSchoolFile");
                }
            }
            catch (Exception ex)
            {
                // Handle exception if picking the file fails
            }
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

                JObject jobObj = await JobModel.fetchJobDetails(jobId);
                JObject jobObjData = (JObject)jobObj.GetValue("data");
                jobModel = jobObjData.ToObject<JobModel>(serializer);
                OnPropertyChanged("jobModel");

                JObject qnsServerObj = await JobApplicationQuestion.fetchJobApplicationQuestions(jobId);
                JArray qnsArray = (JArray)qnsServerObj.GetValue("data");

                if (qnsArray.Count > 0)
                {
                    //display questionare view
                    isQuestionareVisible = true;

                    if (!isQuestionareFilled)
                        isQuestionareFilled = false;
                }
                else
                {
                    isQuestionareVisible = false;
                }
                OnPropertyChanged("isQuestionareVisible");
                OnPropertyChanged("isQuestionareFilled");
                OnPropertyChanged("JobApplicationQuestions");
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
                OnPropertyChanged("isQuestionareVisible");
                OnPropertyChanged("isQuestionareFilled");
                OnPropertyChanged("JobApplicationQuestions");
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
