using Youth.ViewModels;
using Youth.ViewModels.Interfaces;

namespace Youth.Views.SafeSpace
{

    public partial class SafeSpaceMainPage : ContentPage
    {
        ISafeSpaceMainViewModel _viewModel;
        public SafeSpaceMainPage(ISafeSpaceMainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
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

        private async void OpenComposePostPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(ComposePostPage)}");
        }
    }
}