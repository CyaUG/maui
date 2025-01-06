using Youth.ViewModels;

namespace Youth.Views.Quizzes
{

    public partial class QuizLeaderBoardPage : ContentPage
    {
        QuizLeaderBoardViewModel _viewModel;
        public QuizLeaderBoardPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new QuizLeaderBoardViewModel();
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