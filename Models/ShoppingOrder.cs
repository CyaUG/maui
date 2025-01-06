using Youth.Helpers.Converters;
using Youth.Models.Base;
using Youth.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace Youth.Models
{
    public class ShoppingOrder : BaseModel
    {
        static readonly string TAG = "X:" + typeof(ShoppingOrder).Name;

        [JsonProperty("invoiceId")]
        public String invoiceId { get; set; }

        [JsonProperty("userId")]
        public int userId { get; set; }

        [JsonProperty("model")]
        public String model { get; set; }

        [JsonProperty("message")]
        public String message { get; set; }

        [JsonProperty("status")]
        public String status { get; set; }

        [JsonProperty("order_status")]
        public String order_status { get; set; }

        [JsonProperty("transactionId")]
        public String transactionId { get; set; }

        [JsonProperty("paymentUrl")]
        public String paymentUrl { get; set; }

        [JsonProperty("paymentMethod")]
        public String paymentMethod { get; set; }

        [JsonProperty("addressId")]
        public int addressId { get; set; }

        [JsonProperty("ammount")]
        public double ammount { get; set; }

        [JsonProperty("ammountPaid")]
        public double ammountPaid { get; set; }

        [JsonProperty("deliveryFee")]
        public double deliveryFee { get; set; }

        [JsonProperty("created_at")]
        public String created_at { get; set; }

        [JsonProperty("updated_at")]
        public String updated_at { get; set; }

        [JsonProperty("level")]
        public int level { get; set; }

        [JsonProperty("name")]
        public String name { get; set; }

        [JsonProperty("user_name")]
        public String user_name { get; set; }

        [JsonProperty("profile_picture")]
        public String profile_picture { get; set; }

        [JsonProperty("totalItems")]
        public int totalItems { get; set; }

        [JsonProperty("products")]
        public int products { get; set; }

        [JsonProperty("fullname")]
        public String fullname { get; set; }

        [JsonProperty("phone_number")]
        public String phone_number { get; set; }

        [JsonProperty("country")]
        public String country { get; set; }

        [JsonProperty("city")]
        public String city { get; set; }

        [JsonProperty("user_address")]
        public String user_address { get; set; }

        [JsonProperty("unit_apt")]
        public String unit_apt { get; set; }

        [JsonProperty("latitude")]
        public double latitude { get; set; }

        [JsonProperty("longitude")]
        public double longitude { get; set; }

        [JsonProperty("place_id")]
        public String place_id { get; set; }

        [JsonProperty("locationAddress")]
        public String locationAddress { get; set; }

        [JsonProperty("CountryCode")]
        public String CountryCode { get; set; }

        [JsonProperty("AdminArea")]
        public String AdminArea { get; set; }

        [JsonProperty("PostalCode")]
        public String PostalCode { get; set; }

        [JsonProperty("SubAdminArea")]
        public String SubAdminArea { get; set; }

        [JsonProperty("SubThoroughfare")]
        public String SubThoroughfare { get; set; }

        [JsonProperty("label")]
        public String label { get; set; }

        [JsonProperty("imageUrl")]
        public String imageUrl { get; set; }

        [JsonProperty("currency")]
        public String currency { get; set; }

        public static async Task<JObject> fetchProductsInvoices()
        {
            var valueTask = Constants.GetAuthTocken();
            var access_token = await valueTask;

            using (var client = new HttpClient())
            {
                // Set the base address for the API
                client.BaseAddress = new Uri(Constants.apiUrl);

                // Add the authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                // Make the request and get the response
                HttpResponseMessage response = await client.GetAsync(Constants.API_FETCH_PRODUCTS_INVOICES);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchProductsInvoice(String invoiceId)
        {
            var valueTask = Constants.GetAuthTocken();
            var access_token = await valueTask;

            using (var client = new HttpClient())
            {
                // Set the base address for the API
                client.BaseAddress = new Uri(Constants.apiUrl);

                // Add the authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                // Make the request and get the response
                HttpResponseMessage response = await client.GetAsync(Constants.API_FETCH_PRODUCTS_INVOICE + invoiceId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }
    }
}
