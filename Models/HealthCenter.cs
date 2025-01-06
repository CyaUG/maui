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
    public class HealthCenter
    {
        static readonly string TAG = "X:" + typeof(HealthCenter).Name;

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("label")]
        public String label { get; set; }

        [JsonProperty("imageUrl")]
        public String imageUrl { get; set; }

        [JsonProperty("email")]
        public String email { get; set; }

        [JsonProperty("phone")]
        public String phone { get; set; }

        [JsonProperty("website")]
        public String website { get; set; }

        [JsonProperty("userId")]
        public int userId { get; set; }

        [JsonProperty("about")]
        public String about { get; set; }

        [JsonProperty("district")]
        public int district { get; set; }

        [JsonProperty("subCounty")]
        public String subCounty { get; set; }

        [JsonProperty("address_label")]
        public String address_label { get; set; }

        [JsonProperty("address_latitude")]
        public double address_latitude { get; set; }

        [JsonProperty("address_longitude")]
        public double address_longitude { get; set; }

        [JsonProperty("licenseUrl")]
        public String licenseUrl { get; set; }

        [JsonProperty("status")]
        public String status { get; set; }

        [JsonProperty("deleted")]
        public bool deleted { get; set; }

        [JsonProperty("deleteTime")]
        public String deleteTime { get; set; }

        [JsonProperty("deleterId")]
        public String deleterId { get; set; }

        [JsonProperty("created_at")]
        public String created_at { get; set; }

        [JsonProperty("updated_at")]
        public String updated_at { get; set; }

        public static async Task<JObject> fetchAllHealthCenters()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_HEALTH_CENTERS);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }
    }
}
