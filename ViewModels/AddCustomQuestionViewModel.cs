using Youth.Models;
using Youth.ViewModels.Base;
using Youth.ViewModels.Interfaces;
using Youth.Views.jobs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Youth.ViewModels
{
    internal class AddCustomQuestionViewModel : BaseViewModel, IAddCustomQuestionViewModel
    {
        public Command SubmitQuestionCommand { get; }

        private String _QuestionValue;
        public String QuestionValue
        {
            get { return _QuestionValue; }
            set
            {
                _QuestionValue = value;
                OnPropertyChanged("QuestionValue");
            }
        }

        public AddCustomQuestionViewModel()
        {
            Title = "Add Custom Question";
            SubmitQuestionCommand = new Command(async () => await ExecureSubmitQuestionCommand());
        }
        async Task ExecureSubmitQuestionCommand()
        {
            if (string.IsNullOrEmpty(QuestionValue))
                return;

            JobSugestionQuestion jobSugestionQuestion = new JobSugestionQuestion();
            jobSugestionQuestion.question = QuestionValue;
            MessagingCenter.Send<AddCustomQuestionViewModel, JobSugestionQuestion>(this, "jobSugestionQuestion", jobSugestionQuestion);
            GoBack();
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
        }
    }
}
