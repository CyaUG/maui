using Youth.ViewModels;
using Youth.ViewModels.Interfaces;

namespace Youth.Views.Account
{

    public partial class LanguageProviderPage : ContentPage
    {
        ILanguageProviderViewModel _viewModel;
        public LanguageProviderPage(ILanguageProviderViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}