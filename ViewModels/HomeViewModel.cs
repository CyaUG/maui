using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.Events;
using Youth.Views.jobs;
using Youth.Views.Plastics;
using Youth.Views.Quizzes;
using Youth.Views.SafeSpace;
using Youth.Views.Shopping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Youth.ViewModels.Base;


namespace Youth.ViewModels
{
    public class HomeViewModel : BaseViewModel, INotifyPropertyChanged, IHomeViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public UserAccount userAccount { get; set; }
        public UserAccount quizProfile { get; set; }
        public DashboardSummary dashboardSummary { get; set; }
        public Command LoadMyAccountCommand { get; }
        public Command ShoppingGridTappedCommand { get; }
        public Command SafeSpaceGridTappedCommand { get; }
        public Command JobsGridTappedCommand { get; }
        public Command PlasticsGridTappedCommand { get; }
        public Command EventsGridTappedCommand { get; }
        public Command QuizzesGridTappedCommand { get; }
        public SystemSettings systemSettings { get; set; }

        public HomeViewModel()
        {
            Title = "Home";
            quizProfile = new UserAccount();
            quizProfile.totalPoints = 0;
            LoadMyAccountCommand = new Command(async () => await ExecuteLoadMyAccountCommand());
            ShoppingGridTappedCommand = new Command(async () => await OpenShoppingPageCommand());
            SafeSpaceGridTappedCommand = new Command(async () => await OpenSafeSpaceMainPageCommand());
            JobsGridTappedCommand = new Command(async () => await OpenMainJobsPageCommand());
            PlasticsGridTappedCommand = new Command(async () => await OpenMainPlasticsPageCommand());
            EventsGridTappedCommand = new Command(async () => await OpenMainEventsPageCommand());
            QuizzesGridTappedCommand = new Command(async () => await OpenMainQuizzesPageCommand());
            _ = ExecuteLoadMyAccountCommand();
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
        }

        async Task ExecuteLoadMyAccountCommand()
        {
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

                JObject accObj = await UserAccount.LoadMyProfileAsync();
                userAccount = accObj.ToObject<UserAccount>(serializer);
                systemSettings = await SystemSettings.fetchSystemSettings();
                Debug.WriteLine("MainShoppingViewModel: " + systemSettings.currency);
                JObject dashObj = await DashboardSummary.LoadUserDashboardSummary();
                dashboardSummary = dashObj["data"].ToObject<DashboardSummary>(serializer);
                JObject quizAccObj = await Quiz.getMyQuizProfile();
                quizProfile = quizAccObj.ToObject<UserAccount>(serializer);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("HomeViewModel: " + ex);
            }
            finally
            {
                OnPropertyChanged("userAccount");
                OnPropertyChanged("systemSettings");
                OnPropertyChanged("dashboardSummary");
                OnPropertyChanged("quizProfile");
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        async Task OpenShoppingPageCommand()
        {
            await Shell.Current.GoToAsync($"{nameof(MainShoppingPage)}");
        }
        async Task OpenSafeSpaceMainPageCommand()
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(SafeSpaceMainPage)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("HomeViewModel: " + ex);
            }
        }
        async Task OpenMainJobsPageCommand()
        {
            await Shell.Current.GoToAsync($"{nameof(MainJobsPage)}");
        }
        async Task OpenMainPlasticsPageCommand()
        {
            await Shell.Current.GoToAsync($"{nameof(MainPlasticsPage)}");
        }
        async Task OpenMainEventsPageCommand()
        {
            await Shell.Current.GoToAsync($"{nameof(MainEventsPage)}");
        }
        async Task OpenMainQuizzesPageCommand()
        {
            await Shell.Current.GoToAsync($"{nameof(MainQuizzesPage)}");
        }
    }
}
