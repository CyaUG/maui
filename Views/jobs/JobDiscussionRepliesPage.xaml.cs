using Youth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Youth.Views.jobs
{

    public partial class JobDiscussionRepliesPage : ContentPage
    {
        JobDiscussionRepliesViewModel _viewModel;
        public JobDiscussionRepliesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new JobDiscussionRepliesViewModel();

            MessagingCenter.Subscribe<JobDiscussionRepliesViewModel, bool>(this, "isMessageSent", (sender, isMessageSent) =>
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