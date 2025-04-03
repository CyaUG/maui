using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.jobs;
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
    public class MainJobsViewModel : BaseViewModel, IMainJobsViewModel
    {
        public ObservableCollection<JobCategory> JobCategories { get; set; }
        public ObservableCollection<JobModel> JobModeles { get; set; }
        //public Command LoadJobCategoriesCommand { get; }
        public UserAccount userAccount { get; set; }
        public JobCategory selectedJobCategory;
        public JobModel selectedJobModel;
        public SystemSettings systemSettings { get; set; }
        public Command<JobCategory> JobCategoryNavTap { get; }
        public Command<JobModel> JobModelNavTap { get; }
        public Command SearchGridTappedCommand { get; }

        public MainJobsViewModel()
        {
            Title = "Jobs";
            JobCategories = new ObservableCollection<JobCategory>();
            JobModeles = new ObservableCollection<JobModel>();
            //LoadJobCategoriesCommand = new Command(async () => await ExecuteLoadJobCategoriesCommand());
            JobCategoryNavTap = new Command<JobCategory>(OnJobCategorySelected);
            JobModelNavTap = new Command<JobModel>(OnJobModelSelected);
            SearchGridTappedCommand = new Command(async () => await ExecuteSearchUiCommand());
            _ = ExecuteLoadJobCategoriesCommand();
        }

        public JobCategory SelectedProduct
        {
            get => selectedJobCategory;
            set
            {
                Debug.WriteLine("ChatViewModel: SelectedChat");
                SetProperty(ref selectedJobCategory, value);
                OnJobCategorySelected(value);
            }
        }

        async void OnJobCategorySelected(JobCategory jobCategory)
        {
            if (jobCategory == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(JobCategoryJobsPage)}?{nameof(JobCategoryJobsViewModel.CategoryId)}={jobCategory.id}");
        }

        public JobModel SelectedJobModel
        {
            get => selectedJobModel;
            set
            {
                Debug.WriteLine("ChatViewModel: SelectedChat");
                SetProperty(ref selectedJobModel, value);
                OnJobModelSelected(value);
            }
        }

        async void OnJobModelSelected(JobModel jobModel)
        {
            if (jobModel == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(JobDetailsPage)}?{nameof(JobDetailsViewModel.JobId)}={jobModel.id}");
        }

        async Task ExecuteSearchUiCommand() { }

        async Task ExecuteLoadJobCategoriesCommand()
        {
            Debug.WriteLine("MainJobsViewModel: ExecuteLoadJobCategoriesCommand()");
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

                systemSettings = await SystemSettings.fetchSystemSettings();
                Debug.WriteLine("MainShoppingViewModel: " + systemSettings.currency);
                OnPropertyChanged("systemSettings");

                JObject accObj = await UserAccount.LoadMyProfileAsync();
                userAccount = accObj.ToObject<UserAccount>(serializer);
                OnPropertyChanged("userAccount");

                JObject categoriesObj = await JobCategory.fetchMyJobCategories();
                JobCategories.Clear();
                JArray categoriesArray = (JArray)categoriesObj.GetValue("data");

                foreach (JToken token in categoriesArray)
                {
                    JObject chatObj = (JObject)token;
                    JobCategory safePost = chatObj.ToObject<JobCategory>(serializer);
                    Debug.WriteLine("MainJobsViewModel: " + safePost.label);
                    JobCategories.Add(safePost);
                }
                OnPropertyChanged("JobCategories");

                JObject jobsServerObj = await JobModel.fetchJobs();
                JobModeles.Clear();
                JArray jobsArray = (JArray)jobsServerObj.GetValue("data");

                foreach (JToken token in jobsArray)
                {
                    JObject chatObj = (JObject)token;
                    JobModel jobModel = chatObj.ToObject<JobModel>(serializer);
                    Debug.WriteLine("MainJobsViewModel: " + jobModel.jobTitle);
                    JobModeles.Add(jobModel);
                }
                OnPropertyChanged("JobModeles");
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MainJobsViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("JobCategories");
                OnPropertyChanged("JobModeles");
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
