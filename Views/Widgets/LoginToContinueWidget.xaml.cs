using Youth.Views.Auth;

namespace Youth.Views.Widgets;

public partial class LoginToContinueWidget
{
    public LoginToContinueWidget()
    {
        InitializeComponent();
    }

    async void LoginToContinue(object sender, EventArgs eventArgs)
    {
        await Shell.Current.GoToAsync($"{nameof(WelcomePage)}", true);
    }
}