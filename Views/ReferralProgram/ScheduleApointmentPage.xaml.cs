using Youth.ViewModels;

namespace Youth.Views.ReferralProgram
{

    public partial class ScheduleApointmentPage : ContentPage
    {
        ScheduleApointmentViewModel _viewModel;
        public ScheduleApointmentPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ScheduleApointmentViewModel();
        }
        private void OnButtonClicked(object sender, EventArgs e)
        {
            var selectedTime = myTimePicker.Time;
            // Do something with the selected time
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
        private async void OnDateSelected(object sender, DateChangedEventArgs args)
        {
            _viewModel.MyDate = args;
        }
        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}