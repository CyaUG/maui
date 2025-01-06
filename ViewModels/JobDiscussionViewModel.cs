using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.jobs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Youth.ViewModels.Base;


namespace Youth.ViewModels
{
    [QueryProperty(nameof(JobId), nameof(JobId))]
    public class JobDiscussionViewModel : BaseViewModel, IJobDiscussionViewModel

    {
        public ObservableCollection<JobDiscussion> JobDiscussions { get; }
        public Command SendReplyTextCommand { get; }
        public bool isRunning { get; set; }
        public JobDiscussionViewModel()
        {
            Title = "Discussion";
            JobDiscussions = new ObservableCollection<JobDiscussion>();
            SendReplyTextCommand = new Command(ExecuteSendReplyTextCommand);
        }

        private async void ExecuteSendReplyTextCommand()
        {
            try
            {
                Debug.WriteLine("JobDiscussionViewModel: ExecuteSendReplyTextCommand()");
                if (ReplyTextMessage == null)
                    return;

                isRunning = true;
                OnPropertyChanged("isRunning");

                await JobDiscussion.submitJobDiscussion(JobId, ReplyTextMessage);
                MessagingCenter.Send<JobDiscussionViewModel, bool>(this, "isMessageSent", true);
                LoadJobDiscussions(JobId);

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

        private int jobId;
        public int JobId
        {
            get
            {
                return jobId;
            }
            set
            {
                jobId = value;
                LoadJobDiscussions(value);
            }
        }
        public async void LoadJobDiscussions(int jobId)
        {
            Debug.WriteLine("JobDiscussionViewModel: LoadJobDiscussions()");
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

                JObject serverObj = await JobDiscussion.fetchJobDiscussions(jobId);
                JArray discussionsArray = (JArray)serverObj.GetValue("data");
                JobDiscussions.Clear();

                foreach (JToken token in discussionsArray)
                {
                    JObject disscussionObj = (JObject)token;
                    JobDiscussion mJobDiscussion = disscussionObj.ToObject<JobDiscussion>(serializer);
                    JobDiscussions.Add(mJobDiscussion);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("JobDiscussionViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("IsBusy");
                OnPropertyChanged("JobDiscussions");
            }
        }
        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
        }
    }
}
