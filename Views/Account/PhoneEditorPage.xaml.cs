using Newtonsoft.Json;
using Youth.Models;
using Youth.ViewModels;
using Youth.ViewModels.Interfaces;

namespace Youth.Views.Account
{

    public partial class PhoneEditorPage : ContentPage
    {
        IPhoneEditorViewModel _viewModel;
        private readonly Country[] _countries;
        private Country _selectedCountry;
        public PhoneEditorPage(IPhoneEditorViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;

            MessagingCenter.Subscribe<CountryCodePickerViewModel, Country>(this, "country", (sender, country) => {
                _selectedCountry = country;
                _countryCodeLabel.Text = _selectedCountry.CallingCode;
                _flagImage.Source = _selectedCountry.FlagUrl;
                UpdatePhoneNumber();
            });

            try
            {
                string resourceName = "Youth.Resources.countries.json";  // Use the correct name based on your assembly
                Stream stream = GetType().Assembly.GetManifestResourceStream(resourceName);

                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        string json = reader.ReadToEnd();
                        // Deserialize JSON data
                        _countries = JsonConvert.DeserializeObject<Country[]>(json);

                        Country selectedCountry = _countries.FirstOrDefault(c => c.NameCode == "UG");
                        SelectCountry(selectedCountry);
                    }
                }
                else
                {
                    Console.WriteLine($"Resource {resourceName} not found.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void SelectCountry(Country country)
        {
            _selectedCountry = country;
            _countryCodeLabel.Text = _selectedCountry.CallingCode;
            _flagImage.Source = _selectedCountry.FlagUrl;
            UpdatePhoneNumber();
        }

        private void PhoneNumberEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            _phoneNumberEntry.Text = e.NewTextValue.Trim();
            UpdatePhoneNumber();
        }

        private void UpdatePhoneNumber()
        {
            _viewModel.OnUpdatePhoneNumber(_countryCodeLabel.Text + _phoneNumberEntry.Text?.TrimStart(' ', '0').TrimEnd());
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("..");
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void OnSubmitPhone(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(PhoneValue.Text))
            //{
            //    MessagingCenter.Send<PhoneEditorViewModel, String>(this, "phone", PhoneValue.Text);
            //    await Shell.Current.GoToAsync("..");
            //}
            //else
            //{
            //    await DisplayAlert("Message", "Phone is required", "CLOSE");
            //}
        }
    }
}