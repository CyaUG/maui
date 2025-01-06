using Youth.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using Youth.ViewModels.Interfaces;
using Youth.ViewModels.Base;

namespace Youth.ViewModels
{
    [QueryProperty(nameof(ChatId), nameof(ChatId))]
    [QueryProperty(nameof(UserId), nameof(UserId))]
    public class MessageViewModel : BaseViewModel, INotifyPropertyChanged, IMessageViewModel
    {
        private string chatId;
        private string userId;

        private Uri audioUri;
        private bool isPlaying;

        public Command<Conversation> PlayCommand { get; }
        public Command PauseCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public UserAccount userAccount { get; set; }
        public Chat mChat { get; set; }
        public ObservableCollection<Conversation> Messages { get; set; }
        public Command LoadMessagesCommand { get; }
        public Command SendTextMessageCommand { get; }

        private string newMessage;
        public string NewMessage
        {
            get { return newMessage; }
            set
            {
                newMessage = value;
                OnPropertyChanged("NewMessage");
            }
        }

        public Command SendMessageCommand { get; set; }

        public MessageViewModel()
        {
            Title = "Chat";

            Messages = new ObservableCollection<Conversation>();
            LoadMessagesCommand = new Command(async () => LoadChatMessages(chatId));

            PlayCommand = new Command<Conversation>(OnPlayButtonClicked);
            PauseCommand = new Command(OnPauseButtonClicked);
            SendTextMessageCommand = new Command(OnSendTextMessageCommand);
        }

        public Uri AudioUri
        {
            get { return audioUri; }
            set
            {
                audioUri = value;
                OnPropertyChanged(nameof(AudioUri));
            }
        }

        public bool IsPlaying
        {
            get { return isPlaying; }
            set
            {
                isPlaying = value;
                OnPropertyChanged(nameof(IsPlaying));
            }
        }
        private async void OnPlayButtonClicked(Conversation conversation)
        {
            AudioUri = new Uri("https://cya.wagaana.com/" + conversation.message);
            IsPlaying = true;

            try
            {
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Head, AudioUri);
                var response = await httpClient.SendAsync(request);

                request.Dispose();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    //ISimpleAudioPlayer player = CrossSimpleAudioPlayer.Current;
                    //string url = "https://cya.wagaana.com/" + conversation.message;
                    //WebClient wc = new WebClient();
                    //Stream fileStream = wc.OpenRead(url);
                    //player.Load(fileStream);
                    //player.Play();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("MessageViewModel: OnPlayButtonClicked() " + e);
            }
        }

        private LocationModule location;
        public LocationModule mLocation
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                SendLocationMessage(value);
            }
        }

        private FileResult mPictureFile;
        public FileResult PictureFile
        {
            get { return mPictureFile; }
            set
            {
                mPictureFile = value;
                SendFileMessage("image", value);
            }
        }

        private FileResult mDocumentFile;
        public FileResult DocumentFile
        {
            get { return mDocumentFile; }
            set
            {
                mDocumentFile = value;
                SendFileMessage("file", value);
            }
        }

        private PhoneContact _PhoneContact;
        public PhoneContact mPhoneContact
        {
            get { return _PhoneContact; }
            set
            {
                _PhoneContact = value;
                SendPhoneContactMessage(value);
            }
        }

        private String _TextMessage;
        public String TextMessage
        {
            get
            {
                return _TextMessage;
            }
            set
            {
                _TextMessage = value;
                OnPropertyChanged("TextMessage");
            }
        }

        public async Task SendPhoneContactMessage(PhoneContact mPhoneContact)
        {
            try
            {
                await PhoneContact.SendContact(mPhoneContact, UserId, ChatId);
                LoadMessagesCommand.Execute(chatId);
            }
            catch (Exception ex) { }
        }

        public async Task SendFileMessage(String fileType, FileResult pictureFile)
        {
            try
            {
                await Conversation.SendFile(fileType, 0, UserId, ChatId, pictureFile);
                LoadMessagesCommand.Execute(chatId);
            }
            catch (Exception ex) { }
        }

        public async Task SendLocationMessage(LocationModule location)
        {
            try
            {
                await Conversation.SendLocationMessage(location.Address, location.Latitude, location.Longitude, UserId, ChatId);
                LoadMessagesCommand.Execute(chatId);
            }
            catch (Exception ex) { }
        }

        public async void OnSendTextMessageCommand()
        {
            try
            {
                await Conversation.SendMessage("text", "", TextMessage, UserId, ChatId);
                TextMessage = "";
                OnPropertyChanged("TextMessage");
                LoadMessagesCommand.Execute(chatId);
            }
            catch (Exception ex) { }
        }

        private void OnPauseButtonClicked()
        {
            IsPlaying = false;
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            /*Set defaults*/
        }

        public string ChatId
        {
            get
            {
                return chatId;
            }
            set
            {
                chatId = value;
                LoadChatMessages(value);
            }
        }

        public string UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
                OnPropertyChanged("UserId");
            }
        }

        public async void LoadChatMessages(string chatId)
        {
            Debug.WriteLine("MessageViewModel: LoadChatMessages(), ChatId: " + chatId);
            IsBusy = true;

            try
            {
                Messages.Clear();
                var serializer = new JsonSerializer();
                serializer.Error += (sender, args) =>
                {
                    if (args.ErrorContext.Error is JsonException)
                    {
                        args.ErrorContext.Handled = true;
                    }
                };

                JObject accObj = await UserAccount.LoadMyProfileAsync();
                UserAccount mUserAccount = accObj.ToObject<UserAccount>(serializer);
                OnPropertyChanged("userAccount");

                JObject chatServerObj = await Chat.FetchChatDetails(chatId);
                JObject chatArray = (JObject)chatServerObj.GetValue("data");
                mChat = chatArray.ToObject<Chat>(serializer);

                JObject otherAccObj = await UserAccount.LoadProfileAsync(UserId);
                userAccount = otherAccObj.ToObject<UserAccount>(serializer);
                OnPropertyChanged("userAccount");

                mChat.userAccount = userAccount;
                mChat.myAccount = mUserAccount;
                OnPropertyChanged("mChat");

                JObject serverObj = await Conversation.FetchChatConversation(chatId);
                JArray chatsArray = (JArray)serverObj.GetValue("data");

                Debug.WriteLine("MessageViewModel: LoadChatMessages(), UserId: " + UserId);

                foreach (JToken token in chatsArray)
                {
                    JObject chatObj = (JObject)token;
                    Conversation _chatModel = chatObj.ToObject<Conversation>(serializer);
                    _chatModel.myAccount = mUserAccount;
                    Messages.Add(_chatModel);
                    Debug.WriteLine("MessageViewModel: LoadChatMessages(), contentType: " + _chatModel.contentType);
                }
                OnPropertyChanged("Messages");
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MessageViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("Messages");
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("Messages");
                OnPropertyChanged("IsBusy");
            }
        }
    }

}
