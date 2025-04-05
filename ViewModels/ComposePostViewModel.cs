using Youth.Models;
using Youth.ViewModels.Interfaces;
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
    public class ComposePostViewModel : BaseViewModel, IComposePostViewModel
    {
        public UserAccount userAccount { get; set; }
        public SystemSettings systemSettings { get; set; }
        //public Command CloudConfigLoadCommand { get; }
        public Command SendPostCommand { get; }
        public ObservableCollection<SafePostAudience> safePostAudiences { get; set; }
        public SafePostAudience activeSafePostAudience { get; set; }
        public bool isRunning { get; set; }
        public SafePostAudience _safePostAudience;
        public Command<SafePostAudience> SafePostAudienceNavTap { get; }

        public ComposePostViewModel()
        {
            Title = "Compose Post";
            safePostAudiences = new ObservableCollection<SafePostAudience>();
            //CloudConfigLoadCommand = new Command(async () => await ExecuteCloudConfigLoadCommand());
            SendPostCommand = new Command(async () => await ExecuteSendPostCommand());
            SafePostAudienceNavTap = new Command<SafePostAudience>(OnSafePostAudienceSelected);
            _ = ExecuteCloudConfigLoadCommand();
        }

        private String videoId;
        public String VideoId
        {
            get => videoId;
            set
            {
                SetProperty(ref videoId, value);
                OnPropertyChanged("videoId");
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

        private FileResult mPostImage;
        public FileResult PostImage
        {
            get { return mPostImage; }
            set
            {
                mPostImage = value;
            }
        }

        private bool _ghostModeEnabled;
        public bool ghostModeEnabled
        {
            get { return _ghostModeEnabled; }
            set
            {
                _ghostModeEnabled = value;
            }
        }

        public SafePostAudience SelectedSafePostAudience
        {
            get => _safePostAudience;
            set
            {
                Debug.WriteLine("SafeSpaceMainViewModel: SafePost");
                SetProperty(ref _safePostAudience, value);
                OnSafePostAudienceSelected(value);
            }
        }

        void OnSafePostAudienceSelected(SafePostAudience safePostAudience)
        {
            if (safePostAudience == null)
            {
                return;
            }
            else
            {
                activeSafePostAudience = safePostAudience;
                MessagingCenter.Send<ComposePostViewModel, SafePostAudience>(this, "safePostAudience", safePostAudience);
                OnPropertyChanged("activeSafePostAudience");
            }
        }

        private async Task ExecuteSendPostCommand()
        {
            try
            {
                bool goOn = true;
                String postContent = "text";

                if (string.IsNullOrEmpty(ReplyTextMessage))
                {
                    goOn = false;
                }

                if (PostImage != null)
                {
                    postContent = "image";
                }

                if (!string.IsNullOrEmpty(VideoId) && PostImage == null)
                {
                    postContent = "video";
                }

                if (goOn)
                {
                    if (postContent == "video" || postContent == "text")
                    {
                        isRunning = true;
                        OnPropertyChanged("isRunning");
                        await SafePost.SendSafePost(postContent, activeSafePostAudience.id, ghostModeEnabled, ReplyTextMessage, VideoId);
                        isRunning = false;
                        OnPropertyChanged("isRunning");
                        GoBack();
                    }
                    else if (postContent == "image")
                    {
                        isRunning = true;
                        OnPropertyChanged("isRunning");
                        await SafePost.SendSafePost(postContent, activeSafePostAudience.id, ghostModeEnabled, ReplyTextMessage, PostImage);
                        isRunning = false;
                        OnPropertyChanged("isRunning");
                        GoBack();
                    }
                }
                else
                {
                    MessagingCenter.Send<ComposePostViewModel, string>(this, "message", "Provide all the details to apply");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MainShoppingViewModel: " + ex);
                MessagingCenter.Send<ComposePostViewModel, string>(this, "message", "Something went wrong, try again.");
                isRunning = false;
                OnPropertyChanged("isRunning");
            }
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
            /*Set defaults*/
        }

        async Task ExecuteCloudConfigLoadCommand()
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

                systemSettings = await SystemSettings.fetchSystemSettings();
                Debug.WriteLine("MainShoppingViewModel: " + systemSettings.currency);
                OnPropertyChanged("systemSettings");

                JObject accObj = await UserAccount.LoadMyProfileAsync();
                userAccount = accObj.ToObject<UserAccount>(serializer);
                OnPropertyChanged("userAccount");

                safePostAudiences.Clear();
                JObject SafePostAudiencesObj = await SafePostAudience.fetchSafePostAudiences();
                JArray SafePostAudiencesArray = (JArray)SafePostAudiencesObj.GetValue("data");

                foreach (JToken token in SafePostAudiencesArray)
                {
                    JObject chatObj = (JObject)token;
                    SafePostAudience _chatModel = chatObj.ToObject<SafePostAudience>(serializer);
                    safePostAudiences.Add(_chatModel);
                }
                OnPropertyChanged("safePostAudiences");

                JObject AudienceObj = await SafePostAudience.fetchSafePostDefaultAudiences();
                activeSafePostAudience = AudienceObj.ToObject<SafePostAudience>(serializer);
                OnPropertyChanged("activeSafePostAudience");

                IsBusy = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MainShoppingViewModel: " + ex);
                IsBusy = false;
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("safePostAudiences");
                OnPropertyChanged("activeSafePostAudience");
                OnPropertyChanged("systemSettings");
                OnPropertyChanged("userAccount");
                OnPropertyChanged("IsBusy");
            }
        }
    }
}
