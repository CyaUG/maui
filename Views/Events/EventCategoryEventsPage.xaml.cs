using Youth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Youth.Views.Events
{

    public partial class EventCategoryEventsPage : ContentPage
    {
        EventCategoryEventsViewModel _viewModel;
        public EventCategoryEventsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new EventCategoryEventsViewModel();
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