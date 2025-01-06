using Youth.ViewModels;
using System;

namespace Youth.Views.Events
{

    public partial class MainEventsPage : ContentPage
    {
        MainEventsViewModel _viewModel;
        public MainEventsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MainEventsViewModel();
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

        private async void GoToCart(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(CartEventsPage)}");
        }

        private async void GoToCreateEventPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(CreateEventPage)}");
        }

        private async void GoToMyActiveListedEventsPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(MyActiveListedEventsPage)}");
        }

        private async void GoToMyEventOrdersPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(MyEventOrdersPage)}");
        }

        private async void GoToMySavedEventsPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(MySavedEventsPage)}");
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