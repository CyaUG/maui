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
    public class EventsOrder
    {
        static readonly string TAG = "X:" + typeof(EventsOrder).Name;

        [JsonProperty("id")]
        public int id { get; set; }

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

        [JsonProperty("level")]
        public int level { get; set; }

        [JsonProperty("name")]
        public String name { get; set; }

        [JsonProperty("user_name")]
        public String user_name { get; set; }

        [JsonProperty("profile_picture")]
        public String profile_picture { get; set; }

        [JsonProperty("totalTickets")]
        public int totalTickets { get; set; }

        public static async Task<JObject> fetchMyEventsOrder(String invoiceId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_MY_EVENTS_ORDER + invoiceId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchMyEventsOrders()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_MY_EVENTS_ORDERS);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }
    }
}
