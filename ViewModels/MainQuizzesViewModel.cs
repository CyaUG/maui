using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.jobs;
using Youth.Views.Quizzes;
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
    public class MainQuizzesViewModel : BaseViewModel, IMainQuizzesViewModel
    {
        public Command LoadQuizLeadBoadCommand { get; }
        public UserAccount quizProfile { get; set; }
        public ObservableCollection<UserAccount> quizLeaderBoard { get; set; }
        public ObservableCollection<QuizCategory> quizCategories { get; set; }
        public ObservableCollection<Quiz> quizzes { get; set; }

        public UserAccount _quizLeaderBoard1;
        public UserAccount quizLeaderBoard1
        {
            get { return _quizLeaderBoard1; }
            set
            {
                _quizLeaderBoard1 = value;
                OnPropertyChanged();
            }
        }
        public UserAccount _quizLeaderBoard2;
        public UserAccount quizLeaderBoard2
        {
            get { return _quizLeaderBoard2; }
            set
            {
                _quizLeaderBoard2 = value;
                OnPropertyChanged();
            }
        }
        public UserAccount _quizLeaderBoard3;
        public UserAccount quizLeaderBoard3
        {
            get { return _quizLeaderBoard3; }
            set
            {
                _quizLeaderBoard3 = value;
                OnPropertyChanged();
            }
        }
        public Command<QuizCategory> QuizCategoryNavTap { get; }
        public Command<Quiz> QuizNavTap { get; }
        public Command QuizLeaderBoardNavTap { get; }

        public MainQuizzesViewModel()
        {
            Title = "Quizzes";
            quizProfile = new UserAccount();
            quizProfile.totalPoints = 0;
            quizLeaderBoard = new ObservableCollection<UserAccount>();
            quizCategories = new ObservableCollection<QuizCategory>();
            quizzes = new ObservableCollection<Quiz>();
            LoadQuizLeadBoadCommand = new Command(async () => await ExecuteLoadQuizLeadBoadCommand());
            QuizCategoryNavTap = new Command<QuizCategory>(OnQuizCategorySelected);
            QuizNavTap = new Command<Quiz>(OnQuizSelected);
            QuizLeaderBoardNavTap = new Command(OnQuizLeaderBoardSelected);
        }

        async void OnQuizCategorySelected(QuizCategory quizCategory)
        {
            if (quizCategory == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(QuizCategoryQuizzesPage)}?{nameof(QuizCategoryQuizzesViewModel.CategoryId)}={quizCategory.id}");
        }

        async void OnQuizSelected(Quiz quiz)
        {
            if (quiz == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(QuizQuestionsPage)}?{nameof(QuizQuestionsViewModel.QuizId)}={quiz.id}");
        }

        async void OnQuizLeaderBoardSelected()
        {
            await Shell.Current.GoToAsync($"{nameof(QuizLeaderBoardPage)}");
        }

        async Task ExecuteLoadQuizLeadBoadCommand()
        {
            Debug.WriteLine("ChatViewModel: ExecuteLoadMyChatsCommand()");
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

                JObject quizAccObj = await Quiz.getMyQuizProfile();
                quizProfile = quizAccObj.ToObject<UserAccount>(serializer);
                OnPropertyChanged("quizProfile");

                JObject serverObj = await Quiz.getLeaderBoard();
                JArray chatsArray = (JArray)serverObj.GetValue("data");

                quizLeaderBoard.Clear();
                foreach (JToken token in chatsArray)
                {
                    JObject chatObj = (JObject)token;
                    UserAccount userAccount = chatObj.ToObject<UserAccount>(serializer);
                    quizLeaderBoard.Add(userAccount);
                }

                if (quizLeaderBoard.Count > 0)
                {
                    _quizLeaderBoard1 = quizLeaderBoard[0];
                }

                if (quizLeaderBoard.Count > 1)
                {
                    _quizLeaderBoard2 = quizLeaderBoard[1];
                }

                if (quizLeaderBoard.Count > 2)
                {
                    _quizLeaderBoard3 = quizLeaderBoard[2];
                }
                OnPropertyChanged("quizLeaderBoard");

                JObject quizCategoryObj = await QuizCategory.fetchQuizCategories();
                JArray quizCategorArray = (JArray)quizCategoryObj.GetValue("data");

                quizCategories.Clear();
                foreach (JToken token in quizCategorArray)
                {
                    JObject chatObj = (JObject)token;
                    QuizCategory quizCategory = chatObj.ToObject<QuizCategory>(serializer);
                    quizCategories.Add(quizCategory);
                }
                OnPropertyChanged("quizCategories");

                JObject quizObj = await Quiz.fetchQuizzes();
                JArray quizArray = (JArray)quizObj.GetValue("data");

                quizzes.Clear();
                foreach (JToken token in quizArray)
                {
                    JObject chatObj = (JObject)token;
                    Quiz quiz = chatObj.ToObject<Quiz>(serializer);
                    quizzes.Add(quiz);
                }
                OnPropertyChanged("quizzes");
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Debug.WriteLine("ChatViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("quizLeaderBoard");
                OnPropertyChanged("quizCategories");
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("quizLeaderBoard");
                OnPropertyChanged("quizLeaderBoard1");
                OnPropertyChanged("quizLeaderBoard2");
                OnPropertyChanged("quizLeaderBoard3");
                OnPropertyChanged("quizCategories");
                OnPropertyChanged("quizzes");
                OnPropertyChanged("IsBusy");
            }
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
            LoadQuizLeadBoadCommand.Execute(this);
        }
    }
}
