using Youth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youth.Views.Events
{

    public partial class MyEventOrdersPage : ContentPage
    {
        MyEventOrdersViewModel _viewModel;
        public MyEventOrdersPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MyEventOrdersViewModel();
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