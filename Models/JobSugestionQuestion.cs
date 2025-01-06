using Youth.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Youth.Models
{
    public class JobSugestionQuestion
    {
        static readonly string TAG = "X:" + typeof(JobSugestionQuestion).Name;

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("userId")]
        public int userId { get; set; }

        [JsonProperty("question")]
        public String question { get; set; }

        [JsonProperty("deleted")]
        public bool deleted { get; set; }

        [JsonProperty("isDefault")]
        public bool isDefault { get; set; }

        [JsonProperty("deleteTime")]
        public String deleteTime { get; set; }

        [JsonProperty("deleterId")]
        public int deleterId { get; set; }

        [JsonProperty("created_at")]
        public String created_at { get; set; }

        [JsonProperty("updated_at")]
        public String updated_at { get; set; }

        public static async Task<JObject> fetchDefaultSugestionQuestions()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_DEFAULT_SUGESTION_QUESTIONS);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchSugestionQuestions()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_SUGESTION_QUESTIONS);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }
    }
}
