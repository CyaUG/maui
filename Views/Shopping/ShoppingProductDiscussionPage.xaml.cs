using Youth.Models;
using Youth.ViewModels;
using System.Diagnostics;

namespace Youth.Views.Shopping
{

    public partial class ShoppingProductDiscussionPage : ContentPage
    {
        ShoppingProductDiscussionViewModel _viewModel;
        public ShoppingProductDiscussionPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ShoppingProductDiscussionViewModel();

            MessagingCenter.Subscribe<ShoppingProductDiscussionViewModel, bool>(this, "isMessageSent", (sender, isMessageSent) =>
            {
                // Do something with the parameter, "arg" in this case
                MessageEntry.Text = string.Empty;
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
        private async void OpenDiscussionComments(object sender, EventArgs e)
        {
            try
            {
                // Get the Switch control that triggered the event
                var mLabel = (Label)sender;

                // Get the item associated with the Switch control
                var mShoppingProductDiscussion = (ShoppingProductDiscussion)mLabel.BindingContext;
                await Shell.Current.GoToAsync($"{nameof(ShoppingProductDiscussionRepliesPage)}?{nameof(ShoppingProductDiscussionRepliesViewModel.DiscussionId)}={mShoppingProductDiscussion.id}");
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
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

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}