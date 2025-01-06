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
    public class EventCart
    {
        static readonly string TAG = "X:" + typeof(EventCart).Name;

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("cartId")]
        public int cartId { get; set; }

        [JsonProperty("userId")]
        public int userId { get; set; }

        [JsonProperty("eventTitle")]
        public String eventTitle { get; set; }

        [JsonProperty("eventCategoryId")]
        public int eventCategoryId { get; set; }

        [JsonProperty("description")]
        public String description { get; set; }

        [JsonProperty("imageUrl")]
        public String imageUrl { get; set; }

        [JsonProperty("currency")]
        public String currency { get; set; }

        [JsonProperty("eventDate")]
        public String eventDate { get; set; }

        [JsonProperty("freeEntrance")]
        public bool freeEntrance { get; set; }

        [JsonProperty("certified")]
        public bool certified { get; set; }

        [JsonProperty("approved")]
        public bool approved { get; set; }

        [JsonProperty("interested")]
        public int interested { get; set; }

        [JsonProperty("shareCount")]
        public int shareCount { get; set; }

        [JsonProperty("viewCount")]
        public int viewCount { get; set; }

        [JsonProperty("ip_address")]
        public String ip_address { get; set; }

        [JsonProperty("event_address")]
        public String event_address { get; set; }

        [JsonProperty("event_latitude")]
        public double event_latitude { get; set; }

        [JsonProperty("event_longitude")]
        public double event_longitude { get; set; }

        [JsonProperty("event_address_image")]
        public String event_address_image { get; set; }

        [JsonProperty("deleted")]
        public bool deleted { get; set; }

        [JsonProperty("deleteTime")]
        public String deleteTime { get; set; }

        [JsonProperty("deleterId")]
        public String deleterId { get; set; }

        [JsonProperty("created_at")]
        public String created_at { get; set; }

        [JsonProperty("updated_at")]
        public String updated_at { get; set; }

        [JsonProperty("orderQty")]
        public int orderQty { get; set; }

        [JsonProperty("ticketLabel")]
        public String ticketLabel { get; set; }

        [JsonProperty("ticketDescription")]
        public String ticketDescription { get; set; }

        [JsonProperty("amount")]
        public double amount { get; set; }

        [JsonProperty("ticketImageUrl")]
        public String ticketImageUrl { get; set; }

        [JsonProperty("onDiscount")]
        public bool onDiscount { get; set; }

        [JsonProperty("discountPrice")]
        public double discountPrice { get; set; }

        [JsonProperty("maxOrder")]
        public int maxOrder { get; set; }

        [JsonProperty("minOrder")]
        public int minOrder { get; set; }

        [JsonProperty("salesCount")]
        public int salesCount { get; set; }

        [JsonProperty("ticketsInStock")]
        public int ticketsInStock { get; set; }

        [JsonProperty("level")]
        public int level { get; set; }

        [JsonProperty("name")]
        public String name { get; set; }

        [JsonProperty("user_name")]
        public String user_name { get; set; }

        [JsonProperty("profile_picture")]
        public String profile_picture { get; set; }

        [JsonProperty("offerDiscount")]
        public bool offerDiscount { get; set; }

        public static async Task<JObject> fetchMyCartEvents()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_MY_CART_EVENTS);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task saveToEventCart(int ticketId, int eventId, int orderQty)
        {
            var valueTask = Constants.GetAuthTocken();
            var access_token = await valueTask;

            using (var client = new HttpClient())
            {
                // Set the base address for the API
                client.BaseAddress = new Uri(Constants.apiUrl);

                var myData = new
                {
                    ticketId = ticketId + "",
                    eventId = eventId + "",
                    orderQty = orderQty + ""
                };
                string jsonData = JsonConvert.SerializeObject(myData);

                // Add the authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(Constants.API_SAVE_EVENT_TO_CART, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("EventCart: saveToEventCart() : " + result);
            }
        }

        public static async Task<JObject> deleteEventTicketCartQuantity(int cartItemId)
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
                HttpResponseMessage response = await client.DeleteAsync(Constants.API_DELETE_TICKET_CART_ITEM + cartItemId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task updateEventTicketCartQuantity(int cartItemId, int orderQty)
        {
            var valueTask = Constants.GetAuthTocken();
            var access_token = await valueTask;

            using (var client = new HttpClient())
            {
                // Set the base address for the API
                client.BaseAddress = new Uri(Constants.apiUrl);

                var myData = new
                {
                    cartItemId = cartItemId + "",
                    orderQty = orderQty + ""
                };
                string jsonData = JsonConvert.SerializeObject(myData);

                // Add the authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(Constants.API_UPDATE_TICKET_CART_ITEM_QTY, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("EventCart: saveToEventCart() : " + result);
            }
        }

        public static async Task<JObject> fetchCartInvoiceCheckOutUrl()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_CREATE_CART_INVOICE);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }
    }
}
