using Youth.Models;
using Youth.ViewModels.Interfaces;
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
    public class QuizLeaderBoardViewModel : BaseViewModel, IQuizLeaderBoardViewModel
    {
        public Command LoadQuizLeadBoadCommand { get; }
        public ObservableCollection<UserAccount> quizLeaderBoard { get; set; }

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
        public UserAccount _quizLeaderBoard4;
        public UserAccount quizLeaderBoard4
        {
            get { return _quizLeaderBoard4; }
            set
            {
                _quizLeaderBoard4 = value;
                OnPropertyChanged();
            }
        }
        public UserAccount _quizLeaderBoard5;
        public UserAccount quizLeaderBoard5
        {
            get { return _quizLeaderBoard5; }
            set
            {
                _quizLeaderBoard5 = value;
                OnPropertyChanged();
            }
        }
        public UserAccount _quizLeaderBoard6;
        public UserAccount quizLeaderBoard6
        {
            get { return _quizLeaderBoard6; }
            set
            {
                _quizLeaderBoard6 = value;
                OnPropertyChanged();
            }
        }
        public UserAccount _quizLeaderBoard7;
        public UserAccount quizLeaderBoard7
        {
            get { return _quizLeaderBoard7; }
            set
            {
                _quizLeaderBoard7 = value;
                OnPropertyChanged();
            }
        }
        public UserAccount _quizLeaderBoard8;
        public UserAccount quizLeaderBoard8
        {
            get { return _quizLeaderBoard8; }
            set
            {
                _quizLeaderBoard8 = value;
                OnPropertyChanged();
            }
        }
        public UserAccount _quizLeaderBoard9;
        public UserAccount quizLeaderBoard9
        {
            get { return _quizLeaderBoard9; }
            set
            {
                _quizLeaderBoard9 = value;
                OnPropertyChanged();
            }
        }
        public UserAccount _quizLeaderBoard10;
        public UserAccount quizLeaderBoard10
        {
            get { return _quizLeaderBoard10; }
            set
            {
                _quizLeaderBoard10 = value;
                OnPropertyChanged();
            }
        }
        public Command<QuizCategory> QuizCategoryNavTap { get; }
        public Command<Quiz> QuizNavTap { get; }

        public QuizLeaderBoardViewModel()
        {
            Title = "Leader Board";
            quizLeaderBoard = new ObservableCollection<UserAccount>();
            LoadQuizLeadBoadCommand = new Command(async () => await ExecuteLoadQuizLeadBoadCommand());
            QuizCategoryNavTap = new Command<QuizCategory>(OnQuizCategorySelected);
            QuizNavTap = new Command<Quiz>(OnQuizSelected);
        }

        async void OnQuizCategorySelected(QuizCategory quizCategory)
        {
            if (quizCategory == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(JobCategoryJobsPage)}?{nameof(JobCategoryJobsViewModel.CategoryId)}={quizCategory.id}");
        }

        async void OnQuizSelected(Quiz quiz)
        {
            if (quiz == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(JobCategoryJobsPage)}?{nameof(JobCategoryJobsViewModel.CategoryId)}={quiz.id}");
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

                if (quizLeaderBoard.Count > 3)
                {
                    _quizLeaderBoard4 = quizLeaderBoard[3];
                }

                if (quizLeaderBoard.Count > 4)
                {
                    _quizLeaderBoard5 = quizLeaderBoard[4];
                }

                if (quizLeaderBoard.Count > 5)
                {
                    _quizLeaderBoard6 = quizLeaderBoard[5];
                }

                if (quizLeaderBoard.Count > 6)
                {
                    _quizLeaderBoard7 = quizLeaderBoard[6];
                }

                if (quizLeaderBoard.Count > 7)
                {
                    _quizLeaderBoard8 = quizLeaderBoard[7];
                }

                if (quizLeaderBoard.Count > 8)
                {
                    _quizLeaderBoard9 = quizLeaderBoard[8];
                }

                if (quizLeaderBoard.Count > 9)
                {
                    _quizLeaderBoard10 = quizLeaderBoard[9];
                }
                OnPropertyChanged("quizLeaderBoard");
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Debug.WriteLine("ChatViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("quizLeaderBoard");
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("quizLeaderBoard");
                OnPropertyChanged("quizLeaderBoard1");
                OnPropertyChanged("quizLeaderBoard2");
                OnPropertyChanged("quizLeaderBoard3");
                OnPropertyChanged("quizLeaderBoard4");
                OnPropertyChanged("quizLeaderBoard5");
                OnPropertyChanged("quizLeaderBoard6");
                OnPropertyChanged("quizLeaderBoard7");
                OnPropertyChanged("quizLeaderBoard8");
                OnPropertyChanged("quizLeaderBoard9");
                OnPropertyChanged("quizLeaderBoard10");
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
