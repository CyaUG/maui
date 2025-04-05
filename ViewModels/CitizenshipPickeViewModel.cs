using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.jobs;
using Youth.Views.Quizzes;
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
    internal class CitizenshipPickeViewModel : BaseViewModel, ICitizenshipPickeViewModel
    {
        //public Command LoadCitizenships { get; }
        public ObservableCollection<ReferralAccountCitizenship> ReferralAccountCitizenships { get; set; }
        public Command<ReferralAccountCitizenship> ReferralAccountCitizenshipTappedCommand { get; }
        public CitizenshipPickeViewModel()
        {
            Title = "Citizenships";
            ReferralAccountCitizenships = new ObservableCollection<ReferralAccountCitizenship>();
            //LoadCitizenships = new Command(async () => await ExecuteLoadCitizenshipsCommand());
            ReferralAccountCitizenshipTappedCommand = new Command<ReferralAccountCitizenship>(ExecuteReferralAccountCitizenshipTappedCommand);
            _ = ExecuteLoadCitizenshipsCommand();
        }

        async void ExecuteReferralAccountCitizenshipTappedCommand(ReferralAccountCitizenship mReferralAccountCitizenship)
        {
            if (mReferralAccountCitizenship == null)
                return;

            MessagingCenter.Send<CitizenshipPickeViewModel, ReferralAccountCitizenship>(this, "mReferralAccountCitizenship", mReferralAccountCitizenship);
            GoBack();
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        async Task ExecuteLoadCitizenshipsCommand()
        {
            Debug.WriteLine("CitizenshipPickeViewModel: ExecuteLoadCitizenshipsCommand()");
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

                JObject serverObj = await ReferralAccountCitizenship.fetchReferralAccountCitizenships();
                JArray citizenshipsArray = (JArray)serverObj.GetValue("data");

                ReferralAccountCitizenships.Clear();
                foreach (JToken token in citizenshipsArray)
                {
                    JObject chatObj = (JObject)token;
                    ReferralAccountCitizenship citizenship = chatObj.ToObject<ReferralAccountCitizenship>(serializer);
                    ReferralAccountCitizenships.Add(citizenship);
                }
                OnPropertyChanged("ReferralAccountCitizenships");
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ChatViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("ReferralAccountCitizenships");
                OnPropertyChanged("IsBusy");
            }
        }
        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
            /*Set defaults*/
        }
    }
}
