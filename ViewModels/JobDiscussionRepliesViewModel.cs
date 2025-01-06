using Youth.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Youth.ViewModels.Interfaces;
using Youth.ViewModels.Base;


namespace Youth.ViewModels
{
    [QueryProperty(nameof(DiscussionId), nameof(DiscussionId))]
    class JobDiscussionRepliesViewModel : BaseViewModel, IJobDiscussionRepliesViewModel
    {
        public ObservableCollection<JobDiscussion> JobDiscussionReplies { get; }
        public JobDiscussion mJobDiscussion { get; set; }
        public Command SendReplyTextCommand { get; }
        public bool isRunning { get; set; }
        public JobDiscussionRepliesViewModel()
        {
            Title = "Discussion";
            JobDiscussionReplies = new ObservableCollection<JobDiscussion>();
            SendReplyTextCommand = new Command(ExecuteSendReplyTextCommand);
        }

        private async void ExecuteSendReplyTextCommand()
        {
            try
            {
                Debug.WriteLine("JobDiscussionViewModel: ExecuteSendReplyTextCommand()");
                if (ReplyTextMessage == null || mJobDiscussion == null)
                    return;

                isRunning = true;
                OnPropertyChanged("isRunning");

                await JobDiscussion.submitJobDiscussionReply(DiscussionId, mJobDiscussion.jobId, ReplyTextMessage);
                MessagingCenter.Send<JobDiscussionRepliesViewModel, bool>(this, "isMessageSent", true);
                LoadJobDiscussionReplies(DiscussionId);

                isRunning = false;
                OnPropertyChanged("isRunning");

            }
            catch (Exception ex)
            {
                Debug.WriteLine("JobDiscussionViewModel: " + ex);
            }
        }

        private String _ReplyTextMessage;
        public String ReplyTextMessage
        {
            get
            {
                return _ReplyTextMessage;
            }
            set
            {
                _ReplyTextMessage = value;
                OnPropertyChanged("ReplyTextMessage");
            }
        }

        private int discussionId;
        public int DiscussionId
        {
            get
            {
                return discussionId;
            }
            set
            {
                discussionId = value;
                LoadJobDiscussionReplies(value);
            }
        }
        public async void LoadJobDiscussionReplies(int discussionId)
        {
            Debug.WriteLine("JobDiscussionRepliesViewModel: LoadJobDiscussionReplies()");
            IsBusy = true;

            try
            {
                var serializer = new JsonSerializer();
                serializer.Error += (sender, args) =>
                {
                    if (args.ErrorContext.Error is JsonException)
                    {
                        args.ErrorContext.Handled = true;
                    }
                };

                JObject serverObj = await JobDiscussion.fetchJobDiscussionDetails(discussionId);
                JObject jobObjData = (JObject)serverObj.GetValue("data");
                mJobDiscussion = jobObjData.ToObject<JobDiscussion>(serializer);

                JObject repliesObj = await JobDiscussion.fetchJobDiscussionReplies(discussionId);
                JArray repliesArray = (JArray)repliesObj.GetValue("data");
                JobDiscussionReplies.Clear();

                foreach (JToken token in repliesArray)
                {
                    JObject disscussionObj = (JObject)token;
                    JobDiscussion reply = disscussionObj.ToObject<JobDiscussion>(serializer);
                    JobDiscussionReplies.Add(reply);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("JobDiscussionRepliesViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("IsBusy");
                OnPropertyChanged("mJobDiscussion");
                OnPropertyChanged("JobDiscussionReplies");
            }
        }
        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
        }
    }
}
