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
    public class Event
    {
        static readonly string TAG = "X:" + typeof(EventCategory).Name;

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

        [JsonProperty("totalComments")]
        public int totalComments { get; set; }

        [JsonProperty("likes")]
        public int likes { get; set; }

        [JsonProperty("likedIt")]
        public bool likedIt { get; set; }

        [JsonProperty("isUnLiked")]
        public bool isUnLiked { get; set; }

        [JsonProperty("totalSales")]
        public double totalSales { get; set; }

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
        public int deleterId { get; set; }

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

        public static async Task<JObject> fetchEvents()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_EVENTS);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task saveEvent(int eventId)
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

                HttpResponseMessage response = await client.PostAsync(Constants.API_SAVE_EVENT, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("EventTicketModule: submitEventTicket() : " + result);
            }
        }

        public static async Task submitEventLike(int eventId)
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

                HttpResponseMessage response = await client.PostAsync(Constants.API_SUBMIT_EVENT_LIKE, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("EventTicketModule: submitEventTicket() : " + result);
            }
        }

        public static async Task<JObject> fetchMyListedEvents()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_MY_LISTED_EVENTS);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("EventTicketModule: fetchMySavedEvents() : " + result);

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchMySavedEvents()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_MY_SAVED_EVENTS);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("EventTicketModule: fetchMySavedEvents() : " + result);

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchRelatedEvents(int eventCategoryId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_RELATED_EVENTS + eventCategoryId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchEventDetails(int eventId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_EVENTS_DETAILS + eventId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchEventManagement(int eventId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_EVENT_MANAGEMENT + eventId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchEventAttendees(int eventId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_EVENT_ATTENDEES + eventId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task submitEvent(Event eventModule, FileResult EventCoverFile)
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
                content.Add(new StringContent(eventModule.eventTitle + ""), "eventTitle");
                content.Add(new StringContent(eventModule.eventCategoryId + ""), "eventCategoryId");
                content.Add(new StringContent(eventModule.description + ""), "description");
                content.Add(new StringContent(eventModule.eventDate + ""), "eventDate");
                content.Add(new StringContent(eventModule.freeEntrance + ""), "freeEntrance");
                content.Add(new StringContent(eventModule.event_address + ""), "event_address");
                content.Add(new StringContent(eventModule.event_latitude + ""), "event_latitude");
                content.Add(new StringContent(eventModule.event_longitude + ""), "event_longitude");

                var EventCoverStream = await EventCoverFile.OpenReadAsync();
                var EventCoverStreamContent = new StreamContent(EventCoverStream);
                EventCoverStreamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                content.Add(EventCoverStreamContent, "image", EventCoverFile.FileName);

                HttpResponseMessage response = await client.PostAsync(Constants.API_SUBMIT_EVENT, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("EventTicketModule: submitEventTicket() : " + result);
            }
        }

        public static async Task submitEventManager(int managerId, int eventId)
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
                content.Add(new StringContent(managerId + ""), "managerId");
                content.Add(new StringContent(eventId + ""), "eventId");

                HttpResponseMessage response = await client.PostAsync(Constants.API_SUBMIT_EVENT_MANAGER, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("Event: submitEventManager() : " + result);
            }
        }
    }
}
