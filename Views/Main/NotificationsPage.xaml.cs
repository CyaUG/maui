using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youth.Models;
using Youth.ViewModels;
using Youth.Views;
using System.ComponentModel;



namespace Youth.Views.Main
{

    public partial class NotificationsPage : ContentPage
    {
        NotificationsViewModel _viewModel;
        public NotificationsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new NotificationsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}