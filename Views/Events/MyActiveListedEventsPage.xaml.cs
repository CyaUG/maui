using Youth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youth.Views.Events
{

    public partial class MyActiveListedEventsPage : ContentPage
    {
        MyActiveListedEventsViewModel _viewModel;
        public MyActiveListedEventsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MyActiveListedEventsViewModel();
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