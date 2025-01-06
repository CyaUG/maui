using Youth.Models;
using Youth.ViewModels.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Youth.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Net;


namespace Youth.ViewModels
{
    internal partial class LanguageProviderViewModel : BaseViewModel, ILanguageProviderViewModel
    {
        public ObservableCollection<Localization> Localizations { get; set; }
        public LanguageProviderViewModel()
        {
            Title = "Localizations";
            Localizations = new ObservableCollection<Localization>();
        }

        [RelayCommand]
        private async Task LoadLocalizations()
        {
            await ExecuteLoadLocalizationsCommand();
        }

        [RelayCommand]
        private async Task LocalizationNavTap(Localization localization)
        {
            if (localization == null)
                return;

            MessagingCenter.Send<LanguageProviderViewModel, Localization>(this, "localization", localization);
            GoBack();
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        async Task ExecuteLoadLocalizationsCommand()
        {
            Debug.WriteLine("LanguageProviderViewModel: ExecuteLoadLocalizationsCommand()");
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

                JObject serverObj = await Localization.LoadLocalizations();
                Localizations.Clear();
                JArray localizationArray = (JArray)serverObj.GetValue("data");

                foreach (JToken token in localizationArray)
                {
                    JObject chatObj = (JObject)token;
                    Localization mLocalization = chatObj.ToObject<Localization>(serializer);
                    Debug.WriteLine("LanguageProviderViewModel: " + mLocalization.label);
                    Localizations.Add(mLocalization);
                }
                OnPropertyChanged("Localizations");
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("LanguageProviderViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("Localizations");
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
