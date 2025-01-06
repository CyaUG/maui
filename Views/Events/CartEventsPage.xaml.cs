using Youth.Models;
using Youth.Utils;
using Youth.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Youth.Views.Events
{

    public partial class CartEventsPage : ContentPage
    {
        CartEventsViewModel _viewModel;
        public CartEventsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CartEventsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
        private async void OnDeleteItem(object sender, EventArgs e)
        {
            ActivityIndicator.IsRunning = true;
            try
            {
                // Get the Switch control that triggered the event
                var deleteControl = (Image)sender;

                // Get the item associated with the Switch control
                var eventCart = (EventCart)deleteControl.BindingContext;

                Debug.WriteLine("CartEventsPage: " + eventCart.eventTitle);
                Debug.WriteLine("CartEventsPage: " + eventCart.amount);
                await EventCart.deleteEventTicketCartQuantity(eventCart.cartId);
                _viewModel.OnAppearing();
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
            ActivityIndicator.IsRunning = false;
        }
        private async void IncreaseQty(object sender, EventArgs e)
        {
            ActivityIndicator.IsRunning = true;
            try
            {
                // Get the Switch control that triggered the event
                var deleteControl = (Label)sender;

                // Get the item associated with the Switch control
                var eventCart = (EventCart)deleteControl.BindingContext;

                int newQty = eventCart.orderQty;
                if (newQty < eventCart.maxOrder)
                {
                    newQty += 1;
                }
                Debug.WriteLine("CartEventsPage: " + eventCart.eventTitle);
                Debug.WriteLine("CartEventsPage: " + newQty);
                await EventCart.updateEventTicketCartQuantity(eventCart.cartId, newQty);
                _viewModel.OnAppearing();
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
            ActivityIndicator.IsRunning = false;
        }
        private async void DecreaseQty(object sender, EventArgs e)
        {
            ActivityIndicator.IsRunning = true;
            try
            {
                // Get the Switch control that triggered the event
                var deleteControl = (Label)sender;

                // Get the item associated with the Switch control
                var eventCart = (EventCart)deleteControl.BindingContext;

                int newQty = eventCart.orderQty;
                if (newQty > eventCart.minOrder)
                {
                    newQty -= 1;
                }
                Debug.WriteLine("CartEventsPage: " + eventCart.eventTitle);
                Debug.WriteLine("CartEventsPage: " + newQty);
                await EventCart.updateEventTicketCartQuantity(eventCart.cartId, newQty);
                _viewModel.OnAppearing();
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
            ActivityIndicator.IsRunning = false;
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void GoToCheckOut(object sender, EventArgs e)
        {
            ActivityIndicator.IsRunning = true;
            JObject serverObject = await EventCart.fetchCartInvoiceCheckOutUrl();
            String checkOutUrl = (String)serverObject.GetValue("checkOutUrl");
            Constants.OpenUri(checkOutUrl);
            ActivityIndicator.IsRunning = false;
        }
    }
}