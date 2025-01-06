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
    public class UserAccount : BaseModel
    {
        static readonly string TAG = "X:" + typeof(UserAccount).Name;

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("name")]
        public String name { get; set; }

        [JsonProperty("email")]
        public String email { get; set; }

        [JsonProperty("phone")]
        public String phone { get; set; }

        [JsonProperty("user_name")]
        public String user_name { get; set; }

        [JsonProperty("place_id")]
        public String place_id { get; set; }

        [JsonProperty("location_label")]
        public String location_label { get; set; }

        [JsonProperty("latitude")]
        public double latitude { get; set; }

        [JsonProperty("longitude")]
        public double longitude { get; set; }

        [JsonProperty("trackingEnabled")]
        public bool trackingEnabled { get; set; }

        [JsonProperty("address_label")]
        public String address_label { get; set; }

        [JsonProperty("address_country_code")]
        public String address_country_code { get; set; }

        [JsonProperty("address_country")]
        public String address_country { get; set; }

        [JsonProperty("address_placeId")]
        public String address_placeId { get; set; }

        [JsonProperty("address_latitude")]
        public double address_latitude { get; set; }

        [JsonProperty("address_longitude")]
        public double address_longitude { get; set; }

        [JsonProperty("company_description")]
        public String company_description { get; set; }

        [JsonProperty("company_email")]
        public String company_email { get; set; }

        [JsonProperty("company_phone")]
        public String company_phone { get; set; }

        [JsonProperty("website")]
        public String website { get; set; }

        [JsonProperty("language")]
        public String language { get; set; }

        [JsonProperty("level")]
        public int level { get; set; }

        [JsonProperty("chatId")]
        public int chatId { get; set; }

        [JsonProperty("account_type")]
        public String account_type { get; set; }

        [JsonProperty("dob")]
        public String dob { get; set; }

        [JsonProperty("gender")]
        public String gender { get; set; }

        [JsonProperty("profile_picture")]
        public String profile_picture { get; set; }

        [JsonProperty("cover_picture")]
        public String cover_picture { get; set; }

        [JsonProperty("about")]
        public String about { get; set; }

        [JsonProperty("wallet_balance")]
        public double wallet_balance { get; set; }

        [JsonProperty("outstanding_payments")]
        public long outstanding_payments { get; set; }

        [JsonProperty("last_seen")]
        public String last_seen { get; set; }

        [JsonProperty("email_verified")]
        public String email_verified { get; set; }

        [JsonProperty("phone_verified")]
        public String phone_verified { get; set; }

        [JsonProperty("app_version_code")]
        public double app_version_code { get; set; }

        [JsonProperty("app_version")]
        public String app_version { get; set; }

        [JsonProperty("password")]
        public String password { get; set; }

        [JsonProperty("lastLoginTime")]
        public double lastLoginTime { get; set; }

        [JsonProperty("created_at")]
        public String created_at { get; set; }

        [JsonProperty("updated_at")]
        public String updated_at { get; set; }

        [JsonProperty("api_token")]
        public String api_token { get; set; }

        [JsonProperty("message")]
        public String message { get; set; }

        [JsonProperty("access_token")]
        public String access_token { get; set; }

        [JsonProperty("token_type")]
        public String token_type { get; set; }

        [JsonProperty("status")]
        public int status { get; set; }

        [JsonProperty("totalPoints")]
        public int totalPoints { get; set; }

        [JsonProperty("eventId")]
        public int eventId { get; set; }

        [JsonProperty("role")]
        public String role { get; set; }

        [JsonProperty("myAccount")]
        public UserAccount myAccount { get; set; }

        public static async Task<JObject> LoginAsync(string email, string password)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Constants.apiUrl);

            var myData = new
            {
                email = email,
                password = password
            };
            string jsonData = JsonConvert.SerializeObject(myData);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("login", content);

            var result = await response.Content.ReadAsStringAsync();
            JObject obj = JObject.Parse(result);
            return obj;
        }

        public static async Task<JObject> AddProfile(UserAccount userAccount)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Constants.apiUrl);

            var myData = new
            {
                name = userAccount.name,
                email = userAccount.email,
                password = userAccount.password,
                password_confirmation = userAccount.password
            };
            string jsonData = JsonConvert.SerializeObject(myData);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("register", content);

            var result = await response.Content.ReadAsStringAsync();
            JObject obj = JObject.Parse(result);
            return obj;
        }

        public static async Task<JObject> LoadMyProfileAsync()
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
                HttpResponseMessage response = await client.GetAsync("getUserProfile");

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> LoadUserProfile(int userId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_SELECTED_USER_PROFILE + userId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> LoadProfileAsync(string userId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_SELECTED_USER_PROFILE + userId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> SearchUsers(string searchText)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_SEARCH_USERS + searchText);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> getContactsUsers()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_CONTACT_USERS);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task UploadProfilePhoto(FileResult ProfilePictureFile)
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

                var ProfilePictureStream = await ProfilePictureFile.OpenReadAsync();
                var ProfilePictureStreamContent = new StreamContent(ProfilePictureStream);
                ProfilePictureStreamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                content.Add(ProfilePictureStreamContent, "image", ProfilePictureFile.FileName);

                HttpResponseMessage response = await client.PostAsync(Constants.API_UPLOAD_PROFILE_PHOTO, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("UserAccount: UploadProfilePhoto() : " + result);
            }
        }

        public static async Task UploadCoverPhoto(FileResult ProfileCoverFile)
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

                var CoverPictureStream = await ProfileCoverFile.OpenReadAsync();
                var CoverPictureStreamContent = new StreamContent(CoverPictureStream);
                CoverPictureStreamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                content.Add(CoverPictureStreamContent, "image", ProfileCoverFile.FileName);

                HttpResponseMessage response = await client.PostAsync(Constants.API_UPLOAD_COVER_PHOTO, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("UserAccount: UploadCoverPhoto() : " + result);
            }
        }

        public static async Task UpdateUserAddress(LocationModule location)
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
                content.Add(new StringContent(location.Latitude + ""), "latitude");
                content.Add(new StringContent(location.Longitude + ""), "longitude");
                content.Add(new StringContent(location.Address), "address");
                content.Add(new StringContent("updateUserAddress"), "action");

                HttpResponseMessage response = await client.PostAsync(Constants.API_UPDATE_USER_ADDRESS, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("UserAccount: UpdateUserAddress() : " + result);
            }
        }

        public static async Task UpdateUserProfileValue(String editSubject, String editValue)
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
                content.Add(new StringContent(editSubject + ""), "editSubject");
                content.Add(new StringContent(editValue + ""), "editValue");

                HttpResponseMessage response = await client.PostAsync(Constants.API_UPDATE_USER_PROFILE_VALUE, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("UserAccount: UpdateUserProfileValue() : " + result);
            }
        }

        public static async Task<HttpStatusCode> UpdatePassword(String old_password, String new_password, String confirm_password)
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
                content.Add(new StringContent("updatePassword"), "action");
                content.Add(new StringContent(old_password), "old_password");
                content.Add(new StringContent(new_password), "new_password");
                content.Add(new StringContent(confirm_password), "confirm_password");

                HttpResponseMessage response = await client.PostAsync(Constants.API_CHANGE_PASSWORD, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("UserAccount: UpdatePassword() : " + result);
                return response.StatusCode;
            }
        }

        public static async Task<HttpStatusCode> UpdateUserLocation(double latitude, double longitude)
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
                content.Add(new StringContent("updateUserLocation"), "action");
                content.Add(new StringContent(latitude + ""), "latitude");
                content.Add(new StringContent(longitude + ""), "longitude");

                HttpResponseMessage response = await client.PostAsync(Constants.API_UPDATE_USER_LOCATION, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("UserAccount: UpdateUserLocation() : " + result);
                return response.StatusCode;
            }
        }

        public static async Task<HttpResponseMessage> DeleteUserAccount(String password)
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
                content.Add(new StringContent(password), "password");

                HttpResponseMessage response = await client.PostAsync(Constants.API_DELETE_USER_ACCOUNT, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("UserAccount: DeleteUserAccount() : " + result);
                return response;
            }
        }

        public static async Task<HttpStatusCode> ForgotPassword(String email)
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
                content.Add(new StringContent(email), "email");

                HttpResponseMessage response = await client.PostAsync(Constants.API_FORGOT_PASSWORD, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("UserAccount: ForgotPassword() : " + result);

                // Return the response content
                return response.StatusCode;
            }
        }
    }
}
