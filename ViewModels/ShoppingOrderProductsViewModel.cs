using Youth.Models;
using Youth.ViewModels.Interfaces;
using Youth.Views.Shopping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Youth.ViewModels.Base;

namespace Youth.ViewModels
{
    [QueryProperty(nameof(InvoiceId), nameof(InvoiceId))]
    public class ShoppingOrderProductsViewModel : BaseViewModel, IShoppingOrderProductsViewModel
    {
        private String invoiceId;
        private InvoiceProduct _invoiceProduct;
        public ObservableCollection<InvoiceProduct> InvoiceProducts { get; }
        public Command<InvoiceProduct> InvoiceProductNavTap { get; }
        //public Command LoadInvoiceProductsCommand { get; }
        public SystemSettings systemSettings { get; set; }

        public ShoppingOrderProductsViewModel()
        {
            Title = "Order Items";
            InvoiceProducts = new ObservableCollection<InvoiceProduct>();
            InvoiceProductNavTap = new Command<InvoiceProduct>(OnInvoiceProductSelected);
            //LoadInvoiceProductsCommand = new Command(async () => await ExecuteLoadInvoiceProductsCommand());
            _ = ExecuteLoadInvoiceProductsCommand();
        }

        async Task ExecuteLoadInvoiceProductsCommand()
        {
            Debug.WriteLine("ShoppingOrderProductsViewModel: ExecuteLoadInvoiceProductsCommand()");
            IsBusy = true;
            LoadInvoiceProducts(invoiceId);
        }

        public InvoiceProduct InvoiceProduct
        {
            get => _invoiceProduct;
            set
            {
                Debug.WriteLine("ShoppingOrderProductsViewModel: SelectedChat");
                SetProperty(ref _invoiceProduct, value);
                OnInvoiceProductSelected(value);
            }
        }

        async void OnInvoiceProductSelected(InvoiceProduct invoiceProduct)
        {
            if (invoiceProduct == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}?{nameof(ProductDetailsViewModel.ProductId)}={invoiceProduct.productId}");
        }

        public void OnAppearing()
        {
            UpdateAuthStatus();
            /*Set defaults*/
        }

        public String InvoiceId
        {
            get
            {
                return invoiceId;
            }
            set
            {
                invoiceId = value;
                LoadInvoiceProducts(value);
            }
        }

        public async void LoadInvoiceProducts(String invoiceId)
        {
            Debug.WriteLine("ShoppingOrderProductsViewModel: ExecuteLoadShoppingOrdersCommand()");
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

                JObject serverObj = await InvoiceProduct.fetchInvoiceProducts(invoiceId);
                JArray brandsArray = (JArray)serverObj.GetValue("data");
                InvoiceProducts.Clear();

                foreach (JToken token in brandsArray)
                {
                    JObject chatObj = (JObject)token;
                    InvoiceProduct invoiceProduct = chatObj.ToObject<InvoiceProduct>(serializer);
                    invoiceProduct.currency = systemSettings.currency;
                    InvoiceProducts.Add(invoiceProduct);
                }
                OnPropertyChanged("invoiceProduct");

                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ShoppingOrderProductsViewModel: " + ex);
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("invoiceProduct");
                OnPropertyChanged("IsBusy");
            }

        }
    }
}
