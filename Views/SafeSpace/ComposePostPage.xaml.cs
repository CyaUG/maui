using Youth.ViewModels;
using Youth.Models;
using System.Diagnostics;
using Youth.Utils;

namespace Youth.Views.SafeSpace
{

    public partial class ComposePostPage : ContentPage
    {
        ComposePostViewModel _viewModel;
        public ComposePostPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ComposePostViewModel();

            MessagingCenter.Subscribe<ComposePostViewModel, SafePostAudience>(this, "safePostAudience", (sender, safePostAudience) =>
            {
                // Do something with the parameter, "arg" in this case
                var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
                Action<double> callback = input => MyDraggableView.HeightRequest = input;
                double startHeight = mainDisplayInfo.Height / 4;
                double endiendHeight = 0;
                uint rate = 32;
                uint length = 500;
                Easing easing = Easing.SinOut;
                MyDraggableView.Animate("anim", callback, startHeight, endiendHeight, rate, length, easing);
            });

            MessagingCenter.Subscribe<ComposePostViewModel, string>(this, "message", (sender, message) =>
            {
                // Do something with the parameter, "arg" in this case
                DisplayFinalMessage(message);
            });
        }

        private async void DisplayFinalMessage(string message)
        {
            await DisplayAlert("Error", message, "", "OKAY");
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

        private async void pickeImage(object sender, EventArgs e)
        {
            try
            {
                FileResult result = await MediaPicker.PickPhotoAsync();

                if (result != null)
                {
                    // Set the selected image as the source of an Image control
                    _viewModel.PostImage = result;
                    ContentImage.Source = ImageSource.FromStream(() => result.OpenReadAsync().Result);
                }
            }
            catch (Exception ex) { }
        }

        void ShowMenu(Object sender, EventArgs e)
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

        private void MyEntry_Completed(object sender, EventArgs e)
        {
            Debug.WriteLine("ComposePostPage: " + PostEntry.Text);
            Debug.WriteLine("ComposePostPage: " + Constants.GetYoutubeId(PostEntry.Text));

            if (Constants.GetYoutubeId(PostEntry.Text) != null)
            {
                _viewModel.VideoId = Constants.GetYoutubeId(PostEntry.Text);
                ContentImage.Source = ImageSource.FromUri(new Uri("https://img.youtube.com/vi/" + Constants.GetYoutubeId(PostEntry.Text) + "/0.jpg"));
            }
        }

    }
}