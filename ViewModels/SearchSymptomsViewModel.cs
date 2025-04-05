using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.ReferralProgram;
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
    internal class SearchSymptomsViewModel : BaseViewModel, ISearchSymptomsViewModel
    {
        //public Command LoadMySymptomsCommand { get; }
        public ObservableCollection<Symptom> Symptoms { get; set; }
        public Command<Symptom> SymptomTappedCommand { get; }

        private string searchText;
        public SearchSymptomsViewModel()
        {
            Title = "Search Symptoms";
            Symptoms = new ObservableCollection<Symptom>();
            //LoadMySymptomsCommand = new Command(async () => await ExecuteLoadMySymptomsCommand());
            SymptomTappedCommand = new Command<Symptom>(OpenSymptonTappedCommand);
            _ = ExecuteLoadMySymptomsCommand();
        }

        async void OpenSymptonTappedCommand(Symptom mSymptom)
        {
            if (mSymptom == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(SymptomDetailsPage)}?{nameof(SymptomDetailsViewModel.SymptomId)}={mSymptom.id}");
        }

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged("SearchText");
                _ = FilterItems();
            }
        }

        async Task FilterItems()
        {
            if (string.IsNullOrWhiteSpace(SearchText)) { return; }

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

                JObject categoriesObj = await Symptom.searchSymptoms(SearchText);
                Symptoms.Clear();
                JArray categoriesArray = (JArray)categoriesObj.GetValue("data");

                foreach (JToken token in categoriesArray)
                {
                    JObject chatObj = (JObject)token;
                    Symptom mSymptom = chatObj.ToObject<Symptom>(serializer);
                    Debug.WriteLine("MainReferralViewModel: " + mSymptom.label);
                    Symptoms.Add(mSymptom);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MainReferralViewModel: " + ex);
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("Symptoms");
                OnPropertyChanged("IsBusy");
            }
        }

        async Task ExecuteLoadMySymptomsCommand()
        {
            Debug.WriteLine("MainReferralViewModel: ExecuteRunReferralMainCommand()");
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


                try
                {
                    JObject categoriesObj = await Symptom.fetchPopularSymptoms();
                    Symptoms.Clear();
                    JArray categoriesArray = (JArray)categoriesObj.GetValue("data");

                    foreach (JToken token in categoriesArray)
                    {
                        JObject chatObj = (JObject)token;
                        Symptom mSymptom = chatObj.ToObject<Symptom>(serializer);
                        Debug.WriteLine("MainReferralViewModel: " + mSymptom.label);
                        Symptoms.Add(mSymptom);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("MainReferralViewModel Err: " + ex);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MainReferralViewModel: " + ex);
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("Symptoms");
                OnPropertyChanged("Events");
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
