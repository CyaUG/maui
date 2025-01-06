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
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Input;
using Youth.ViewModels.Base;



namespace Youth.ViewModels
{
    [QueryProperty(nameof(DiscussionId), nameof(DiscussionId))]
    internal class ShoppingProductDiscussionRepliesViewModel : BaseViewModel, IShoppingProductDiscussionRepliesViewModel
    {
        public SystemSettings systemSettings { get; set; }
        public UserAccount userAccount { get; set; }
        public ShoppingProductDiscussion mShoppingProductDiscussion { get; set; }
        public Command SendReplyTextCommand { get; }
        public bool isRunning { get; set; }
        public ObservableCollection<ShoppingProductDiscussion> ShoppingProductDiscussionReplis { get; }

        public ShoppingProductDiscussionRepliesViewModel()
        {
            Title = "Discussion";
            ShoppingProductDiscussionReplis = new ObservableCollection<ShoppingProductDiscussion>();
            SendReplyTextCommand = new Command(ExecuteSendReplyTextCommand);
        }

        private async void ExecuteSendReplyTextCommand()
        {
            try
            {
                Debug.WriteLine("ShoppingProductDiscussionRepliesViewModel: ExecuteSendReplyTextCommand()");
                if (ReplyTextMessage == null || mShoppingProductDiscussion == null)
                    return;

                isRunning = true;
                OnPropertyChanged("isRunning");

                await ShoppingProductDiscussion.submitShoppingProductDiscussionReply(DiscussionId, mShoppingProductDiscussion.productId, ReplyTextMessage);
                MessagingCenter.Send<ShoppingProductDiscussionRepliesViewModel, bool>(this, "isMessageSent", true);
                LoadProductDiscussionReplies(DiscussionId);

                isRunning = false;
                OnPropertyChanged("isRunning");

            }
            catch (Exception ex)
            {
                Debug.WriteLine("ShoppingProductDiscussionRepliesViewModel: " + ex);
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
                LoadProductDiscussionReplies(value);
            }
        }

        public async void LoadProductDiscussionReplies(int discussionId)
        {
            Debug.WriteLine("ShoppingProductDiscussionRepliesViewModel: LoadProductDiscussionReplies()");
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

                systemSettings = await SystemSettings.fetchSystemSettings();
                Debug.WriteLine("ShoppingProductDiscussionRepliesViewModel: " + systemSettings.currency);

                JObject EventObj = await ShoppingProductDiscussion.fetchShoppingProductDiscussionDetails(discussionId);
                JObject productObj = (JObject)EventObj.GetValue("data");
                mShoppingProductDiscussion = productObj.ToObject<ShoppingProductDiscussion>(serializer);

                JObject serverObj = await ShoppingProductDiscussion.fetchShoppingProductDiscussionReplies(discussionId);
                JArray discussionsArray = (JArray)serverObj.GetValue("data");
                ShoppingProductDiscussionReplis.Clear();

                foreach (JToken token in discussionsArray)
                {
                    JObject disscussionObj = (JObject)token;
                    ShoppingProductDiscussion mShoppingProductDiscussion = disscussionObj.ToObject<ShoppingProductDiscussion>(serializer);
                    ShoppingProductDiscussionReplis.Add(mShoppingProductDiscussion);
                }

                JObject accObj = await UserAccount.LoadMyProfileAsync();
                userAccount = accObj.ToObject<UserAccount>(serializer);

                IsBusy = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ShoppingProductDiscussionRepliesViewModel: " + ex);
                IsBusy = false;
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("systemSettings");
                OnPropertyChanged("mShoppingProductDiscussion");
                OnPropertyChanged("ShoppingProductDiscussionReplis");
                OnPropertyChanged("IsBusy");
            }
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
        }
    }
}
