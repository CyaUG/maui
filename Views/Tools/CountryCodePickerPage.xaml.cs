using CommunityToolkit.Maui.Views;
using Youth.Utils;
using Youth.ViewModels;
using Youth.ViewModels.Interfaces;

namespace Youth.Views.Tools;

public partial class CountryCodePickerPage : ContentPage
{
    ICountryCodePickerViewModel _viewModel;
    public CountryCodePickerPage(ICountryCodePickerViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.OnAppearing();
    }

    private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private void BtnDeleteAccount(object sender, EventArgs e)
    {
        //Shell.Current.GoToAsync("..");
    }

    async void OpenCreateDeliveryRoutesPage(object sender, EventArgs e)
    {
       // await Shell.Current.GoToAsync($"{nameof(CreateDeliveryRoutesPage)}", true);
    }

    private void CollectionView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is CollectionView collectionView) collectionView.SelectedItem = null;
    }

    [Obsolete]
    private async void BtnPrivacyPolicy(object sender, EventArgs e)
    {
        var uri = new Uri(Constants.appDomain + "privacy_policy");

        await Browser.OpenAsync(uri, new BrowserLaunchOptions
        {
            LaunchMode = BrowserLaunchMode.SystemPreferred,
            TitleMode = BrowserTitleMode.Show,
        });

    }
}