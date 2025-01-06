using Youth.Models;
using Youth.Services;
using Youth.ViewModels;
using Youth.ViewModels.Interfaces;
using Youth.Views.Account;
using Youth.Views.Quizzes;
using System.Diagnostics;

namespace Youth.Views.Main
{
    public partial class HomePage : ContentPage
    {
        IHomeViewModel _viewModel;
        private LocationModule selectedPosition;
        static bool isSetup = false;
        public HomePage(IHomeViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
            if (!isSetup)
            {
                isSetup = true;

                SetupAppActions();
                SetupTrayIcon();
            }
        }

        public async Task<LocationModule> GetPositionAsync()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    var position = new LocationModule();
                    position.Latitude = location.Latitude;
                    position.Longitude = location.Longitude;
                    return position;
                }
                else
                {
                    Console.WriteLine("Could not get the location");
                    return new LocationModule();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new LocationModule();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void SetupAppActions()
        {
            try
            {
#if WINDOWS
            //AppActions.IconDirectory = Application.Current.On<WindowsConfiguration>().GetImageDirectory();
#endif
                AppActions.SetAsync(
                    new AppAction(nameof(MainQuizzesPage), "Quizzes", icon: "bag"),
                    new AppAction(nameof(ShoppingCart), "Shopping Cart", icon: "shopping_cart")
                );

                AppActions.OnAppAction += async (sender, args) =>
                {
                    if (args.AppAction.Id == nameof(MainQuizzesPage))
                    {
                        // Handle the "Quizzes" action, e.g., navigate to the Order History page.
                        await Shell.Current.GoToAsync($"{nameof(MainQuizzesPage)}", true);
                    }

                    if (args.AppAction.Id == nameof(ShoppingCart))
                    {
                        // Handle the "Quizzes" action, e.g., navigate to the Order History page.
                        await Shell.Current.GoToAsync($"{nameof(ShoppingCart)}", true);
                    }
                };
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("App Actions not supported", ex);
            }
        }

        private async void ProfileImage_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(MyAccountPage)}");
        }

        private async void BtnVisitWagaana(object sender, EventArgs e)
        {
            var uri = new Uri("https://wagaana.com");

            await Browser.OpenAsync(uri, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
            });
        }

        private void SetupTrayIcon()
        {
            var trayService = ServiceProvider.GetService<ITrayService>();

            if (trayService != null)
            {
                trayService.Initialize();
                trayService.ClickHandler = () =>
                    ServiceProvider.GetService<INotificationService>()
                        ?.ShowNotification("CYA", "Your Marketplace");
            }
        }
    }
}