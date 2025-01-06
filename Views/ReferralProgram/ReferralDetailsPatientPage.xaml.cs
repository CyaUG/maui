using Youth.Models;
using Youth.ViewModels;

namespace Youth.Views.ReferralProgram
{

    public partial class ReferralDetailsPatientPage : ContentPage
    {
        ReferralDetailsViewModel _viewModel;
        public ReferralDetailsPatientPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ReferralDetailsViewModel();

            MessagingCenter.Subscribe<HealthCenterProviderViewModel, HealthCenter>(this, "healthCenter", (sender, healthCenter) =>
            {
                // update health center
                _viewModel.HealthCenterId = healthCenter.id;
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