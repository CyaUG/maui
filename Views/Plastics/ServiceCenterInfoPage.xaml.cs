using Youth.ViewModels;


namespace Youth.Views.Plastics
{

    public partial class ServiceCenterInfoPage : ContentPage
    {
        ServiceCenterInfoViewModel _viewModel;
        public ServiceCenterInfoPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ServiceCenterInfoViewModel();
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