using Youth.Helpers.Converters;
using Youth.Models.Base;
using Youth.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace Youth.Models
{
    public class ShoppingCart
    {
        static readonly string TAG = "X:" + typeof(ShoppingCart).Name;

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("userId")]
        public int userId { get; set; }

        [JsonProperty("productId")]
        public int productId { get; set; }

        [JsonProperty("colorId")]
        public int colorId { get; set; }

        [JsonProperty("sizeId")]
        public int sizeId { get; set; }

        [JsonProperty("orderQty")]
        public int orderQty { get; set; }

        [JsonProperty("categoryId")]
        public int categoryId { get; set; }

        [JsonProperty("subCategoryId")]
        public int subCategoryId { get; set; }

        [JsonProperty("brandId")]
        public int brandId { get; set; }

        [JsonProperty("brandModel")]
        public int brandModel { get; set; }

        [JsonProperty("label")]
        public String label { get; set; }

        [JsonProperty("description")]
        public String description { get; set; }

        [JsonProperty("imageUrl")]
        public String imageUrl { get; set; }

        [JsonProperty("costPrice")]
        public double costPrice { get; set; }

        [JsonProperty("sellPrice")]
        public double sellPrice { get; set; }

        [JsonProperty("onDiscount")]
        public bool onDiscount { get; set; }

        [JsonProperty("discountPrice")]
        public double discountPrice { get; set; }

        [JsonProperty("hasColorOptions")]
        public bool hasColorOptions { get; set; }

        [JsonProperty("colorOptionsRequired")]
        public bool colorOptionsRequired { get; set; }

        [JsonProperty("hasSizeOptions")]
        public bool hasSizeOptions { get; set; }

        [JsonProperty("sizeOptionsRequired")]
        public bool sizeOptionsRequired { get; set; }

        [JsonProperty("hasFeatureOptions")]
        public bool hasFeatureOptions { get; set; }

        [JsonProperty("featureOptionsRequired")]
        public bool featureOptionsRequired { get; set; }

        [JsonProperty("itemsInStock")]
        public int itemsInStock { get; set; }

        [JsonProperty("maxOrder")]
        public int maxOrder { get; set; }

        [JsonProperty("minOrder")]
        public int minOrder { get; set; }

        [JsonProperty("salesCount")]
        public int salesCount { get; set; }

        [JsonProperty("returnsCount")]
        public int returnsCount { get; set; }

        [JsonProperty("ratting")]
        public String ratting { get; set; }

        [JsonProperty("views")]
        public int views { get; set; }

        [JsonProperty("likes")]
        public int likes { get; set; }

        [JsonProperty("language")]
        public String language { get; set; }

        [JsonProperty("deleted")]
        public bool deleted { get; set; }

        [JsonProperty("deleteTime")]
        public String deleteTime { get; set; }

        [JsonProperty("deleterId")]
        public String deleterId { get; set; }

        [JsonProperty("blocked")]
        public bool blocked { get; set; }

        [JsonProperty("blockTime")]
        public String blockTime { get; set; }

        [JsonProperty("blokerId")]
        public String blokerId { get; set; }

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

        [JsonProperty("colorLabel")]
        public String colorLabel { get; set; }

        [JsonProperty("colorCode")]
        public String colorCode { get; set; }

        [JsonProperty("colorAdditionalPrice")]
        public String colorAdditionalPrice { get; set; }

        [JsonProperty("sizeLabel")]
        public String sizeLabel { get; set; }

        [JsonProperty("sizeAdditionalPrice")]
        public String sizeAdditionalPrice { get; set; }

        [JsonProperty("currency")]
        public String currency { get; set; }

        [JsonProperty("offerDiscount")]
        public bool offerDiscount { get; set; }

        public static async Task SubmitShoppingCartBuyNow(int productId, int orderQty, int colorId, int sizeId)
        {
            var valueTask = Constants.GetAuthTocken();
            var access_token = await valueTask;

            using (var client = new HttpClient())
            {
                // Set the base address for the API
                client.BaseAddress = new Uri(Constants.apiUrl);

                // Add the authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(productId + ""), "productId");
                content.Add(new StringContent(orderQty + ""), "orderQty");
                content.Add(new StringContent(colorId + ""), "colorId");
                content.Add(new StringContent(sizeId + ""), "sizeId");

                HttpResponseMessage response = await client.PostAsync(Constants.API_SUBMIT_SHOPPING_CART, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("ShoppingCart: SubmitShoppingCartBuyNow() : " + result);
            }
        }

        public static async Task<JObject> getShoppingCart()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_SHOPPING_CART);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task deleteCartItem(int cartItemId)
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
                HttpResponseMessage response = await client.DeleteAsync(Constants.API_DELETE_CART_ITEM + cartItemId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
            }
        }

        public static async Task updateCartItemQuantity(int cartItemId, int orderQty)
        {
            var valueTask = Constants.GetAuthTocken();
            var access_token = await valueTask;

            using (var client = new HttpClient())
            {
                // Set the base address for the API
                client.BaseAddress = new Uri(Constants.apiUrl);

                // Add the authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                string json = "{\"orderQty\":\"" + orderQty + "\"}";
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");


                HttpResponseMessage response = await client.PutAsync(Constants.API_UPDATE_CART_ITEM_QTY + cartItemId, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("ShoppingCart: SubmitShoppingCartBuyNow() : " + result);
            }
        }

        public static async Task<JObject> CheckoutPayOnDelivery(DeliveryAddress deliveryAddress)
        {
            var valueTask = Constants.GetAuthTocken();
            var access_token = await valueTask;

            using (var client = new HttpClient())
            {
                // Set the base address for the API
                client.BaseAddress = new Uri(Constants.apiUrl);

                // Add the authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(deliveryAddress.fullname + ""), "fullname");
                content.Add(new StringContent(deliveryAddress.phone_number + ""), "phone_number");
                content.Add(new StringContent(deliveryAddress.country + ""), "country");
                content.Add(new StringContent(deliveryAddress.city + ""), "city");
                content.Add(new StringContent(deliveryAddress.user_address + ""), "user_address");
                content.Add(new StringContent(deliveryAddress.unit_apt + ""), "unit_apt");
                content.Add(new StringContent(deliveryAddress.latitude + ""), "latitude");
                content.Add(new StringContent(deliveryAddress.longitude + ""), "longitude");
                content.Add(new StringContent(deliveryAddress.place_id + ""), "place_id");
                content.Add(new StringContent(deliveryAddress.locationAddress + ""), "locationAddress");
                content.Add(new StringContent(deliveryAddress.CountryCode + ""), "CountryCode");
                content.Add(new StringContent(deliveryAddress.AdminArea + ""), "AdminArea");
                content.Add(new StringContent(deliveryAddress.PostalCode + ""), "PostalCode");
                content.Add(new StringContent(deliveryAddress.SubAdminArea + ""), "SubAdminArea");
                content.Add(new StringContent(deliveryAddress.SubThoroughfare + ""), "SubThoroughfare");

                HttpResponseMessage response = await client.PostAsync(Constants.API_CHECKOUT_ON_DELIVERY, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("CheckOutViewModel: CheckoutPayOnDelivery()" + result);
                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> CheckoutPayWithNsiimbi(DeliveryAddress deliveryAddress)
        {
            var valueTask = Constants.GetAuthTocken();
            var access_token = await valueTask;

            using (var client = new HttpClient())
            {
                // Set the base address for the API
                client.BaseAddress = new Uri(Constants.apiUrl);

                // Add the authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(deliveryAddress.fullname + ""), "fullname");
                content.Add(new StringContent(deliveryAddress.phone_number + ""), "phone_number");
                content.Add(new StringContent(deliveryAddress.country + ""), "country");
                content.Add(new StringContent(deliveryAddress.city + ""), "city");
                content.Add(new StringContent(deliveryAddress.user_address + ""), "user_address");
                content.Add(new StringContent(deliveryAddress.unit_apt + ""), "unit_apt");
                content.Add(new StringContent(deliveryAddress.latitude + ""), "latitude");
                content.Add(new StringContent(deliveryAddress.longitude + ""), "longitude");
                content.Add(new StringContent(deliveryAddress.place_id + ""), "place_id");
                content.Add(new StringContent(deliveryAddress.locationAddress + ""), "locationAddress");
                content.Add(new StringContent(deliveryAddress.CountryCode + ""), "CountryCode");
                content.Add(new StringContent(deliveryAddress.AdminArea + ""), "AdminArea");
                content.Add(new StringContent(deliveryAddress.PostalCode + ""), "PostalCode");
                content.Add(new StringContent(deliveryAddress.SubAdminArea + ""), "SubAdminArea");
                content.Add(new StringContent(deliveryAddress.SubThoroughfare + ""), "SubThoroughfare");

                HttpResponseMessage response = await client.PostAsync(Constants.API_CHECKOUT_WITH_NSIIMBI, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("CheckOutViewModel: CheckoutPayWithNsiimbi()" + result);
                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }
    }
}
