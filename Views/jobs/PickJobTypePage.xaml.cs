using Youth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Youth.Views.jobs
{

    public partial class PickJobTypePage : ContentPage
    {
        PickJobTypeViewModel _viewModel;
        public PickJobTypePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new PickJobTypeViewModel();
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