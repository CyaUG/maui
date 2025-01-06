using Youth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Youth.Views.ReferralProgram
{

    public partial class GenderPickerPage : ContentPage
    {
        GenderPickerViewModel _viewModel;
        public GenderPickerPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new GenderPickerViewModel();
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