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
    class MyAccountViewModel : BaseViewModel, IMyAccountViewModel
    {
        public bool isRunning { get; set; }
        public UserAccount myAccount { get; set; }
        public SystemSettings systemSettings { get; set; }
        public DashboardSummary dashboardSummary { get; set; }
        public Command SendMessageCommand { get; }
        public Command OpenContactUsPage { get; }
        public Command BlockUserCommand { get; }
        public Command UpdateProfileCommand { get; }
        public Command AccountLoadCommand { get; }
        public CustomerCareChatGroup customerCareChatGroup { get; set; }
        public MyAccountViewModel()
        {
            Title = "Profile";
            isRunning = false;
            SendMessageCommand = new Command(async () => await ExecuteSendMessageCommand());
            OpenContactUsPage = new Command(async () => await ExecuteOpenContactUsPage());
            BlockUserCommand = new Command(async () => await ExecuteBlockUserCommand());
            UpdateProfileCommand = new Command(async () => await ExecuteUpdateProfileCommand());
            AccountLoadCommand = new Command(async () => await LoadAccount());
        }

        private FileResult mProfilePicture;
        public FileResult ProfilePicture
        {
            get { return mProfilePicture; }
            set
            {
                mProfilePicture = value;
                UploadProfilePicture(value);
            }
        }
        public string LocationAddress { get; set; }
        public String SetLocationAddress
        {
            get { return LocationAddress; }
            set
            {
                LocationAddress = value;
                OnPropertyChanged("LocationAddress");
            }
        }

        private Location _Location;
        public Location activeLocation
        {
            get { return _Location; }
            set
            {
                _Location = value;
                OnPropertyChanged("activeLocation");
            }
        }

        private String dateOfBirth;
        public String DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }
            set
            {
                dateOfBirth = value;
                UpdateDateProfileValue("dob", value);
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
                UpdateLocation(value);
            }
        }

        private String _NewName;
        public String NewName
        {
            get
            {
                return _NewName;
            }
            set
            {
                _NewName = value;
                UpdateDateProfileValue("name", value);
            }
        }

        private String _NewPhone;
        public String NewPhone
        {
            get
            {
                return _NewPhone;
            }
            set
            {
                _NewPhone = value;
                UpdateDateProfileValue("phone", value);
            }
        }

        private Localization _Localization;
        public Localization NewLocalization
        {
            get
            {
                return _Localization;
            }
            set
            {
                _Localization = value;
                UpdateDateProfileValue("language", value.language);
            }
        }

        public async Task UpdateLocation(LocationModule location)
        {
            isRunning = true;
            OnPropertyChanged("isRunning");
            await UserAccount.UpdateUserAddress(location);
            MessagingCenter.Send<MyAccountViewModel, String>(this, "message", "Your Request has been completed");
            isRunning = false;
            OnPropertyChanged("isRunning");

            IsBusy = true;
            OnPropertyChanged("IsBusy");
        }

        public async Task UpdateDateProfileValue(String key, String dateOfBirth)
        {
            isRunning = true;
            OnPropertyChanged("isRunning");
            await UserAccount.UpdateUserProfileValue(key, dateOfBirth);
            MessagingCenter.Send<MyAccountViewModel, String>(this, "message", "Your Request has been completed");
            isRunning = false;
            OnPropertyChanged("isRunning");

            IsBusy = true;
            OnPropertyChanged("IsBusy");
        }

        public async Task UploadProfilePicture(FileResult ProfilePicture)
        {
            isRunning = true;
            OnPropertyChanged("isRunning");
            await UserAccount.UploadProfilePhoto(ProfilePicture);
            MessagingCenter.Send<MyAccountViewModel, String>(this, "message", "Your Request has been completed");
            isRunning = false;
            OnPropertyChanged("isRunning");

            IsBusy = true;
            OnPropertyChanged("IsBusy");
        }

        private FileResult mCoverImage;
        public FileResult CoverImage
        {
            get { return mCoverImage; }
            set
            {
                mCoverImage = value;
                UploadCoverImage(value);
            }
        }

        public async Task UploadCoverImage(FileResult CoverImage)
        {
            isRunning = true;
            OnPropertyChanged("isRunning");
            await UserAccount.UploadCoverPhoto(CoverImage);
            MessagingCenter.Send<MyAccountViewModel, String>(this, "message", "Your Request has been completed");
            isRunning = false;
            OnPropertyChanged("isRunning");

            IsBusy = true;
            OnPropertyChanged("IsBusy");
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

        }

        public async Task LoadAccount()
        {
            try
            {
                Debug.WriteLine("MyAccountViewModel: LoadAccount() ");
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

                systemSettings = await SystemSettings.fetchSystemSettings();
                Debug.WriteLine("MyAccountViewModel: " + systemSettings.currency);
                OnPropertyChanged("systemSettings");

                JObject dashObj = await DashboardSummary.LoadUserDashboardSummary();
                dashboardSummary = dashObj["data"].ToObject<DashboardSummary>(serializer);
                OnPropertyChanged("dashboardSummary");

                JObject ccObj = await CustomerCareChatGroup.getCustomerCareChatGroupId();
                customerCareChatGroup = ccObj.ToObject<CustomerCareChatGroup>(serializer);
                IsBusy = false;
            }
            catch (Exception e)
            {
                IsBusy = false;
                Debug.WriteLine("MyAccountViewModel: " + e);
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
