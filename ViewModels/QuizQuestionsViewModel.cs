using Youth.Models;
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
    [QueryProperty(nameof(QuizId), nameof(QuizId))]
    public class QuizQuestionsViewModel : BaseViewModel, IQuizQuestionsViewModel
    {
        public ObservableCollection<QuizQuestion> QuizQuestions { get; set; }
        private int _quizId;
        public int totalScore { get; set; }
        public bool nextPending { get; set; }
        public int activeQuestionPosition { get; set; }
        public QuizQuestion activeQuestion { get; set; }
        public int totalQuestions { get; set; }
        public Command LoadQuizQuestionsCommand { get; }
        public Command OpenNextQuestion { get; }
        public Command<QuizQuestionAnswer> QuizAnsListTap { get; }

        public QuizQuestionAnswer _quizQuestionAnswer;
        public QuizQuestionsViewModel()
        {
            Title = "Quiz";
            QuizQuestions = new ObservableCollection<QuizQuestion>();
            LoadQuizQuestionsCommand = new Command(async () => ExecuteLoadQuizQuestionsCommand(QuizId));
            OpenNextQuestion = new Command(async () => ExecuteOpenNextQuestionCommand());
            QuizAnsListTap = new Command<QuizQuestionAnswer>(OnQuizAnswerSelected);
        }

        public QuizQuestionAnswer QuizQuestionAnswer
        {
            get => _quizQuestionAnswer;
            set
            {
                Debug.WriteLine("QuizQuestionsViewModel: QuizQuestionAnswer");
                SetProperty(ref _quizQuestionAnswer, value);
                OnQuizAnswerSelected(value);
            }
        }

        async void OnQuizAnswerSelected(QuizQuestionAnswer quizQuestionAnswer)
        {
            if (quizQuestionAnswer == null)
                return;

            for (int x = 0; x < activeQuestion.answers.Count; x++)
            {
                QuizQuestionAnswer mQuizQuestionAnswer = activeQuestion.answers[x];
                if (mQuizQuestionAnswer.id == quizQuestionAnswer.id)
                {
                    activeQuestion.answers[x].isSelected = true;
                }
                else
                {
                    activeQuestion.answers[x].isSelected = false;
                }
            }
            if (quizQuestionAnswer.isCorrect)
            {
                totalScore += activeQuestion.points;
            }
            OnPropertyChanged("totalScore");
            nextPending = true;
            OnPropertyChanged("nextPending");
            OnPropertyChanged("activeQuestion");
            SetProperty(ref _quizQuestionAnswer, quizQuestionAnswer);
            Debug.WriteLine("QuizQuestionsViewModel: OnQuizAnswerSelected: " + quizQuestionAnswer.answer);
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
        }

        public int QuizId
        {
            get
            {
                return _quizId;
            }
            set
            {
                _quizId = value;
                ExecuteLoadQuizQuestionsCommand(value);
            }
        }

        public async void ExecuteOpenNextQuestionCommand()
        {
            if (nextPending)
            {
                if (QuizQuestions.Count > 0 && QuizQuestions.Count != activeQuestionPosition)
                {
                    nextPending = false;
                    OnPropertyChanged("nextPending");
                    activeQuestion = QuizQuestions[activeQuestionPosition];
                    activeQuestionPosition++;
                    OnPropertyChanged("activeQuestion");
                    OnPropertyChanged("activeQuestionPosition");
                    OnPropertyChanged("totalQuestions");
                    OnPropertyChanged("totalScore");
                }

                else
                {
                    await Quiz.submitQuizResults(_quizId, totalScore);
                    MessagingCenter.Send<QuizQuestionsViewModel, int>(this, "totalScore", totalScore);
                }
            }
        }

        public async void ExecuteLoadQuizQuestionsCommand(int quizId)
        {
            try
            {
                Debug.WriteLine("QuizQuestionsViewModel: ExecuteLoadQuizQuestionsCommand(), quizId: " + quizId);
                var serializer = new JsonSerializer();
                serializer.Error += (sender, args) =>
                {
                    if (args.ErrorContext.Error is JsonException)
                    {
                        args.ErrorContext.Handled = true;
                    }
                };

                JObject sventsServerObj = await QuizQuestion.fetchQuizQuestions(quizId);
                QuizQuestions.Clear();
                JArray eventssArray = (JArray)sventsServerObj.GetValue("data");

                foreach (JToken token in eventssArray)
                {
                    JObject questionObj = (JObject)token;
                    JArray answersArray = (JArray)questionObj.GetValue("answers");
                    QuizQuestion quizQuestion = questionObj.ToObject<QuizQuestion>(serializer);
                    QuizQuestions.Add(quizQuestion);
                }
                if (QuizQuestions.Count > 0)
                {
                    activeQuestionPosition = 1;
                    activeQuestion = QuizQuestions[0];
                    nextPending = false;
                    OnPropertyChanged("nextPending");
                    Debug.WriteLine("QuizQuestionsViewModel: " + activeQuestion.question);
                }

                totalQuestions = QuizQuestions.Count;
                totalScore = 0;
                OnPropertyChanged("activeQuestion");
                OnPropertyChanged("activeQuestionPosition");
                OnPropertyChanged("totalQuestions");
                OnPropertyChanged("totalScore");
                IsBusy = false;
            }
            catch (Exception e)
            {
                IsBusy = false;
                Debug.WriteLine("Failed to Load Item");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("IsBusy");
                OnPropertyChanged("QuizQuestions");
                OnPropertyChanged("activeQuestion");
                OnPropertyChanged("activeQuestionPosition");
                OnPropertyChanged("totalQuestions");
                OnPropertyChanged("totalScore");
            }
        }
    }
}
