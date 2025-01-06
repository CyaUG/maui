using Youth.Models;
using Youth.ViewModels;
using System.Diagnostics;

namespace Youth.Views.Shopping
{

    public partial class ShoppingCartPage : ContentPage
    {
        ShoppingCartViewModel _viewModel;
        public ShoppingCartPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ShoppingCartViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
        private async void OnDeleteItem(object sender, EventArgs e)
        {
            try
            {
                // Get the Switch control that triggered the event
                var deleteControl = (Image)sender;

                // Get the item associated with the Switch control
                var shoppingCart = (ShoppingCart)deleteControl.BindingContext;

                Debug.WriteLine("ShoppingCartPage: " + shoppingCart.label);
                Debug.WriteLine("ShoppingCartPage: " + shoppingCart.sellPrice);
                await ShoppingCart.deleteCartItem(shoppingCart.id);
                _viewModel.OnAppearing();
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
        }
        private async void IncreaseQty(object sender, EventArgs e)
        {
            try
            {
                // Get the Switch control that triggered the event
                var deleteControl = (Label)sender;

                // Get the item associated with the Switch control
                var shoppingCart = (ShoppingCart)deleteControl.BindingContext;

                int newQty = shoppingCart.orderQty;
                if (newQty < shoppingCart.maxOrder)
                {
                    newQty += 1;
                }
                Debug.WriteLine("ShoppingCartPage: " + shoppingCart.label);
                Debug.WriteLine("ShoppingCartPage: " + newQty);
                await ShoppingCart.updateCartItemQuantity(shoppingCart.id, newQty);
                _viewModel.OnAppearing();
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
        }
        private async void DecreaseQty(object sender, EventArgs e)
        {
            try
            {
                // Get the Switch control that triggered the event
                var deleteControl = (Label)sender;

                // Get the item associated with the Switch control
                var shoppingCart = (ShoppingCart)deleteControl.BindingContext;

                int newQty = shoppingCart.orderQty;
                if (newQty > shoppingCart.minOrder)
                {
                    newQty -= 1;
                }
                Debug.WriteLine("ShoppingCartPage: " + shoppingCart.label);
                Debug.WriteLine("ShoppingCartPage: " + newQty);
                await ShoppingCart.updateCartItemQuantity(shoppingCart.id, newQty);
                _viewModel.OnAppearing();
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OpenSearchPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(SearchProductsPage)}");
        }

        private async void GoToCheckOut(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(CheckOutPage)}");
        }
    }
}