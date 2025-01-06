using Youth.ViewModels;

namespace Youth.Views.Quizzes
{

    public partial class QuizQuestionsPage : ContentPage
    {
        QuizQuestionsViewModel _viewModel;
        public QuizQuestionsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new QuizQuestionsViewModel();

            MessagingCenter.Subscribe<QuizQuestionsViewModel, int>(this, "totalScore", (sender, totalScore) =>
            {
                // Do something with the parameter, "arg" in this case
                DisplayFinalMessage(totalScore);
            });
        }

        private async void DisplayFinalMessage(int totalScore)
        {
            await DisplayAlert("Complete", "Your Total score is " + totalScore + ", Thank you for playing", "", "OKAY");
            await Shell.Current.GoToAsync("..");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}