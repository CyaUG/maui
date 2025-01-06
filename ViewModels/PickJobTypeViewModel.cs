using Youth.Models;
using Youth.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Youth.ViewModels.Base;



namespace Youth.ViewModels
{
    internal class PickJobTypeViewModel : BaseViewModel, IPickJobTypeViewModel
    {
        public Command<String> SubmitJobTypeNavTap { get; set; }

        public PickJobTypeViewModel()
        {
            Title = "Pick Job Type";
            SubmitJobTypeNavTap = new Command<String>(ExecuteSubmitJobCommand);
        }

        public void ExecuteSubmitJobCommand(String jobType)
        {
            if (string.IsNullOrEmpty(jobType))
                return;

            MessagingCenter.Send<PickJobTypeViewModel, String>(this, "jobType", jobType);
            GoBack();
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
        }
    }
}
