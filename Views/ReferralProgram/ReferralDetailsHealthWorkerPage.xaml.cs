using Youth.Models;
using Youth.ViewModels;

namespace Youth.Views.ReferralProgram
{

    public partial class ReferralDetailsHealthWorkerPage : ContentPage
    {
        ReferralDetailsViewModel _viewModel;
        public ReferralDetailsHealthWorkerPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ReferralDetailsViewModel();

            MessagingCenter.Subscribe<HealthCenterStaffMemberProviderViewModel, StaffMember>(this, "staffMember", (sender, staffMember) =>
            {
                // update health center
                _viewModel.SecondaryUserId = staffMember.id;
            });

            MessagingCenter.Subscribe<ScheduleApointmentViewModel, String>(this, "dateAndTime", (sender, seletedDateAndTime) =>
            {
                // Do something with the parameter, "arg" in this case
                _viewModel.AppointmentDate = seletedDateAndTime;
            });

            MessagingCenter.Subscribe<ReferralServicesProviderViewModel, ReferralService>(this, "referralService", (sender, referralService) =>
            {
                // Do something with the parameter, "arg" in this case
                //_viewModel.AppointmentDate = referralService;
            });
        }

        void ShowMenu(System.Object sender, System.EventArgs e)
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            if (MyDraggableView.HeightRequest == 0)
            {
                Action<double> callback = input => MyDraggableView.HeightRequest = input;
                double startHeight = 0;
                double endHeight = mainDisplayInfo.Height / 4;
                uint rate = 32;
                uint length = 500;
                Easing easing = Easing.CubicOut;
                MyDraggableView.Animate("anim", callback, startHeight, endHeight, rate, length, easing);
            }
            else
            {
                Action<double> callback = input => MyDraggableView.HeightRequest = input;
                double startHeight = mainDisplayInfo.Height / 4;
                double endiendHeight = 0;
                uint rate = 32;
                uint length = 500;
                Easing easing = Easing.SinOut;
                MyDraggableView.Animate("anim", callback, startHeight, endiendHeight, rate, length, easing);

            }
        }
        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            Action<double> callback = input => MyDraggableView.HeightRequest = input;
            double startHeight = mainDisplayInfo.Height / 4;
            double endiendHeight = 0;
            uint rate = 32;
            uint length = 500;
            Easing easing = Easing.SinOut;
            MyDraggableView.Animate("anim", callback, startHeight, endiendHeight, rate, length, easing);
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