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
    public class DashboardSummary : BaseModel
    {
        static readonly string TAG = "X:" + typeof(DashboardSummary).Name;

        [JsonProperty("productsCount")]
        public int productsCount { get; set; }

        [JsonProperty("shoppingCartCount")]
        public int shoppingCartCount { get; set; }

        [JsonProperty("eventCartCount")]
        public int eventCartCount { get; set; }

        [JsonProperty("jobsCount")]
        public int jobsCount { get; set; }

        [JsonProperty("service_points")]
        public int service_points { get; set; }

        [JsonProperty("safe_posts")]
        public int safe_posts { get; set; }

        [JsonProperty("plasticBuy")]
        public double plasticBuy { get; set; }

        [JsonProperty("plasticSell")]
        public double plasticSell { get; set; }

        [JsonProperty("postProfilePicture1")]
        public String postProfilePicture1 { get; set; }

        [JsonProperty("postProfilePicture2")]
        public String postProfilePicture2 { get; set; }

        [JsonProperty("jobsProfilePicture1")]
        public String jobsProfilePicture1 { get; set; }

        [JsonProperty("jobsProfilePicture2")]
        public String jobsProfilePicture2 { get; set; }

        [JsonProperty("newPosts")]
        public int newPosts { get; set; }

        [JsonProperty("newProducts")]
        public int newProducts { get; set; }

        [JsonProperty("newJobs")]
        public int newJobs { get; set; }

        [JsonProperty("newEvents")]
        public int newEvents { get; set; }

        [JsonProperty("newQuizzes")]
        public int newQuizzes { get; set; }

        [JsonProperty("eventsCount")]
        public int eventsCount { get; set; }

        [JsonProperty("savedEventsCount")]
        public int savedEventsCount { get; set; }

        public static async Task<JObject> LoadUserDashboardSummary()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_LOAD_DASHBOARD_SUMMARY);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }
    }
}
