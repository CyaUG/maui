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
    public class Conversation : BaseModel
    {
        static readonly string TAG = "X:" + typeof(Conversation).Name;

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("chatId")]
        public String chatId { get; set; }

        [JsonProperty("message")]
        public String message { get; set; }

        [JsonProperty("label")]
        public String label { get; set; }

        [JsonProperty("messageType")]
        public String messageType { get; set; }

        [JsonProperty("status")]
        public String status { get; set; }

        [JsonProperty("senderId")]
        public int senderId { get; set; }

        [JsonProperty("recieverId")]
        public int recieverId { get; set; }

        [JsonProperty("deleted")]
        public bool deleted { get; set; }

        [JsonProperty("isReply")]
        public bool isReply { get; set; }

        [JsonProperty("replyMessageId")]
        public int replyMessageId { get; set; }

        [JsonProperty("recordTime")]
        public long recordTime { get; set; }

        [JsonProperty("replyContentType")]
        public String replyContentType { get; set; }

        [JsonProperty("replyMessageHint")]
        public String replyMessageHint { get; set; }

        [JsonProperty("sentTime")]
        public String sentTime { get; set; }

        [JsonProperty("deliverdTime")]
        public String deliverdTime { get; set; }

        [JsonProperty("readTime")]
        public String readTime { get; set; }

        [JsonProperty("ip_address")]
        public String ip_address { get; set; }

        [JsonProperty("deleteTime")]
        public String deleteTime { get; set; }

        [JsonProperty("userId")]
        public int userId { get; set; }

        [JsonProperty("phone")]
        public String phone { get; set; }

        [JsonProperty("email")]
        public String email { get; set; }

        [JsonProperty("placeId")]
        public String placeId { get; set; }

        [JsonProperty("locationLabel")]
        public String locationLabel { get; set; }

        [JsonProperty("latitude")]
        public double latitude { get; set; }

        [JsonProperty("longitude")]
        public double longitude { get; set; }

        [JsonProperty("name")]
        public String name { get; set; }

        [JsonProperty("companyDescription")]
        public String companyDescription { get; set; }

        [JsonProperty("companyEmail")]
        public String companyEmail { get; set; }

        [JsonProperty("companyPhone")]
        public String companyPhone { get; set; }

        [JsonProperty("website")]
        public String website { get; set; }

        [JsonProperty("level")]
        public int level { get; set; }

        [JsonProperty("accountType")]
        public String accountType { get; set; }

        [JsonProperty("dob")]
        public String dob { get; set; }

        [JsonProperty("gender")]
        public String gender { get; set; }

        [JsonProperty("profilePicture")]
        public String profilePicture { get; set; }

        [JsonProperty("coverPicture")]
        public String coverPicture { get; set; }

        [JsonProperty("lastSeen")]
        public String lastSeen { get; set; }

        [JsonProperty("about")]
        public String about { get; set; }

        [JsonProperty("walletBalance")]
        public double walletBalance { get; set; }

        [JsonProperty("joinDate")]
        public String joinDate { get; set; }

        [JsonProperty("contentType")]
        public String contentType { get; set; }

        [JsonProperty("firstName")]

        public String firstName { get; set; }

        [JsonProperty("lastName")]
        public String lastName { get; set; }

        [JsonProperty("custom_email")]
        public String custom_email { get; set; }

        [JsonProperty("home_email")]
        public String home_email { get; set; }

        [JsonProperty("work_email")]
        public String work_email { get; set; }

        [JsonProperty("other_email")]
        public String other_email { get; set; }

        [JsonProperty("mobile_email")]
        public String mobile_email { get; set; }

        [JsonProperty("custom_phone")]
        public String custom_phone { get; set; }

        [JsonProperty("home_phone")]
        public String home_phone { get; set; }

        [JsonProperty("mobile_phone")]
        public String mobile_phone { get; set; }

        [JsonProperty("work_phone")]
        public String work_phone { get; set; }

        [JsonProperty("fax_work_phone")]
        public String fax_work_phone { get; set; }

        [JsonProperty("fax_home_phone")]
        public String fax_home_phone { get; set; }

        [JsonProperty("pager_phone")]
        public String pager_phone { get; set; }

        [JsonProperty("other_phone")]
        public String other_phone { get; set; }

        [JsonProperty("callback_phone")]
        public String callback_phone { get; set; }

        [JsonProperty("car_phone")]
        public String car_phone { get; set; }

        [JsonProperty("company_main_phone")]
        public String company_main_phone { get; set; }

        [JsonProperty("isdn_phone")]
        public String isdn_phone { get; set; }

        [JsonProperty("main_phone")]
        public String main_phone { get; set; }

        [JsonProperty("other_fax_phone")]
        public String other_fax_phone { get; set; }

        [JsonProperty("radio_phone")]
        public String radio_phone { get; set; }

        [JsonProperty("telex_phone")]
        public String telex_phone { get; set; }

        [JsonProperty("tty_tdd_phone")]
        public String tty_tdd_phone { get; set; }

        [JsonProperty("work_mobile_phone")]
        public String work_mobile_phone { get; set; }

        [JsonProperty("work_pager_phone")]
        public String work_pager_phone { get; set; }

        [JsonProperty("assistant_phone")]
        public String assistant_phone { get; set; }

        [JsonProperty("mms_phone")]
        public String mms_phone { get; set; }

        [JsonProperty("custom_address")]
        public String custom_address { get; set; }

        [JsonProperty("home_address")]
        public String home_address { get; set; }

        [JsonProperty("work_address")]
        public String work_address { get; set; }

        [JsonProperty("other_address")]
        public String other_address { get; set; }

        [JsonProperty("mLatitude")]
        public double mLatitude { get; set; }

        [JsonProperty("mLongitude")]
        public double mLongitude { get; set; }

        [JsonProperty("mAddress")]
        public String mAddress { get; set; }

        [JsonProperty("mImage")]
        public String mImage { get; set; }


        [JsonProperty("myAccount")]
        public UserAccount myAccount { get; set; }

        public static async Task<JObject> FetchChatConversation(String chatId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_CHAT_CONVERSATION + chatId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> FetchFriendChatConversationMedia(String chatId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_CONVERSATION_MEDIA + chatId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<HttpStatusCode> SendLocationMessage(String address, Double latitude, Double longitude, string receiverId, String chatId)
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
                content.Add(new StringContent("location"), "contentType");
                content.Add(new StringContent(receiverId + ""), "recieverId");
                content.Add(new StringContent(chatId + ""), "chatId");
                content.Add(new StringContent(latitude + ""), "latitude");
                content.Add(new StringContent(longitude + ""), "longitude");
                content.Add(new StringContent(address + ""), "address");

                HttpResponseMessage response = await client.PostAsync(Constants.API_SEND_LOCATION, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("Conversation: SendLocationMessage() : " + result);

                // Return the response content
                return response.StatusCode;
            }
        }

        public static async Task<HttpStatusCode> SendMessage(String contentType, String image, String message, String receiverId, String chatId)
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
                content.Add(new StringContent("sendMessage"), "action");
                content.Add(new StringContent(contentType + ""), "contentType");
                content.Add(new StringContent(receiverId + ""), "receiverId");
                content.Add(new StringContent(chatId + ""), "chatId");
                content.Add(new StringContent(System.Uri.EscapeDataString(message)), "message");
                content.Add(new StringContent(image + ""), "image");

                HttpResponseMessage response = await client.PostAsync(Constants.API_SEND_STRING_MESSAGE, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("Conversation: SendMessage() : " + result);

                // Return the response content
                return response.StatusCode;
            }
        }

        public static async Task<HttpStatusCode> SendFile(String contentType, long recordTime, string receiverId, String chatId, FileResult FileContent)
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
                content.Add(new StringContent("contentType"), contentType);
                content.Add(new StringContent(receiverId + ""), "recieverId");
                content.Add(new StringContent(chatId + ""), "chatId");
                content.Add(new StringContent(recordTime + ""), "recordTime");

                var FileStream = await FileContent.OpenReadAsync();
                var FileStreamContent = new StreamContent(FileStream);
                FileStreamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                content.Add(FileStreamContent, "filename", FileContent.FileName);

                HttpResponseMessage response = await client.PostAsync(Constants.API_SEND_FILE, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("Conversation: SendFile() : " + result);

                // Return the response content
                return response.StatusCode;
            }
        }
    }
}
