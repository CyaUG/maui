using Youth.Models;
using Youth.ViewModels;
using System.Diagnostics;

namespace Youth.Views.ReferralProgram
{

    public partial class ReferralServicesProviderPage : ContentPage
    {
        ReferralServicesProviderViewModel _viewModel;
        public ReferralServicesProviderPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ReferralServicesProviderViewModel();
        }
        private void OnToggled(object sender, ToggledEventArgs e)
        {
            try
            {
                // Get the Switch control that triggered the event
                var switchControl = (Microsoft.Maui.Controls.Switch)sender;

                // Get the item associated with the Switch control
                var item = (ReferralService)switchControl.BindingContext;

                // Update the IsOwned property of the item
                item.active = e.Value;

                Debug.WriteLine("ReferralServicesProviderViewModel: " + item.label);
                Debug.WriteLine("ReferralServicesProviderViewModel: " + item.active);

                _viewModel.mReferralService = item;
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
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