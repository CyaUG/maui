using Youth.ViewModels;
using Youth.Views.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Youth.Views.jobs
{

    public partial class MainJobsPage : ContentPage
    {
        MainJobsViewModel _viewModel;
        public MainJobsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MainJobsViewModel();
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

        private async void GoToCreateJobPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(CreateJobPage)}");
        }

        private async void GoToMyActiveListedJobsPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(MyActiveListedJobsPage)}");
        }

        private async void GoToMyJobApplicationsPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(MyJobApplicationsPage)}");
        }

        private async void GoToMySavedJobsPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(MySavedJobsPage)}");
        }
    }
}