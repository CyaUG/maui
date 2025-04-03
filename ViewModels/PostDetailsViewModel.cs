using Youth.Models;
using Youth.Utils;
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
    [QueryProperty(nameof(PostId), nameof(PostId))]
    public class PostDetailsViewModel : BaseViewModel, IPostDetailsViewModel
    {
        private int postId;
        public string dot { get; set; }
        public SafePost safePost { get; set; }
        //public Command LoadPostCommand { get; }
        public ObservableCollection<SafePost> SafePosts { get; set; }
        public Command<SafePost> SafePostNavTap { get; }
        public Command<SafePost> LikeSafePostNavTap { get; }
        public Command<SafePost> ShareSafePostNavTap { get; }
        public SafePost selectedSafePost;
        public Command SendPostCommand { get; }
        public bool isRunning { get; set; }

        public PostDetailsViewModel()
        {
            Title = "Safe Post";
            dot = "";
            //LoadPostCommand = new Command(async () => LoadPostDetails(postId));
            safePost = new SafePost();
            safePost.isShowingVideo = false;
            safePost.isShowingImage = false;
            SafePosts = new ObservableCollection<SafePost>();
            SafePostNavTap = new Command<SafePost>(OnSafePostSelected);
            LikeSafePostNavTap = new Command<SafePost>(OnLikeSafePost);
            ShareSafePostNavTap = new Command<SafePost>(OnShareSafePost);
            SendPostCommand = new Command(async () => await ExecuteSendPostCommand());
            LoadPostDetails(postId);
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

        private bool _ghostModeEnabled;
        public bool ghostModeEnabled
        {
            get { return _ghostModeEnabled; }
            set
            {
                _ghostModeEnabled = value;
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
                        await SafePost.SendSafePostComment(postContent, PostId, safePost.targetAudience, ghostModeEnabled, ReplyTextMessage, VideoId);
                        isRunning = false;
                        OnPropertyChanged("isRunning");
                    }
                    else if (postContent == "image")
                    {
                        isRunning = true;
                        OnPropertyChanged("isRunning");
                        await SafePost.SendSafePostComment(postContent, PostId, safePost.targetAudience, ghostModeEnabled, ReplyTextMessage, PostImage);
                        isRunning = false;
                        OnPropertyChanged("isRunning");
                    }
                    MessagingCenter.Send<PostDetailsViewModel, bool>(this, "isMessageSent", true);
                    LoadPostDetails(PostId);
                }
                else
                {
                    MessagingCenter.Send<PostDetailsViewModel, string>(this, "message", "Provide all the details to apply");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("PostDetailsViewModel: " + ex);
                MessagingCenter.Send<PostDetailsViewModel, string>(this, "message", "Something went wrong, try again.");
                isRunning = false;
                OnPropertyChanged("isRunning");
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

        public SafePost SelectedProduct
        {
            get => selectedSafePost;
            set
            {
                Debug.WriteLine("SafeSpaceMainViewModel: SafePost");
                SetProperty(ref selectedSafePost, value);
                OnSafePostSelected(value);
            }
        }

        async void OnLikeSafePost(SafePost safePost)
        {
            if (safePost == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await SafePost.submitSafePostLike(safePost.id);
            IsBusy = true;
            OnPropertyChanged("IsBusy");
        }

        async void OnShareSafePost(SafePost safePost)
        {
            if (safePost == null)
                return;

            await Share.RequestAsync(new ShareTextRequest
            {
                Text = "Hello, I want to share this cool app with you " + Constants.appDomain,
                Title = Constants.appName
            });
        }

        async void OnSafePostSelected(SafePost safePost)
        {
            if (safePost == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(PostDetailsPage)}?{nameof(PostDetailsViewModel.PostId)}={safePost.id}");
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
            /*Set defaults*/
        }

        public int PostId
        {
            get
            {
                return postId;
            }
            set
            {
                postId = value;
                LoadPostDetails(value);
            }
        }

        public async void LoadPostDetails(int postId)
        {
            try
            {
                Debug.WriteLine("PostDetailsViewModel: LoadPostDetails(), postId: " + postId);
                var serializer = new JsonSerializer();
                serializer.Error += (sender, args) =>
                {
                    if (args.ErrorContext.Error is JsonException)
                    {
                        args.ErrorContext.Handled = true;
                    }
                };

                JObject postsServerObj = await SafePost.fetchSafePostComments(postId);
                SafePosts.Clear();
                JArray chatsArray = (JArray)postsServerObj.GetValue("data");

                foreach (JToken token in chatsArray)
                {
                    JObject chatObj = (JObject)token;
                    SafePost safePost = chatObj.ToObject<SafePost>(serializer);
                    Debug.WriteLine("PostDetailsViewModel: " + safePost.message);
                    safePost.isUnLiked = !safePost.likedIt;
                    if (safePost.postContent == "video")
                    {
                        safePost.isShowingVideo = true;
                        safePost.isShowingImage = false;
                    }
                    else if (safePost.postContent == "image")
                    {
                        safePost.isShowingImage = true;
                        safePost.isShowingVideo = false;
                    }
                    else
                    {
                        safePost.isShowingVideo = false;
                        safePost.isShowingImage = false;
                    }
                    SafePosts.Add(safePost);
                }
                OnPropertyChanged("SafePosts");

                JObject serverObj = await SafePost.getSelectedSafePost(postId);
                SafePost _safePost = serverObj.ToObject<SafePost>(serializer);
                Debug.WriteLine("PostDetailsViewModel: LoadPostDetails(): " + _safePost.name);
                _safePost.isUnLiked = !_safePost.likedIt;
                if (_safePost.postContent == "video")
                {
                    _safePost.isShowingVideo = true;
                    _safePost.isShowingImage = false;
                }
                else if (_safePost.postContent == "image")
                {
                    _safePost.isShowingImage = true;
                    _safePost.isShowingVideo = false;
                }
                else
                {
                    _safePost.isShowingVideo = false;
                    _safePost.isShowingImage = false;
                }
                dot = " ◦ ";
                OnPropertyChanged("dot");
                safePost = _safePost;
                OnPropertyChanged("safePost");
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
                OnPropertyChanged("safePost");
                OnPropertyChanged("IsBusy");
            }
        }
    }

}
