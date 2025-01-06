using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Youth
{
    [Activity(Theme = "@style/SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    //[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var decorView = Window.DecorView;
            decorView.SystemUiVisibility = (StatusBarVisibility)(
                SystemUiFlags.HideNavigation |
                SystemUiFlags.ImmersiveSticky
            );

            // Get the current UI mode (day/night mode)
            var uiMode = Resources.Configuration.UiMode & UiMode.NightMask;

            // Check the UI mode and apply platform-specific changes
            ApplyThemeStyles(uiMode);
        }

        internal void ApplyThemeStyles(UiMode uiMode)
        {
            if (uiMode == UiMode.NightYes)
            {
                int colorResourceId = Resource.Color.colorPrimaryDark;

                Android.Graphics.Color androidColor = Android.Graphics.Color.ParseColor(
                    Platform.CurrentActivity.Resources.GetString(colorResourceId));

                Window.SetStatusBarColor(androidColor);
            }
            else
            {
                int colorResourceId = Resource.Color.colorPrimaryLight;

                Android.Graphics.Color androidColor = Android.Graphics.Color.ParseColor(
                    Platform.CurrentActivity.Resources.GetString(colorResourceId));

                Window.SetStatusBarColor(androidColor);
            }
        }
        private void ShowToast(string message)
        {
            Toast.MakeText(this, message, ToastLength.Short).Show();
        }
    }
}
