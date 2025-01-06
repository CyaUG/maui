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
    internal class QuestionSuggestionsViewModel : BaseViewModel, IQuestionSuggestionsViewModel
    {
        public Command RunMainCommand { get; set; }
        public SystemSettings systemSettings { get; set; }
        public Command<JobSugestionQuestion> JobSugestionQuestionNavTap { get; }
        public ObservableCollection<JobSugestionQuestion> JobSugestionQuestions { get; set; }
        public QuestionSuggestionsViewModel()
        {
            Title = "Question Suggestions";
            JobSugestionQuestions = new ObservableCollection<JobSugestionQuestion>();
            RunMainCommand = new Command(async () => await ExecuteMainCommand());
            JobSugestionQuestionNavTap = new Command<JobSugestionQuestion>(OnJobSugestionQuestionSelected);
        }

        async void OnJobSugestionQuestionSelected(JobSugestionQuestion jobSugestionQuestion)
        {
            if (jobSugestionQuestion == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            MessagingCenter.Send<QuestionSuggestionsViewModel, JobSugestionQuestion>(this, "jobSugestionQuestion", jobSugestionQuestion);
            GoBack();
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        async Task ExecuteMainCommand()
        {
            Debug.WriteLine("QuestionSuggestionsViewModel: ExecuteMainCommand()");
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
                Debug.WriteLine("QuestionSuggestionsViewModel: " + systemSettings.currency);
                OnPropertyChanged("systemSettings");

                JObject jobsServerObj = await JobSugestionQuestion.fetchSugestionQuestions();
                JobSugestionQuestions.Clear();
                JArray jobsArray = (JArray)jobsServerObj.GetValue("data");

                foreach (JToken token in jobsArray)
                {
                    JObject chatObj = (JObject)token;
                    JobSugestionQuestion jobSugestionQuestion = chatObj.ToObject<JobSugestionQuestion>(serializer);
                    Debug.WriteLine("QuestionSuggestionsViewModel: " + jobSugestionQuestion.question);
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
        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
        }
    }
}
