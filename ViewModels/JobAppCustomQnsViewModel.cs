using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.Events;
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



namespace Youth.ViewModels
{
    [QueryProperty(nameof(JobId), nameof(JobId))]
    internal class JobAppCustomQnsViewModel : BaseViewModel, IJobAppCustomQnsViewModel
    {
        private int jobId;
        public JobModel jobModel { get; set; }
        public ObservableCollection<JobApplicationQuestion> JobApplicationQuestions { get; set; }
        public Command OpenNextQiestion { get; }
        public int activeQuestionPosition { get; set; }
        public int totalQuestions { get; set; }
        public JobApplicationQuestion activeQuestion { get; set; }

        private string _answerValue;
        public string AnswerValue
        {
            get { return _answerValue; }
            set
            {
                _answerValue = value;
                OnPropertyChanged("AnswerValue");
            }
        }
        public JobAppCustomQnsViewModel()
        {
            Title = "Custom Questions";
            JobApplicationQuestions = new ObservableCollection<JobApplicationQuestion>();
            OpenNextQiestion = new Command(async () => ExecuteOpenNextQuestionCommand());
        }

        public async void ExecuteOpenNextQuestionCommand()
        {
            if (!string.IsNullOrEmpty(AnswerValue))
            {
                if (JobApplicationQuestions.Count > 0 && JobApplicationQuestions.Count != activeQuestionPosition)
                {
                    int actualCurent = activeQuestionPosition - 1;
                    JobApplicationQuestions[actualCurent].answer = _answerValue;
                    activeQuestion = JobApplicationQuestions[activeQuestionPosition];
                    activeQuestionPosition++;
                    OnPropertyChanged("activeQuestion");
                    OnPropertyChanged("activeQuestionPosition");
                    OnPropertyChanged("totalQuestions");
                    OnPropertyChanged("totalScore");
                    AnswerValue = "";
                    OnPropertyChanged("AnswerValue");
                }

                else
                {
                    int actualCurent = activeQuestionPosition - 1;
                    JobApplicationQuestions[actualCurent].answer = _answerValue;
                    MessagingCenter.Send<JobAppCustomQnsViewModel, ObservableCollection<JobApplicationQuestion>>(this, "JobApplicationQuestions", JobApplicationQuestions);
                    GoBack();
                }
            }
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
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
            Debug.WriteLine("JobAppCustomQnsViewModel: ExecuteLoadJobCategoriesCommand()");
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
                JobApplicationQuestions.Clear();
                JArray qnsArray = (JArray)qnsServerObj.GetValue("data");

                foreach (JToken token in qnsArray)
                {
                    JObject chatObj = (JObject)token;
                    JobApplicationQuestion jobModel = chatObj.ToObject<JobApplicationQuestion>(serializer);
                    Debug.WriteLine("JobAppCustomQnsViewModel: " + jobModel.question);
                    JobApplicationQuestions.Add(jobModel);
                }
                OnPropertyChanged("JobApplicationQuestions");

                if (JobApplicationQuestions.Count > 0)
                {
                    activeQuestionPosition = 1;
                    activeQuestion = JobApplicationQuestions[0];
                    totalQuestions = JobApplicationQuestions.Count;
                    OnPropertyChanged("totalQuestions");
                    OnPropertyChanged("activeQuestionPosition");
                    OnPropertyChanged("activeQuestion");
                    Debug.WriteLine("JobAppCustomQnsViewModel: " + activeQuestion.question);
                }
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("JobAppCustomQnsViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("JobAppCustomQnsViewModel");
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
