using Youth.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;

namespace Youth.Models
{
    public class JobDiscussion
    {
        static readonly string TAG = "X:" + typeof(JobDiscussion).Name;

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("jobId")]
        public int jobId { get; set; }

        [JsonProperty("message")]
        public String message { get; set; }

        [JsonProperty("userId")]
        public int userId { get; set; }

        [JsonProperty("isAComment")]
        public bool isAComment { get; set; }

        [JsonProperty("discussionId")]
        public int discussionId { get; set; }

        [JsonProperty("language")]
        public String language { get; set; }

        [JsonProperty("ip_address")]
        public String ip_address { get; set; }

        [JsonProperty("deleted")]
        public bool deleted { get; set; }

        [JsonProperty("deleteTime")]
        public String deleteTime { get; set; }

        [JsonProperty("deleterId")]
        public int deleterId { get; set; }

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

        [JsonProperty("altComment")]
        public JobDiscussion altComment { get; set; }

        public static async Task<JObject> fetchJobDiscussions(int jobId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_JOB_DISCUSSION + jobId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchJobDiscussionDetails(int discussionId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_JOB_DISCUSSION_DETAILS + discussionId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchJobDiscussionReplies(int discussionId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_JOB_DISCUSSION_REPLIES + discussionId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<HttpStatusCode> submitJobDiscussion(int jobId, String message)
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
                content.Add(new StringContent(message), "message");
                content.Add(new StringContent(jobId + ""), "jobId");

                HttpResponseMessage response = await client.PostAsync(Constants.API_SUBMIT_JOB_DISCUSSION, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("UserAccount: ForgotPassword() : " + result);

                // Return the response content
                return response.StatusCode;
            }
        }

        public static async Task<HttpStatusCode> submitJobDiscussionReply(int discussionId, int jobId, String message)
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
                content.Add(new StringContent(message), "message");
                content.Add(new StringContent(jobId + ""), "jobId");
                content.Add(new StringContent(discussionId + ""), "discussionId");

                HttpResponseMessage response = await client.PostAsync(Constants.API_SUBMIT_JOB_DISCUSSION_REPLIES, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("UserAccount: ForgotPassword() : " + result);

                // Return the response content
                return response.StatusCode;
            }
        }
    }
}
