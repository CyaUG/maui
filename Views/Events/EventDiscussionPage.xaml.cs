using Youth.Models;
using Youth.ViewModels;
using Youth.Views.jobs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Youth.Views.Events
{

    public partial class EventDiscussionPage : ContentPage
    {
        EventDiscussionViewModel _viewModel;
        public EventDiscussionPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new EventDiscussionViewModel();

            MessagingCenter.Subscribe<EventDiscussionViewModel, bool>(this, "isMessageSent", (sender, isMessageSent) =>
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
                var mEventDiscussion = (EventDiscussion)mLabel.BindingContext;
                await Shell.Current.GoToAsync($"{nameof(EventDiscussionRepliesPage)}?{nameof(EventDiscussionRepliesViewModel.DiscussionId)}={mEventDiscussion.id}");
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