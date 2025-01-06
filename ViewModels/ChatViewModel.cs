using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views;
using Youth.Views.Main;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Youth.ViewModels.Base;
using System.Windows.Input;


namespace Youth.ViewModels
{
    public class ChatViewModel : BaseViewModel, INotifyPropertyChanged, IChatViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public LastChatMessage selectedChatModel;
        public UserAccount userAccount { get; set; }
        public ObservableCollection<LastChatMessage> lastChatMessages { get; set; }
        public Command LoadMyChatsCommand { get; }
        public ICommand ChatTapped { get; }
        public Command AddChatCommand { get; }

        public ChatViewModel()
        {
            Title = "Chat.";
            lastChatMessages = new ObservableCollection<LastChatMessage>();
            LoadMyChatsCommand = new Command(async () => await ExecuteLoadMyChatsCommand());
            ChatTapped = new Command<LastChatMessage>(OnChatSelected);
            AddChatCommand = new Command(OnAddChat);
        }
        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
            SelectedChat = null;
            LoadMyChatsCommand.Execute(lastChatMessages);
        }

        async Task ExecuteLoadMyChatsCommand()
        {
            Debug.WriteLine("ChatViewModel: ExecuteLoadMyChatsCommand()");
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

                JObject accObj = await UserAccount.LoadMyProfileAsync();
                userAccount = accObj.ToObject<UserAccount>(serializer);
                OnPropertyChanged("userAccount");

                lastChatMessages.Clear();
                JObject serverObj = await LastChatMessage.FetchConversations();
                JArray chatsArray = (JArray)serverObj.GetValue("data");

                foreach (JToken token in chatsArray)
                {
                    JObject chatObj = (JObject)token;
                    LastChatMessage _chatModel = chatObj.ToObject<LastChatMessage>(serializer);
                    _chatModel.myUserId = userAccount.id;
                    lastChatMessages.Add(_chatModel);
                }
                IsBusy = false;
                OnPropertyChanged("lastChatMessages");
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Debug.WriteLine("ChatViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("lastChatMessages");
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("lastChatMessages");
                OnPropertyChanged("IsBusy");
            }
        }

        public ContactModule _contactModule;
        public ContactModule mContactModule
        {
            get => _contactModule;
            set
            {
                _contactModule = value;
                OnContactModuleSelected(value);
            }
        }

        public LastChatMessage SelectedChat
        {
            get => selectedChatModel;
            set
            {
                Debug.WriteLine("ChatViewModel: SelectedChat");
                SetProperty(ref selectedChatModel, value);
                OnChatSelected(value);
            }
        }

        private async void OnAddChat(object obj)
        {
            Debug.WriteLine("ChatViewModel: OnAddChat()");
            //await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnContactModuleSelected(ContactModule contactModule)
        {
            try
            {
                if (contactModule == null)
                    return;
                await Task.Delay(1000);
                await Shell.Current.GoToAsync($"{nameof(MessagesPage)}?{nameof(MessageViewModel.ChatId)}={contactModule.chatId}&{nameof(MessageViewModel.UserId)}={contactModule.friendId}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        async void OnChatSelected(LastChatMessage chat)
        {
            if (chat == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            var userId = chat.senderId == userAccount.id ? chat.recieverId : chat.senderId;
            Debug.WriteLine("ChatViewModel: OnChatSelected(), userId: " + userId);
            await Shell.Current.GoToAsync($"{nameof(MessagesPage)}?{nameof(MessageViewModel.ChatId)}={chat.id}&{nameof(MessageViewModel.UserId)}={userId}");
        }
    }

}
