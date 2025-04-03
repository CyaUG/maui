using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.jobs;
using Youth.Views.Shopping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Youth.ViewModels.Base;

namespace Youth.ViewModels
{
    [QueryProperty(nameof(CategoryId), nameof(CategoryId))]
    public class JobCategoryJobsViewModel : BaseViewModel, IJobCategoryJobsViewModel
    {
        private int categoryId;
        public ObservableCollection<JobModel> JobModeles { get; set; }
        //public Command LoadJobCategoriesCommand { get; }
        public Command<JobModel> JobModelNavTap { get; }
        public JobModel selectedJobModel;

        public JobCategoryJobsViewModel()
        {
            Title = "Category Jobs";
            JobModeles = new ObservableCollection<JobModel>();
            //LoadJobCategoriesCommand = new Command(async () => LoadShoppingBrands(CategoryId));
            JobModelNavTap = new Command<JobModel>(OnJobModelSelected);
            LoadShoppingBrands(CategoryId);
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

        public int CategoryId
        {
            get
            {
                return categoryId;
            }
            set
            {
                categoryId = value;
                LoadShoppingBrands(value);
            }
        }

        public async void LoadShoppingBrands(int categoryId)
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

                JObject jobsServerObj = await JobModel.fetchRelatedJobs(categoryId);
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
