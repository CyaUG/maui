using Youth.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Youth.ViewModels.Base;

using Youth.Views.jobs;
using Youth.Views.Events;
using Youth.ViewModels.Interfaces;

namespace Youth.ViewModels
{
    [QueryProperty(nameof(EventId), nameof(EventId))]
    class EventDiscussionViewModel : BaseViewModel, IEventDiscussionViewModel
    {
        public ObservableCollection<EventDiscussion> EventDiscussions { get; }
        public Command SendReplyTextCommand { get; }
        public bool isRunning { get; set; }
        public EventDiscussionViewModel()
        {
            Title = "Discussion";
            EventDiscussions = new ObservableCollection<EventDiscussion>();
            SendReplyTextCommand = new Command(ExecuteSendReplyTextCommand);
        }

        private async void ExecuteSendReplyTextCommand()
        {
            try
            {
                Debug.WriteLine("EventDiscussionViewModel: ExecuteSendReplyTextCommand()");
                if (ReplyTextMessage == null)
                    return;

                isRunning = true;
                OnPropertyChanged("isRunning");

                await EventDiscussion.submitEventDiscussion(EventId, ReplyTextMessage);
                MessagingCenter.Send<EventDiscussionViewModel, bool>(this, "isMessageSent", true);
                LoadEventDiscussions(EventId);

                isRunning = false;
                OnPropertyChanged("isRunning");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EventDiscussionViewModel: " + ex);
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

        private int eventId;
        public int EventId
        {
            get
            {
                return eventId;
            }
            set
            {
                eventId = value;
                LoadEventDiscussions(value);
            }
        }
        public async void LoadEventDiscussions(int eventId)
        {
            Debug.WriteLine("EventDiscussionViewModel: LoadEventDiscussions()");
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

                JObject serverObj = await EventDiscussion.fetchEventDiscussions(eventId);
                JArray discussionsArray = (JArray)serverObj.GetValue("data");
                EventDiscussions.Clear();

                foreach (JToken token in discussionsArray)
                {
                    JObject disscussionObj = (JObject)token;
                    EventDiscussion mEventDiscussion = disscussionObj.ToObject<EventDiscussion>(serializer);
                    EventDiscussions.Add(mEventDiscussion);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EventDiscussionViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("IsBusy");
                OnPropertyChanged("EventDiscussions");
            }
        }
        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
        }
    }
}
