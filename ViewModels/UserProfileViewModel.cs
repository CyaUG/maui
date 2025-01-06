using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views;
using Youth.Views.Account;
using Youth.Views.Main;
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
    [QueryProperty(nameof(UserId), nameof(UserId))]
    internal class UserProfileViewModel : BaseViewModel, IUserProfileViewModel
    {
        public UserAccount userAccount { get; set; }
        public UserAccount myAccount { get; set; }
        public SystemSettings systemSettings { get; set; }
        public DashboardSummary dashboardSummary { get; set; }
        public ContactModule contactModule { get; set; }
        public ObservableCollection<Conversation> Conversations { get; }
        public Command SendMessageCommand { get; }
        public Command OpenContactUsPage { get; }
        public Command BlockUserCommand { get; }
        public Command UpdateProfileCommand { get; }
        public Command AddContactCommand { get; }
        public CustomerCareChatGroup customerCareChatGroup { get; set; }
        public bool isRunning { get; set; }
        public UserProfileViewModel()
        {
            Title = "Profile";
            Conversations = new ObservableCollection<Conversation>();
            SendMessageCommand = new Command(async () => await ExecuteSendMessageCommand());
            OpenContactUsPage = new Command(async () => await ExecuteOpenContactUsPage());
            BlockUserCommand = new Command(async () => await ExecuteBlockUserCommand());
            UpdateProfileCommand = new Command(async () => await ExecuteUpdateProfileCommand());
            AddContactCommand = new Command(async () => await ExecuteAddContactCommand());
        }

        public int userId;
        public int UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
                LoadItemId(value);
            }
        }

        async Task ExecuteAddContactCommand()
        {
            isRunning = true;
            OnPropertyChanged("isRunning");
            await ContactModule.AddContacts(UserId);
            MessagingCenter.Send<UserProfileViewModel, String>(this, "message", "Your Request has been completed");
            isRunning = false;
            OnPropertyChanged("isRunning");
            LoadItemId(UserId);
        }

        async Task ExecuteUpdateProfileCommand()
        {
            //open contact us page
            await Shell.Current.GoToAsync($"{nameof(MyAccountPage)}");
        }

        async Task ExecuteBlockUserCommand()
        {
            if (customerCareChatGroup == null)
                return;

            //open contact us page
            //await Shell.Current.GoToAsync($"{nameof(MessagesPage)}?{nameof(MessageViewModel.ChatId)}={customerCareChatGroup.id}&{nameof(MessageViewModel.UserId)}={userAccount.id}");
        }

        async Task ExecuteOpenContactUsPage()
        {
            if (customerCareChatGroup == null)
                return;

            //open contact us page
            await Shell.Current.GoToAsync($"{nameof(MessagesPage)}?{nameof(MessageViewModel.ChatId)}={customerCareChatGroup.id}&{nameof(MessageViewModel.UserId)}={myAccount.id}");
        }

        async Task ExecuteSendMessageCommand()
        {
            if (contactModule == null)
                return;

            if (!contactModule.blocked)
            {
                //open contact assignee (health worker)
                await Shell.Current.GoToAsync($"{nameof(MessagesPage)}?{nameof(MessageViewModel.ChatId)}={contactModule.chatId}&{nameof(MessageViewModel.UserId)}={UserId}");
            }
        }

        public async void LoadItemId(int userId)
        {
            try
            {
                Debug.WriteLine("UserProfileViewModel: LoadItemId() " + userId);
                var serializer = new JsonSerializer();
                serializer.Error += (sender, args) =>
                {
                    if (args.ErrorContext.Error is JsonException)
                    {
                        args.ErrorContext.Handled = true;
                    }
                };

                JObject mAccObj = await UserAccount.LoadMyProfileAsync();
                myAccount = mAccObj.ToObject<UserAccount>(serializer);
                OnPropertyChanged("myAccount");

                JObject accObj = await UserAccount.LoadUserProfile(userId);
                userAccount = accObj.ToObject<UserAccount>(serializer);
                userAccount.myAccount = myAccount;
                OnPropertyChanged("userAccount");

                systemSettings = await SystemSettings.fetchSystemSettings();
                Debug.WriteLine("MainShoppingViewModel: " + systemSettings.currency);
                OnPropertyChanged("systemSettings");

                JObject dashObj = await DashboardSummary.LoadUserDashboardSummary();
                dashboardSummary = dashObj["data"].ToObject<DashboardSummary>(serializer);
                OnPropertyChanged("dashboardSummary");

                JObject friendshipServerObj = await ContactModule.CheckFriendshipStatus(userId);
                JObject friendshipsObj = (JObject)friendshipServerObj.GetValue("data");
                contactModule = friendshipsObj.ToObject<ContactModule>(serializer);
                OnPropertyChanged("contactModule");

                JObject ConversationMediaObj = await Conversation.FetchFriendChatConversationMedia(contactModule.chatId);
                JArray ConversationMediaArray = (JArray)ConversationMediaObj.GetValue("data");
                Conversations.Clear();

                foreach (JToken token in ConversationMediaArray)
                {
                    JObject chatObj = (JObject)token;
                    Conversation conversation = chatObj.ToObject<Conversation>(serializer);
                    Debug.WriteLine("MainShoppingViewModel: " + conversation.message);
                    Conversations.Add(conversation);
                }
                OnPropertyChanged("Conversations");

                JObject ccObj = await CustomerCareChatGroup.getCustomerCareChatGroupId();
                customerCareChatGroup = ccObj.ToObject<CustomerCareChatGroup>(serializer);
                IsBusy = false;
            }
            catch (Exception)
            {
                IsBusy = false;
                Debug.WriteLine("Failed to Load Item");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("shoppingProduct");
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
