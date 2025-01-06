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
    public class Referral
    {
        static readonly string TAG = "X:" + typeof(Referral).Name;


        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("about")]
        public String about { get; set; }

        [JsonProperty("accCategoryId")]
        public int accCategoryId { get; set; }

        [JsonProperty("address_label")]
        public String address_label { get; set; }

        [JsonProperty("address_latitude")]
        public double address_latitude { get; set; }

        [JsonProperty("address_longitude")]
        public double address_longitude { get; set; }

        [JsonProperty("birthDistrict")]
        public String birthDistrict { get; set; }

        [JsonProperty("birthSubCounty")]
        public String birthSubCounty { get; set; }

        [JsonProperty("citizenshipId")]
        public String citizenshipId { get; set; }

        [JsonProperty("created_at")]
        public String created_at { get; set; }

        [JsonProperty("appointmentDate")]
        public String appointmentDate { get; set; }

        [JsonProperty("dateOfBirth")]
        public String dateOfBirth { get; set; }

        [JsonProperty("deleteTime")]
        public String deleteTime { get; set; }

        [JsonProperty("deleted")]
        public bool deleted { get; set; }

        [JsonProperty("deleterId")]
        public int deleterId { get; set; }

        [JsonProperty("referralId")]
        public int referralId { get; set; }

        [JsonProperty("email")]
        public String email { get; set; }

        [JsonProperty("problem")]
        public String problem { get; set; }

        [JsonProperty("referralImageUrl")]
        public String referralImageUrl { get; set; }

        [JsonProperty("genderId")]
        public String genderId { get; set; }

        [JsonProperty("name")]
        public String name { get; set; }

        [JsonProperty("nationalIdUrl")]
        public String nationalIdUrl { get; set; }

        [JsonProperty("nin")]
        public String nin { get; set; }

        [JsonProperty("phone")]
        public String phone { get; set; }

        [JsonProperty("profile_picture")]
        public String profile_picture { get; set; }

        [JsonProperty("publisherId")]
        public String publisherId { get; set; }

        [JsonProperty("remindDate")]
        public String remindDate { get; set; }

        [JsonProperty("status")]
        public String status { get; set; }

        [JsonProperty("token")]
        public String token { get; set; }

        [JsonProperty("updated_at")]
        public String updated_at { get; set; }

        [JsonProperty("userId")]
        public int userId { get; set; }

        [JsonProperty("assigneeLavel")]
        public int assigneeLavel { get; set; }

        [JsonProperty("assigneeId")]
        public int assigneeId { get; set; }

        [JsonProperty("hc_id")]
        public int hc_id { get; set; }

        [JsonProperty("hc_label")]
        public String hc_label { get; set; }

        [JsonProperty("hc_imageUrl")]
        public String hc_imageUrl { get; set; }

        [JsonProperty("hc_email")]
        public String hc_email { get; set; }

        [JsonProperty("hc_phone")]
        public String hc_phone { get; set; }

        [JsonProperty("hc_website")]
        public String hc_website { get; set; }

        [JsonProperty("hc_userId")]
        public int hc_userId { get; set; }

        [JsonProperty("hc_about")]
        public String hc_about { get; set; }

        [JsonProperty("hc_district")]
        public String hc_district { get; set; }

        [JsonProperty("hc_subCounty")]
        public String hc_subCounty { get; set; }

        [JsonProperty("ch_address_label")]
        public String ch_address_label { get; set; }

        [JsonProperty("ch_address_latitude")]
        public double ch_address_latitude { get; set; }

        [JsonProperty("hc_address_longitude")]
        public double hc_address_longitude { get; set; }

        [JsonProperty("assigneeName")]
        public String assigneeName { get; set; }

        [JsonProperty("assigneeUserName")]
        public String assigneeUserName { get; set; }

        [JsonProperty("assigneeEmail")]
        public String assigneeEmail { get; set; }

        [JsonProperty("assigneePhone")]
        public String assigneePhone { get; set; }

        [JsonProperty("assigneeProfilePicture")]
        public String assigneeProfilePicture { get; set; }

        [JsonProperty("citizenshipLabel")]
        public String citizenshipLabel { get; set; }

        [JsonProperty("citizenshipDescription")]
        public String citizenshipDescription { get; set; }

        [JsonProperty("accountCategoryLabel")]
        public String accountCategoryLabel { get; set; }

        [JsonProperty("accountCategoryDescription")]
        public String accountCategoryDescription { get; set; }

        [JsonProperty("chatId")]
        public String chatId { get; set; }

        public static async Task<JObject> fetchAllMyPeerEducationReferrals()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_ALL_MY_PEER_EDUCATION_REFERRALS);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchAllMyAssigneeReferrals()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_ALL_MY_ASSIGNEE_REFERRALS);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchAllMyReferrals()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_ALL_MY_REFERRALS);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchReferralDetails(int referralId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_REFERRAL_DETAILS + referralId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("ReferralDetailsViewModel: " + result);

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task updateReferralHealthCenter(int referralId, int healthCenterId)
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
                content.Add(new StringContent(referralId + ""), "referralId");
                content.Add(new StringContent(healthCenterId + ""), "healthCenterId");

                HttpResponseMessage response = await client.PostAsync(Constants.API_UPDATE_REFERRAL_HEALTH_CENTER, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("ReferralAccount: createMyReferralAccount() : " + result);
            }
        }

        public static async Task submitSecondaryReferral(int secondaryUserId, int primaryUserId, int referralId)
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
                content.Add(new StringContent(secondaryUserId + ""), "secondaryUserId");
                content.Add(new StringContent(primaryUserId + ""), "primaryUserId");
                content.Add(new StringContent(referralId + ""), "referralId");

                HttpResponseMessage response = await client.PostAsync(Constants.API_SUBMIT_SECONDARY_REFERRAL, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("ReferralAccount: createMyReferralAccount() : " + result);
            }
        }

        public static async Task setReferralAppointment(String appointmentDate, int referralId)
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
                content.Add(new StringContent(referralId + ""), "referralId");
                content.Add(new StringContent(appointmentDate + ""), "appointmentDate");

                HttpResponseMessage response = await client.PostAsync(Constants.API_UPDATE_REFERRAL_APPOINTMENT, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("ReferralAccount: createMyReferralAccount() : " + result);
            }
        }

        public static async Task SubmitReferral(String token, int healthCenterId, String remindDate, String problem)
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
                content.Add(new StringContent(token), "token");
                content.Add(new StringContent(healthCenterId + ""), "healthCenterId");
                content.Add(new StringContent(remindDate), "remindDate");
                content.Add(new StringContent(problem), "problem");

                HttpResponseMessage response = await client.PostAsync(Constants.API_SUBMIT_REFERRAL, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("ReferralAccount: createMyReferralAccount() : " + result);
            }
        }

        public static async Task SubmitReferral(String token, int healthCenterId, String remindDate, String problem, FileResult PictureFile)
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
                content.Add(new StringContent(token), "token");
                content.Add(new StringContent(healthCenterId + ""), "healthCenterId");
                content.Add(new StringContent(remindDate), "remindDate");
                content.Add(new StringContent(problem), "problem");

                var PictureStream = await PictureFile.OpenReadAsync();
                var PictureStreamContent = new StreamContent(PictureStream);
                PictureStreamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                content.Add(PictureStreamContent, "image", PictureFile.FileName);

                HttpResponseMessage response = await client.PostAsync(Constants.API_SUBMIT_REFERRAL, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("ReferralAccount: createMyReferralAccount() : " + result);
            }
        }
    }
}
