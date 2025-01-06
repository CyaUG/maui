using Youth.ViewModels;
using Youth.Views.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Youth.Views.jobs
{

    public partial class JobDetailsPage : ContentPage
    {
        JobDetailsViewModel _viewModel;
        public JobDetailsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new JobDetailsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}