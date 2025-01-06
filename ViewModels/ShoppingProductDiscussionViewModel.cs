using Youth.Models;
using Youth.ViewModels.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Youth.ViewModels.Base;


namespace Youth.ViewModels
{
    [QueryProperty(nameof(ProductId), nameof(ProductId))]
    class ShoppingProductDiscussionViewModel : BaseViewModel, IShoppingProductDiscussionViewModel
    {
        public ObservableCollection<ShoppingProductDiscussion> ShoppingProductDiscussions { get; }
        public Command SendReplyTextCommand { get; }
        public bool isRunning { get; set; }
        public ShoppingProductDiscussionViewModel()
        {
            Title = "Discussion";
            ShoppingProductDiscussions = new ObservableCollection<ShoppingProductDiscussion>();
            SendReplyTextCommand = new Command(ExecuteSendReplyTextCommand);
        }

        private async void ExecuteSendReplyTextCommand()
        {
            try
            {
                Debug.WriteLine("ShoppingProductDiscussionViewModel: ExecuteSendReplyTextCommand()");
                if (ReplyTextMessage == null)
                    return;

                isRunning = true;
                OnPropertyChanged("isRunning");

                await ShoppingProductDiscussion.submitShoppingProductDiscussion(ProductId, ReplyTextMessage);
                MessagingCenter.Send<ShoppingProductDiscussionViewModel, bool>(this, "isMessageSent", true);
                LoadProductDiscussions(ProductId);

                isRunning = false;
                OnPropertyChanged("isRunning");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ShoppingProductDiscussionViewModel: " + ex);
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

        private int productId;
        public int ProductId
        {
            get
            {
                return productId;
            }
            set
            {
                productId = value;
                LoadProductDiscussions(value);
            }
        }
        public async void LoadProductDiscussions(int productId)
        {
            Debug.WriteLine("ShoppingProductDiscussionViewModel: LoadProductDiscussions()");
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

                JObject serverObj = await ShoppingProductDiscussion.fetchShoppingProductDiscussions(productId);
                JArray discussionsArray = (JArray)serverObj.GetValue("data");
                ShoppingProductDiscussions.Clear();

                foreach (JToken token in discussionsArray)
                {
                    JObject disscussionObj = (JObject)token;
                    ShoppingProductDiscussion mShoppingProductDiscussion = disscussionObj.ToObject<ShoppingProductDiscussion>(serializer);
                    ShoppingProductDiscussions.Add(mShoppingProductDiscussion);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("JobDetailsViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("JobModeles");
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
