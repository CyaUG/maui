using Youth.ViewModels;

namespace Youth.Views.Shopping
{

    public partial class ShoppingProductDiscussionRepliesPage : ContentPage
    {
        ShoppingProductDiscussionRepliesViewModel _viewModel;
        public ShoppingProductDiscussionRepliesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ShoppingProductDiscussionRepliesViewModel();

            MessagingCenter.Subscribe<ShoppingProductDiscussionRepliesViewModel, bool>(this, "isMessageSent", (sender, isMessageSent) =>
            {
                // Do something with the parameter, "arg" in this case
                MessageEntry.Text = string.Empty;
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void ToggleSendButton(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MessageEntry.Text))
            {
                SendButton.Source = "send_disabled";
            }
            else
            {
                SendButton.Source = "send_enabled";
            }
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OpenShoppingCart(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(ShoppingCartPage)}");
        }

        private async void OpenSearchPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(SearchProductsPage)}");
        }
    }
}