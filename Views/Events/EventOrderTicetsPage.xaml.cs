using Youth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Youth.Views.Events
{

    public partial class EventOrderTicetsPage : ContentPage
    {
        EventOrderTicetsViewModel _viewModel;
        public EventOrderTicetsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new EventOrderTicetsViewModel();
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