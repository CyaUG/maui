using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.Shopping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Youth.ViewModels.Base;


namespace Youth.ViewModels
{
    public class ShoppingOrdersViewModel : BaseViewModel, IShoppingOrdersViewModel
    {
        public Command LoadShoppingOrdersCommand { get; }
        public Command<ShoppingOrder> ShoppingOrderTapedCommand { get; }
        public ObservableCollection<ShoppingOrder> ShoppingOrders { get; }
        private ShoppingOrder _shoppingOrder;
        public SystemSettings systemSettings { get; set; }

        public ShoppingOrdersViewModel()
        {
            Title = "Orders";
            ShoppingOrders = new ObservableCollection<ShoppingOrder>();
            ShoppingOrderTapedCommand = new Command<ShoppingOrder>(OnShoppingOrderTaped);
            LoadShoppingOrdersCommand = new Command(async () => await ExecuteLoadShoppingOrdersCommand());
        }

        public ShoppingOrder ShoppingOrder
        {
            get => _shoppingOrder;
            set
            {
                Debug.WriteLine("ShoppingOrdersViewModel: SelectedChat");
                SetProperty(ref _shoppingOrder, value);
                OnShoppingOrderTaped(value);
            }
        }

        async void OnShoppingOrderTaped(ShoppingOrder shoppingOrder)
        {
            if (shoppingOrder == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ShoppingOrderProductsPage)}?{nameof(ShoppingOrderProductsViewModel.InvoiceId)}={shoppingOrder.invoiceId}");
        }

        async Task ExecuteLoadShoppingOrdersCommand()
        {
            Debug.WriteLine("ShoppingOrdersViewModel: ExecuteLoadShoppingOrdersCommand()");
            IsBusy = true;

            try
            {
                var serializer = new JsonSerializer();
                serializer.Error += (sender, args) =>
                {
                    if (args.ErrorContext.Error is JsonException)
                    {
                        args.ErrorContext.Handled = true;
                    }
                };

                systemSettings = await SystemSettings.fetchSystemSettings();
                Debug.WriteLine("MainShoppingViewModel: " + systemSettings.currency);
                OnPropertyChanged("systemSettings");

                JObject serverObj = await ShoppingOrder.fetchProductsInvoices();
                JArray brandsArray = (JArray)serverObj.GetValue("data");
                ShoppingOrders.Clear();

                foreach (JToken token in brandsArray)
                {
                    JObject chatObj = (JObject)token;
                    ShoppingOrder shoppingOrder = chatObj.ToObject<ShoppingOrder>(serializer);

                    JObject invoiceProductObj = await InvoiceProduct.fetchInvoiceProducts(shoppingOrder.invoiceId);
                    JArray invoiceProductArray = (JArray)invoiceProductObj.GetValue("data");
                    String label = "";

                    if (invoiceProductArray.Count == 1)
                    {
                        JObject productObj1 = (JObject)invoiceProductArray[0];
                        InvoiceProduct invoiceProduct1 = productObj1.ToObject<InvoiceProduct>(serializer);
                        label = invoiceProduct1.label;
                    }
                    else
                    {

                        for (int x = 0; x < invoiceProductArray.Count; x++)
                        {
                            JObject productObj2 = (JObject)invoiceProductArray[x];

                            InvoiceProduct invoiceProduct2 = productObj2.ToObject<InvoiceProduct>(serializer);
                            if (x == (invoiceProductArray.Count - 1))
                            {
                                label += invoiceProduct2.label;
                            }
                            else
                            {
                                label += invoiceProduct2.label;
                                label += " . ";
                            }
                        }
                    }
                    JObject productObj = (JObject)invoiceProductArray[0];
                    InvoiceProduct invoiceProduct = productObj.ToObject<InvoiceProduct>(serializer);
                    String imageUrl = invoiceProduct.imageUrl;
                    shoppingOrder.label = label;
                    shoppingOrder.imageUrl = imageUrl;
                    shoppingOrder.currency = systemSettings.currency;

                    ShoppingOrders.Add(shoppingOrder);
                }
                OnPropertyChanged("ShoppingBrandModels");

                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ShoppingOrdersViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("shoppingQueryParameter");
                OnPropertyChanged("ShoppingBrandModels");
                OnPropertyChanged("IsBusy");
            }
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            IsBusy = true;
            /*Set defaults*/
        }
    }
}
