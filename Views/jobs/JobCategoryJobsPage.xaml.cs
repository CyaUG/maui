using Youth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youth.Views.jobs
{

    public partial class JobCategoryJobsPage : ContentPage
    {
        JobCategoryJobsViewModel _viewModel;
        public JobCategoryJobsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new JobCategoryJobsViewModel();
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