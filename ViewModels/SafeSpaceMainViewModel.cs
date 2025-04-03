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
using Youth.Services;

namespace Youth.ViewModels
{
    public partial class SafeSpaceMainViewModel : BaseViewModel, ISafeSpaceMainViewModel
    {
        private readonly IMainApplicationServices _mainApplicationServices;
        public UserAccount userAccount { get; set; }
        //public Command LoadSafePostsCommand { get; }
        public ObservableCollection<SafePost> SafePosts { get; set; }
        public Command<SafePost> SafePostNavTap { get; }
        public ICommand LikePostNavTap { get; }
        public Command<SafePost> SharePostNavTap { get; }
        public SafePost selectedSafePost;
        public SystemSettings systemSettings { get; set; }
        public string dot { get; set; }
        public Command OpenAllSafePostsCommand { get; set; }
        public Command OpenSafePostsPhotosCommand { get; set; }
        public Command OpenSafePostsVideosCommand { get; set; }

        public SafeSpaceMainViewModel(IMainApplicationServices mainApplicationServices)
        {
            _mainApplicationServices = mainApplicationServices;
            Title = "Safe Space";
            dot = " ◦ ";
            SafePosts = new ObservableCollection<SafePost>();
            //LoadSafePostsCommand = new Command(async () => await ExecuteLoadShoppingWishListCommand());
            SafePostNavTap = new Command<SafePost>(OnSafePostSelected);
            LikePostNavTap = new Command<SafePost>(OnLikeSafePost);
            SharePostNavTap = new Command<SafePost>(OnShareSafePost);
            OpenAllSafePostsCommand = new Command(async () => await OnOpenAllSafePosts());
            OpenSafePostsPhotosCommand = new Command(async () => await OnOpenSafePostsPhotos());
            OpenSafePostsVideosCommand = new Command(async () => await OnOpenSafePostsVideos());
            _ = ExecuteLoadShoppingWishListCommand();
        }

        async Task OnOpenAllSafePosts()
        {
            Debug.WriteLine("SafeSpaceMainViewModel: OnOpenAllSafePosts()");
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

                JObject postsServerObj = await SafePost.fetchSafePosts();
                SafePosts.Clear();
                JArray chatsArray = (JArray)postsServerObj.GetValue("data");

                foreach (JToken token in chatsArray)
                {
                    JObject chatObj = (JObject)token;
                    SafePost safePost = chatObj.ToObject<SafePost>(serializer);
                    Debug.WriteLine("SafeSpaceMainViewModel: " + safePost.message);
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
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SafeSpaceMainViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("SafePosts");
                OnPropertyChanged("IsBusy");
            }
        }

        async Task OnOpenSafePostsPhotos()
        {
            Debug.WriteLine("SafeSpaceMainViewModel: OnOpenSafePostsPhotos()");
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

                JObject postsServerObj = await SafePost.fetchSafeImagePosts();
                SafePosts.Clear();
                JArray chatsArray = (JArray)postsServerObj.GetValue("data");

                foreach (JToken token in chatsArray)
                {
                    JObject chatObj = (JObject)token;
                    SafePost safePost = chatObj.ToObject<SafePost>(serializer);
                    Debug.WriteLine("SafeSpaceMainViewModel: " + safePost.message);
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
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SafeSpaceMainViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("SafePosts");
                OnPropertyChanged("IsBusy");
            }
        }

        async Task OnOpenSafePostsVideos()
        {
            Debug.WriteLine("SafeSpaceMainViewModel: OnOpenSafePostsVideos()");
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

                JObject postsServerObj = await SafePost.fetchSafeVideoPosts();
                SafePosts.Clear();
                JArray chatsArray = (JArray)postsServerObj.GetValue("data");

                foreach (JToken token in chatsArray)
                {
                    JObject chatObj = (JObject)token;
                    SafePost safePost = chatObj.ToObject<SafePost>(serializer);
                    Debug.WriteLine("SafeSpaceMainViewModel: " + safePost.message);
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
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SafeSpaceMainViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("SafePosts");
                OnPropertyChanged("IsBusy");
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

        async Task ExecuteLoadShoppingWishListCommand()
        {
            Debug.WriteLine("SafeSpaceMainViewModel: ExecuteLoadShoppingWishListCommand()");
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

                JObject postsServerObj = await SafePost.fetchSafePosts();
                SafePosts.Clear();
                JArray chatsArray = (JArray)postsServerObj.GetValue("data");

                foreach (JToken token in chatsArray)
                {
                    JObject chatObj = (JObject)token;
                    SafePost safePost = chatObj.ToObject<SafePost>(serializer);
                    Debug.WriteLine("SafeSpaceMainViewModel: " + safePost.message);
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
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SafeSpaceMainViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("SafePosts");
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
