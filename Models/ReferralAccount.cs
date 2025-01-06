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
    public class ReferralAccount
    {
        static readonly string TAG = "X:" + typeof(ReferralAccount).Name;

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("userId")]
        public int userId { get; set; }

        [JsonProperty("name")]
        public String name { get; set; }

        [JsonProperty("email")]
        public String email { get; set; }

        [JsonProperty("phone")]
        public String phone { get; set; }

        [JsonProperty("token")]
        public String token { get; set; }

        [JsonProperty("accCategoryId")]
        public int accCategoryId { get; set; }

        [JsonProperty("accountCategoryLabel")]
        public String accountCategoryLabel { get; set; }

        [JsonProperty("accountCategoryDescription")]
        public String accountCategoryDescription { get; set; }

        [JsonProperty("genderId")]
        public int genderId { get; set; }

        [JsonProperty("about")]
        public String about { get; set; }

        [JsonProperty("birthDistrict")]
        public String birthDistrict { get; set; }

        [JsonProperty("birthSubCounty")]
        public String birthSubCounty { get; set; }

        [JsonProperty("address_label")]
        public String address_label { get; set; }

        [JsonProperty("address_latitude")]
        public double address_latitude { get; set; }

        [JsonProperty("address_longitude")]
        public double address_longitude { get; set; }

        [JsonProperty("citizenshipId")]
        public int citizenshipId { get; set; }

        [JsonProperty("dateOfBirth")]
        public String dateOfBirth { get; set; }

        [JsonProperty("citizenshipLabel")]
        public String citizenshipLabel { get; set; }

        [JsonProperty("citizenshipDescription")]
        public String citizenshipDescription { get; set; }

        [JsonProperty("nationalIdUrl")]
        public String nationalIdUrl { get; set; }

        [JsonProperty("profile_picture")]
        public String profile_picture { get; set; }

        [JsonProperty("nin")]
        public String nin { get; set; }

        [JsonProperty("publisherId")]
        public int publisherId { get; set; }

        [JsonProperty("status")]
        public String status { get; set; }

        [JsonProperty("deleted")]
        public String deleted { get; set; }

        [JsonProperty("deleteTime")]
        public String deleteTime { get; set; }

        [JsonProperty("deleterId")]
        public String deleterId { get; set; }

        [JsonProperty("created_at")]
        public String created_at { get; set; }

        [JsonProperty("updated_at")]
        public String updated_at { get; set; }

        public static async Task<JObject> fetchMyReferralAccount()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_MY_REFERRAL_ACCOUNT_DETAILS);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task createMyReferralAccount(ReferralAccount referralAccount,
                                            FileResult NationalIDFile,
                                            FileResult ProfilePictureFile)
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
                content.Add(new StringContent(referralAccount.name), "name");
                content.Add(new StringContent(referralAccount.email), "email");
                content.Add(new StringContent(referralAccount.phone), "phone");
                content.Add(new StringContent(referralAccount.accCategoryId + ""), "accCategoryId");
                content.Add(new StringContent(referralAccount.genderId + ""), "genderId");
                content.Add(new StringContent(referralAccount.about), "about");
                content.Add(new StringContent(referralAccount.birthDistrict), "birthDistrict");
                content.Add(new StringContent(referralAccount.birthSubCounty), "birthSubCounty");
                content.Add(new StringContent(referralAccount.address_label), "address_label");
                content.Add(new StringContent(referralAccount.address_latitude + ""), "address_latitude");
                content.Add(new StringContent(referralAccount.address_longitude + ""), "address_longitude");
                content.Add(new StringContent(referralAccount.citizenshipId + ""), "citizenshipId");
                content.Add(new StringContent(referralAccount.dateOfBirth), "dateOfBirth");
                content.Add(new StringContent(referralAccount.nin), "nin");

                var NationalIDStream = await NationalIDFile.OpenReadAsync();
                var NationalIDStreamContent = new StreamContent(NationalIDStream);
                NationalIDStreamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                content.Add(NationalIDStreamContent, "nationalId", NationalIDFile.FileName);

                var ProfilePictureStream = await ProfilePictureFile.OpenReadAsync();
                var ProfilePictureStreamContent = new StreamContent(ProfilePictureStream);
                ProfilePictureStreamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                content.Add(ProfilePictureStreamContent, "profile_picture", ProfilePictureFile.FileName);

                HttpResponseMessage response = await client.PostAsync(Constants.API_CREATE_MY_REFERRAL_ACCOUNT, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("ReferralAccount: createMyReferralAccount() : " + result);
            }
        }

        public static async Task<JObject> fetchReferralAccountByTocken(String tocken)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_REFERRAL_ACCOUNT_DETAILS_BY_TOCKEN + tocken);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<HttpResponseMessage> submitReferralAccountRequest(String tocken)
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
                content.Add(new StringContent(tocken), "token");

                HttpResponseMessage response = await client.PostAsync(Constants.API_SUBMIT_REFERRAL_ACCOUNT_REQUEST, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("ReferralAccount: submitReferralAccountRequest() : " + result);
                return response;
            }
        }

        public static async Task<JObject> SearchReferralAccounts(String queryText)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_SEARCH_REFERRAL_ACCOUNTS + queryText);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task submitReferralAccount(ReferralAccount referralAccount,
                                            FileResult NationalIDFile,
                                            FileResult ProfilePictureFile)
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
                content.Add(new StringContent(referralAccount.name), "name");
                content.Add(new StringContent(referralAccount.email), "email");
                content.Add(new StringContent(referralAccount.phone), "phone");
                content.Add(new StringContent(referralAccount.accCategoryId + ""), "accCategoryId");
                content.Add(new StringContent(referralAccount.genderId + ""), "genderId");
                content.Add(new StringContent(referralAccount.about), "about");
                content.Add(new StringContent(referralAccount.birthDistrict), "birthDistrict");
                content.Add(new StringContent(referralAccount.birthSubCounty), "birthSubCounty");
                content.Add(new StringContent(referralAccount.address_label), "address_label");
                content.Add(new StringContent(referralAccount.address_latitude + ""), "address_latitude");
                content.Add(new StringContent(referralAccount.address_longitude + ""), "address_longitude");
                content.Add(new StringContent(referralAccount.citizenshipId + ""), "citizenshipId");
                content.Add(new StringContent(referralAccount.dateOfBirth), "dateOfBirth");
                content.Add(new StringContent(referralAccount.nin), "nin");

                var NationalIDStream = await NationalIDFile.OpenReadAsync();
                var NationalIDStreamContent = new StreamContent(NationalIDStream);
                NationalIDStreamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                content.Add(NationalIDStreamContent, "nationalId", NationalIDFile.FileName);

                var ProfilePictureStream = await ProfilePictureFile.OpenReadAsync();
                var ProfilePictureStreamContent = new StreamContent(ProfilePictureStream);
                ProfilePictureStreamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                content.Add(ProfilePictureStreamContent, "profile_picture", ProfilePictureFile.FileName);

                HttpResponseMessage response = await client.PostAsync(Constants.API_CREATE_REFERRAL_ACCOUNT, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("ReferralAccount: createMyReferralAccount() : " + result);
            }
        }

    }
}
