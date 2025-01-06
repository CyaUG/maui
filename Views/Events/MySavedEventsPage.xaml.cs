using Youth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youth.Views.Events
{

    public partial class MySavedEventsPage : ContentPage
    {
        MySavedEventsViewModel _viewModel;
        public MySavedEventsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MySavedEventsViewModel();
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