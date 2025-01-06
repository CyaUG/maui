using Youth.Utils;
using Youth.ViewModels;
using System.Diagnostics;
using ZXing;


namespace Youth.Views.SafeSpace
{

    public partial class PostDetailsPage : ContentPage
    {
        PostDetailsViewModel _viewModel;

        public PostDetailsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new PostDetailsViewModel();

            MessagingCenter.Subscribe<PostDetailsViewModel, string>(this, "message", (sender, message) =>
            {
                // Do something with the parameter, "arg" in this case
                DisplayFinalMessage(message);
            });

            MessagingCenter.Subscribe<PostDetailsViewModel, bool>(this, "isMessageSent", (sender, isMessageSent) =>
            {
                AttachmentView.IsVisible = false;
                NewImageView.Source = string.Empty;
                NewImageLabel.Text = string.Empty;
                MessageEntry.Text = string.Empty;
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
        private async void ShowAttachmentOptions(object sender, EventArgs e)
        {
            try
            {
                FileResult result = await MediaPicker.PickPhotoAsync();

                if (result != null)
                {
                    if (result != null)
                    {
                        AttachmentView.IsVisible = true;
                        NewImageView.Source = result.FullPath;
                        NewImageLabel.Text = result.FileName;
                        _viewModel.PostImage = result;
                    }
                }
            }
            catch (Exception ex) { }
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private void ToggleSendButton(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MessageEntry.Text))
            {
                SendButton.Source = "send_disabled";
            }
            else
            {
                SendButton.Source = "send_enabled";
            }
        }

        private void MyEntry_Completed(object sender, EventArgs e)
        {
            Debug.WriteLine("ComposePostPage: " + MessageEntry.Text);
            Debug.WriteLine("ComposePostPage: " + Constants.GetYoutubeId(MessageEntry.Text));

            if (Constants.GetYoutubeId(MessageEntry.Text) != null)
            {
                _viewModel.VideoId = Constants.GetYoutubeId(MessageEntry.Text);
                AttachmentView.IsVisible = true;
                NewImageView.Source = ImageSource.FromUri(new Uri("https://img.youtube.com/vi/" + Constants.GetYoutubeId(MessageEntry.Text) + "/0.jpg"));
                NewImageLabel.Text = "https://img.youtube.com/vi/" + Constants.GetYoutubeId(MessageEntry.Text) + "/0.jpg";
            }
        }
    }
}