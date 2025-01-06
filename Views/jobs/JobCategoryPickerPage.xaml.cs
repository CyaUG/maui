using Youth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Youth.Views.jobs
{

    public partial class JobCategoryPickerPage : ContentPage
    {
        JobCategoryPickerViewModel _viewModel;
        public JobCategoryPickerPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new JobCategoryPickerViewModel();
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