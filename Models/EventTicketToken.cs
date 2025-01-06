using Youth.Helpers.Converters;
using Youth.Models.Base;
using Youth.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;



namespace Youth.Models
{
    public class EventTicketToken
    {
        static readonly string TAG = "X:" + typeof(EventTicketToken).Name;

        [JsonProperty("tocken")]
        public String tocken { get; set; }
        [JsonProperty("userId")]
        public int userId { get; set; }
        [JsonProperty("eventId")]
        public int eventId { get; set; }
        [JsonProperty("invoiceId")]
        public String invoiceId { get; set; }
        [JsonProperty("ticketId")]
        public int ticketId { get; set; }
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
        [JsonProperty("orderDate")]
        public String orderDate { get; set; }
        [JsonProperty("ammountPaid")]
        public double ammountPaid { get; set; }
        [JsonProperty("ammount")]
        public double ammount { get; set; }
        [JsonProperty("event_address")]
        public String event_address { get; set; }
        [JsonProperty("eventTitle")]
        public String eventTitle { get; set; }
        [JsonProperty("imageUrl")]
        public String imageUrl { get; set; }
        [JsonProperty("status")]
        public String status { get; set; }
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
        public String level { get; set; }
        [JsonProperty("name")]
        public String name { get; set; }
        [JsonProperty("user_name")]
        public String user_name { get; set; }
        [JsonProperty("profile_picture")]
        public String profile_picture { get; set; }
        [JsonProperty("currency")]
        public String currency { get; set; }

        [JsonProperty("offerDiscount")]
        public bool offerDiscount { get; set; }

        public static async Task<JObject> fetchEventScannedTicketTokens(int eventId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_EVENT_SCANNED_TICKET_TOKENS + eventId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchEventTicketInfo(String ticketCode, int eventId)
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
                content.Add(new StringContent(eventId + ""), "eventId");
                content.Add(new StringContent(ticketCode + ""), "ticketCode");

                HttpResponseMessage response = await client.PostAsync(Constants.API_GET_EVENT_TICKET_INFO, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("ReferralAccount: createMyReferralAccount() : " + result);

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchEventTiketTockets(int eventId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_EVENT_TICKET_TOKEN + eventId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<bool> useEventTicket(String ticketCode, int eventId)
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
                content.Add(new StringContent(eventId + ""), "eventId");
                content.Add(new StringContent(ticketCode), "ticketCode");

                HttpResponseMessage response = await client.PostAsync(Constants.API_USE_EVENT_TICKET, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("EventTicketToken: useEventTicket() : " + result);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
