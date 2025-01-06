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
    public class EventTicketModule
    {
        static readonly string TAG = "X:" + typeof(EventTicketModule).Name;

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("userId")]
        public int userId { get; set; }

        [JsonProperty("eventId")]
        public int eventId { get; set; }

        [JsonProperty("label")]
        public String label { get; set; }

        [JsonProperty("description")]
        public String description { get; set; }

        [JsonProperty("amount")]
        public double amount { get; set; }

        [JsonProperty("imageUrl")]
        public String imageUrl { get; set; }

        [JsonProperty("deleted")]
        public bool deleted { get; set; }

        [JsonProperty("deleteTime")]
        public String deleteTime { get; set; }

        [JsonProperty("deleterId")]
        public int deleterId { get; set; }

        [JsonProperty("onDiscount")]
        public bool onDiscount { get; set; }

        [JsonProperty("discountPrice")]
        public double discountPrice { get; set; }

        [JsonProperty("minOrder")]
        public int minOrder { get; set; }

        [JsonProperty("maxOrder")]
        public int maxOrder { get; set; }

        [JsonProperty("ticketsInStock")]
        public int ticketsInStock { get; set; }

        [JsonProperty("orderQty")]
        public int orderQty { get; set; }

        [JsonProperty("level")]
        public int level { get; set; }

        [JsonProperty("name")]
        public String name { get; set; }

        [JsonProperty("user_name")]
        public String user_name { get; set; }

        [JsonProperty("profile_picture")]
        public String profile_picture { get; set; }

        [JsonProperty("created_at")]
        public String created_at { get; set; }

        [JsonProperty("updated_at")]
        public String updated_at { get; set; }

        [JsonProperty("currency")]
        public String currency { get; set; }

        [JsonProperty("offerDiscount")]
        public bool offerDiscount { get; set; }

        public static async Task<JObject> fetchEventTickets(int eventId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_EVENT_TICKETS + eventId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task submitEventTicket(EventTicketModule eventTicketModule, FileResult EventCoverFile)
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
                Dictionary<string, object> Map = Constants.ObjectToDictionary(eventTicketModule);
                foreach (KeyValuePair<string, object> entry in Map)
                {
                    content.Add(new StringContent(entry.Value.ToString()), entry.Key);
                }

                var EventCoverStream = await EventCoverFile.OpenReadAsync();
                var EventCoverStreamContent = new StreamContent(EventCoverStream);
                EventCoverStreamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                content.Add(EventCoverStreamContent, "image", EventCoverFile.FileName);

                HttpResponseMessage response = await client.PostAsync(Constants.API_SUBMIT_EVENT_TICKET, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("EventTicketModule: submitEventTicket() : " + result);
            }
        }

        public static async Task submitEventTicket(EventTicketModule eventTicketModule)
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
                Dictionary<string, object> Map = Constants.ObjectToDictionary(eventTicketModule);
                foreach (KeyValuePair<string, object> entry in Map)
                {
                    content.Add(new StringContent(entry.Value.ToString()), entry.Key);
                }

                HttpResponseMessage response = await client.PostAsync(Constants.API_SUBMIT_EVENT_TICKET, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("EventTicketModule: submitEventTicket() : " + result);
            }
        }
    }
}
