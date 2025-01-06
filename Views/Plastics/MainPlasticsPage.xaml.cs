using Youth.ViewModels;

namespace Youth.Views.Plastics
{

    public partial class MainPlasticsPage : ContentPage
    {
        MainPlasticsViewModel _viewModel;
        public MainPlasticsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MainPlasticsViewModel();

            MessagingCenter.Subscribe<MainPlasticsViewModel, String>(this, "message", (sender, message) =>
            {
                DisplayMessage(message);
            });
        }

        protected async void DisplayMessage(String message)
        {
            await DisplayAlert("Error", message, "OKAY");
        }

        protected override async void OnAppearing()
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