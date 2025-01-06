using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.jobs;
using Youth.Views.SafeSpace;
using Youth.Views.Shopping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Youth.ViewModels.Base;



namespace Youth.ViewModels
{
    [QueryProperty(nameof(DiscussionId), nameof(DiscussionId))]
    internal class EventDiscussionRepliesViewModel : BaseViewModel, IEventDiscussionRepliesViewModel
    {
        public UserAccount userAccount { get; set; }
        public EventDiscussion mEventDiscussion { get; set; }
        public ObservableCollection<EventDiscussion> EventDiscussionReplies { get; }
        public bool isRunning { get; set; }
        public Command SendReplyTextCommand { get; }
        public EventDiscussionRepliesViewModel()
        {
            Title = "Discussion";
            EventDiscussionReplies = new ObservableCollection<EventDiscussion>();
            SendReplyTextCommand = new Command(ExecuteSendReplyTextCommand);
        }

        private async void ExecuteSendReplyTextCommand()
        {
            try
            {
                Debug.WriteLine("EventDiscussionRepliesViewModel: ExecuteSendReplyTextCommand()");
                if (ReplyTextMessage == null || mEventDiscussion == null)
                    return;

                isRunning = true;
                OnPropertyChanged("isRunning");

                await EventDiscussion.submitEventDiscussionReply(DiscussionId, mEventDiscussion.eventId, ReplyTextMessage);
                MessagingCenter.Send<EventDiscussionRepliesViewModel, bool>(this, "isMessageSent", true);
                LoadEventDDiscussionReplies(DiscussionId);

                isRunning = false;
                OnPropertyChanged("isRunning");

            }
            catch (Exception ex)
            {
                Debug.WriteLine("EventDiscussionRepliesViewModel: " + ex);
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

        private int _discussionId;
        public int DiscussionId
        {
            get
            {
                return _discussionId;
            }
            set
            {
                _discussionId = value;
                LoadEventDDiscussionReplies(value);
            }
        }

        public async void LoadEventDDiscussionReplies(int discussionId)
        {
            Debug.WriteLine("MainShoppingViewModel: ExecuteShoppingCategoryCommand()");
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

                JObject EventObj = await EventDiscussion.fetchEventDiscussionDetails(discussionId);
                JObject productObj = (JObject)EventObj.GetValue("data");
                mEventDiscussion = productObj.ToObject<EventDiscussion>(serializer);

                JObject serverObj = await EventDiscussion.fetchEventDiscussionReplies(discussionId);
                JArray discussionsArray = (JArray)serverObj.GetValue("data");
                EventDiscussionReplies.Clear();

                foreach (JToken token in discussionsArray)
                {
                    JObject disscussionObj = (JObject)token;
                    EventDiscussion mEventDiscussion = disscussionObj.ToObject<EventDiscussion>(serializer);
                    EventDiscussionReplies.Add(mEventDiscussion);
                }

                JObject accObj = await UserAccount.LoadMyProfileAsync();
                userAccount = accObj.ToObject<UserAccount>(serializer);

                IsBusy = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EventDiscussionRepliesViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("IsBusy");
                OnPropertyChanged("userAccount");
                OnPropertyChanged("mEventDiscussion");
                OnPropertyChanged("EventDiscussionReplies");
            }
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
        }
    }
}
