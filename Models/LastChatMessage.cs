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
    public class LastChatMessage
    {
        static readonly string TAG = "X:" + typeof(LastChatMessage).Name;

        [JsonProperty("id")]
        public String id { get; set; }

        [JsonProperty("chatType")]
        public String chatType { get; set; }

        [JsonProperty("label")]
        public String label { get; set; }

        [JsonProperty("description")]
        public String description { get; set; }

        [JsonProperty("creationTime")]
        public String creationTime { get; set; }

        [JsonProperty("logoUrl")]
        public String logoUrl { get; set; }

        [JsonProperty("coverUrl")]
        public String coverUrl { get; set; }

        [JsonProperty("contentType")]
        public String contentType { get; set; }

        [JsonProperty("message")]
        public String message { get; set; }

        [JsonProperty("messageType")]
        public String messageType { get; set; }

        [JsonProperty("status")]
        public String status { get; set; }

        [JsonProperty("senderId")]
        public int senderId { get; set; }

        [JsonProperty("chatSenderId")]
        public int chatSenderId { get; set; }

        [JsonProperty("recieverId")]
        public int recieverId { get; set; }

        [JsonProperty("isReply")]
        public bool isReply { get; set; }

        [JsonProperty("replyMessageId")]
        public String replyMessageId { get; set; }

        [JsonProperty("replyContentType")]
        public String replyContentType { get; set; }

        [JsonProperty("replyMessageHint")]
        public String replyMessageHint { get; set; }

        [JsonProperty("sentTime")]
        public String sentTime { get; set; }

        [JsonProperty("ip_address")]
        public String ip_address { get; set; }

        [JsonProperty("userId")]
        public int userId { get; set; }

        [JsonProperty("phone")]
        public String phone { get; set; }

        [JsonProperty("email")]
        public String email { get; set; }

        [JsonProperty("placeId")]
        public String placeId { get; set; }

        [JsonProperty("location_label")]
        public String location_label { get; set; }

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
        public String level { get; set; }

        [JsonProperty("account_type")]
        public String account_type { get; set; }

        [JsonProperty("dob")]
        public String dob { get; set; }

        [JsonProperty("gender")]
        public String gender { get; set; }

        [JsonProperty("email_verified")]
        public String email_verified { get; set; }

        [JsonProperty("phone_verified")]
        public String phone_verified { get; set; }

        [JsonProperty("joinDate")]
        public String joinDate { get; set; }

        [JsonProperty("lastSeen")]
        public String lastSeen { get; set; }

        [JsonProperty("profile_picture")]
        public String profile_picture { get; set; }

        [JsonProperty("cover_picture")]
        public String cover_picture { get; set; }

        [JsonProperty("about")]
        public String about { get; set; }

        [JsonProperty("wallet_balance")]
        public double wallet_balance { get; set; }

        [JsonProperty("myUserId")]
        public int myUserId { get; set; }

        public static async Task<JObject> FetchConversations()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_CHATS);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> GetNewConversationMessages()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_CHATS_MESSAGES);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }
    }
}
