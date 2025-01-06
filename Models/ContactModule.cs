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
    public class ContactModule
    {
        static readonly string TAG = "X:" + typeof(ContactModule).Name;

        [JsonProperty("chatId")]
        public String chatId { get; set; }

        [JsonProperty("userId")]
        public int userId { get; set; }

        [JsonProperty("friendId")]
        public int friendId { get; set; }

        [JsonProperty("approved")]
        public bool approved { get; set; }

        [JsonProperty("blocked")]
        public bool blocked { get; set; }

        [JsonProperty("blockerId")]
        public int blockerId { get; set; }

        [JsonProperty("timeAdded")]
        public String timeAdded { get; set; }

        [JsonProperty("blockeTime")]
        public String blockeTime { get; set; }

        [JsonProperty("accountId")]
        public int accountId { get; set; }

        [JsonProperty("phone")]
        public String phone { get; set; }

        [JsonProperty("email")]
        public String email { get; set; }

        [JsonProperty("place_id")]
        public String place_id { get; set; }

        [JsonProperty("location_label")]
        public String location_label { get; set; }

        [JsonProperty("user_name")]
        public String user_name { get; set; }

        [JsonProperty("latitude")]
        public double latitude { get; set; }

        [JsonProperty("longitude")]
        public double longitude { get; set; }

        [JsonProperty("name")]
        public String name { get; set; }

        [JsonProperty("company_description")]
        public String company_description { get; set; }

        [JsonProperty("company_email")]
        public String company_email { get; set; }

        [JsonProperty("company_phone")]
        public String company_phone { get; set; }

        [JsonProperty("website")]
        public String website { get; set; }

        [JsonProperty("level")]
        public int level { get; set; }

        [JsonProperty("account_type")]
        public String account_type { get; set; }

        [JsonProperty("dob")]
        public String dob { get; set; }

        [JsonProperty("gender")]
        public String gender { get; set; }

        [JsonProperty("email_verified")]
        public bool email_verified { get; set; }

        [JsonProperty("phone_verified")]
        public bool phone_verified { get; set; }

        [JsonProperty("profile_picture")]
        public String profile_picture { get; set; }

        [JsonProperty("cover_picture")]
        public String cover_picture { get; set; }

        [JsonProperty("about")]
        public String about { get; set; }

        [JsonProperty("app_version_code")]
        public int app_version_code { get; set; }

        [JsonProperty("lastLoginTime")]
        public String lastLoginTime { get; set; }

        [JsonProperty("app_version")]
        public String app_version { get; set; }

        [JsonProperty("wallet_balance")]
        public double wallet_balance { get; set; }

        [JsonProperty("created_at")]
        public String created_at { get; set; }

        [JsonProperty("updated_at")]
        public String updated_at { get; set; }

        [JsonProperty("last_seen")]
        public String last_seen { get; set; }

        [JsonProperty("outstanding_payments")]
        public double outstanding_payments { get; set; }

        public static async Task<JObject> LoadUserContacts()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_FETCH_FRIENDS);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> CheckFriendshipStatus(int friendId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_CHECK_FRIENDSHIP_STATUS + friendId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task AddContacts(int friendId)
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
                content.Add(new StringContent(friendId + ""), "friendId");

                HttpResponseMessage response = await client.PostAsync(Constants.API_CREATE_NEW_FRIEND, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("Event: submitEventManager() : " + result);
            }
        }
    }
}
