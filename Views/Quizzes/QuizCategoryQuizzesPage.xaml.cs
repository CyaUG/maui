using Youth.ViewModels;

namespace Youth.Views.Quizzes
{

    public partial class QuizCategoryQuizzesPage : ContentPage
    {
        QuizCategoryQuizzesViewModel _viewModel;
        public QuizCategoryQuizzesPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new QuizCategoryQuizzesViewModel();
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