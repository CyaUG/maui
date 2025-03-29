using Youth.ViewModels;
using ZXing.Net.Maui;
using Youth.ViewModels.Interfaces;

namespace Youth.Views.Events
{
    public partial class EventPosPage : ContentPage
    {
        IEventPosViewModel _viewModel;
        public EventPosPage(IEventPosViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
            MessagingCenter.Subscribe<EventPosViewModel, bool>(this, "showTicket", (sender, showTicket) =>
            {
                if (showTicket)
                {
                    ShowMenu();
                }
            });

            MessagingCenter.Subscribe<EventPosViewModel, string>(this, "message", (sender, message) =>
            {
                // Do something with the parameter, "arg" in this case
                DisplayFinalMessage(message);
            });
        }

        void ShowMenu()
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
                double endHeight = 0;
                uint rate = 32;
                uint length = 500;
                Easing easing = Easing.SinOut;
                MyDraggableView.Animate("anim", callback, startHeight, endHeight, rate, length, easing);
            }
        }
        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            Action<double> callback = input => MyDraggableView.HeightRequest = input;
            double startHeight = mainDisplayInfo.Height / 4;
            double endHeight = 0;
            uint rate = 32;
            uint length = 500;
            Easing easing = Easing.SinOut;
            MyDraggableView.Animate("anim", callback, startHeight, endHeight, rate, length, easing);
            _viewModel.OnTockenStatusUpdate(false);
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

        public void Handle_OnScanResult(object sender, BarcodeDetectionEventArgs e)
        {
            var result = e.Results?.FirstOrDefault();
            if (result is null) return;

            Dispatcher.DispatchAsync(async () =>
            {
                _viewModel.OnBarcodeDetected(result.Value);
            });
        }

        private async void DisplayFinalMessage(string message)
        {
            await DisplayAlert("Alert", message, "", "OKAY");
        }
    }
}