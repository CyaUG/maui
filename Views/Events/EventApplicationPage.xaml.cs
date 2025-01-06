using Youth.Models;
using Youth.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Youth.Views.Events
{

    public partial class EventApplicationPage : ContentPage
    {
        EventApplicationViewModel _viewModel;
        public EventApplicationPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new EventApplicationViewModel();

            MessagingCenter.Subscribe<EventTicketsProviderViewModel, EventTicketModule>(this, "eventTicketModule", (sender, eventTicketModule) =>
            {
                // Do something with the parameter, "arg" in this case
                _viewModel.mEventTicketModule = eventTicketModule;
            });
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