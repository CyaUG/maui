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
    [QueryProperty(nameof(CategoryId), nameof(CategoryId))]
    public class QuizCategoryQuizzesViewModel : BaseViewModel, IQuizCategoryQuizzesViewModel
    {
        private int _categoryId;
        //public Command LoadQuizCategoryQuizzesCommand { get; }
        public ObservableCollection<Quiz> quizzes { get; set; }
        public Command<Quiz> QuizNavTap { get; }

        public QuizCategoryQuizzesViewModel()
        {
            Title = "Category Quizzes";
            //LoadQuizCategoryQuizzesCommand = new Command(async () => ExecuteLoadQuizCategoryQuizzesCommand(CategoryId));
            quizzes = new ObservableCollection<Quiz>();
            QuizNavTap = new Command<Quiz>(OnQuizSelected);
            ExecuteLoadQuizCategoryQuizzesCommand(CategoryId);
        }

        async void OnQuizSelected(Quiz quiz)
        {
            if (quiz == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(QuizQuestionsPage)}?{nameof(QuizQuestionsViewModel.QuizId)}={quiz.id}");
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
        }

        public int CategoryId
        {
            get
            {
                return _categoryId;
            }
            set
            {
                _categoryId = value;
                ExecuteLoadQuizCategoryQuizzesCommand(value);
            }
        }

        public async void ExecuteLoadQuizCategoryQuizzesCommand(int categoryId)
        {
            try
            {
                Debug.WriteLine("QuizCategoryQuizzesViewModel: ExecuteLoadQuizCategoryQuizzesCommand(), categoryId: " + categoryId);
                var serializer = new JsonSerializer();
                serializer.Error += (sender, args) =>
                {
                    if (args.ErrorContext.Error is JsonException)
                    {
                        args.ErrorContext.Handled = true;
                    }
                };

                JObject quizObj = await Quiz.fetchCategoryQuizzes(categoryId);
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
            }
            catch (Exception)
            {
                IsBusy = false;
                Debug.WriteLine("Failed to Load Item");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
        }
    }
}
