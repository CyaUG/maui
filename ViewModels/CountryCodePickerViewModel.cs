using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Youth.Models;
using Youth.ViewModels.Base;
using Youth.ViewModels.Interfaces;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Youth.ViewModels;

public partial class CountryCodePickerViewModel : BaseViewModel, ICountryCodePickerViewModel
{
    public ObservableCollection<Country> Countries { get; set; }

    private string _inputValue;
    public string InputValue
    {
        get { return _inputValue; }
        set
        {
            _inputValue = value;
            _ = SearchPlaces(value);
        }
    }

    public CountryCodePickerViewModel()
    {
        Countries = new ObservableCollection<Country>();
    }

    public void OnAppearing()
    {
        UpdateAuthStatus();
        _ = RunMain();
    }

    private async Task RunMain()
    {
        try
        {
            using StreamReader reader = new StreamReader(GetType().Assembly.GetManifestResourceStream("Youth.Resources.countries.json"));
            string json = reader.ReadToEnd();
            Country[] _countries = JsonConvert.DeserializeObject<Country[]>(json);
            Countries.Clear();
            foreach (Country country in _countries)
            {
                Countries.Add(country);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("CountryCodePickerViewModel: " + ex);
        }
        finally
        {
            OnPropertyChanged("Countries");
        }
    }

    [RelayCommand]
    private async Task SearchPlaces(string key)
    {
    }

    [RelayCommand]
    private async Task CountryTapped(Country country)
    {
        if (country == null)
            return;

        MessagingCenter.Send<CountryCodePickerViewModel, Country>(this, "country", country);
        GoBack();
    }
}
