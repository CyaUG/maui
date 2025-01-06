using Youth.Models;
using Youth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Youth.Views.Events
{

    public partial class EventMgmtPage : ContentPage
    {
        EventMgmtViewModel _viewModel;
        public EventMgmtPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new EventMgmtViewModel();

            MessagingCenter.Subscribe<ContactsProviderViewModel, ContactModule>(this, "contactModule", (sender, contactModule) =>
            {
                _viewModel.mContactModule = contactModule;
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

        private async void OnAddNewManager(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(ContactsProviderPage)}");
        }
    }
}