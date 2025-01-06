using Youth.Models;
using Youth.ViewModels;

namespace Youth.Views.Quizzes
{

    public partial class MainQuizzesPage : ContentPage
    {
        MainQuizzesViewModel _viewModel;
        public MainQuizzesPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MainQuizzesViewModel();
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