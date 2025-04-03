using Youth.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Youth.Views.jobs;
using Youth.ViewModels.Interfaces;
using Youth.ViewModels.Base;

namespace Youth.ViewModels
{
    internal class JobCategoryPickerViewModel : BaseViewModel, IJobCategoryPickerViewModel
    {
        //public Command RunMainCommand { get; }
        public ObservableCollection<JobCategory> JobCategories { get; set; }
        public Command<JobCategory> JobCategoryNavTap { get; }
        public JobCategoryPickerViewModel()
        {
            Title = "Pick Job Category";
            JobCategories = new ObservableCollection<JobCategory>();
            //RunMainCommand = new Command(async () => await ExecuteMainCommand());
            JobCategoryNavTap = new Command<JobCategory>(OnJobCategorySelected);
            _ = ExecuteMainCommand();
        }

        async void OnJobCategorySelected(JobCategory jobCategory)
        {
            if (jobCategory == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            MessagingCenter.Send<JobCategoryPickerViewModel, JobCategory>(this, "jobCategory", jobCategory);
            GoBack();
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        async Task ExecuteMainCommand()
        {
            Debug.WriteLine("JobCategoryPickerViewModel: ExecuteMainCommand()");
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

                JObject jobsServerObj = await JobCategory.fetchMyJobCategories();
                JobCategories.Clear();
                JArray jobsArray = (JArray)jobsServerObj.GetValue("data");

                foreach (JToken token in jobsArray)
                {
                    JObject chatObj = (JObject)token;
                    JobCategory jobCategory = chatObj.ToObject<JobCategory>(serializer);
                    Debug.WriteLine("JobCategoryPickerViewModel: " + jobCategory.label);
                    JobCategories.Add(jobCategory);
                }
                OnPropertyChanged("JobCategories");
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("JobCategoryPickerViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("JobCategories");
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
