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
    public class ShoppingCategory : BaseModel
    {
        static readonly string TAG = "X:" + typeof(ShoppingCategory).Name;


        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("userId")]
        public String userId { get; set; }

        [JsonProperty("label")]
        public String label { get; set; }

        [JsonProperty("description")]
        public String description { get; set; }

        [JsonProperty("imageUrl")]
        public String imageUrl { get; set; }

        [JsonProperty("adsCount")]
        public int adsCount { get; set; }

        [JsonProperty("views")]
        public int views { get; set; }

        [JsonProperty("status")]
        public String status { get; set; }

        [JsonProperty("suport_name")]
        public String suport_name { get; set; }

        [JsonProperty("suport_phone")]
        public String suport_phone { get; set; }

        [JsonProperty("suport_email")]
        public String suport_email { get; set; }

        [JsonProperty("suport_website")]
        public String suport_website { get; set; }

        [JsonProperty("latitude")]
        public String latitude { get; set; }

        [JsonProperty("longitude")]
        public String longitude { get; set; }

        [JsonProperty("language")]
        public String language { get; set; }

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

        [JsonProperty("level")]
        public int level { get; set; }

        [JsonProperty("name")]
        public String name { get; set; }

        [JsonProperty("user_name")]
        public String user_name { get; set; }

        [JsonProperty("profile_picture")]
        public String profile_picture { get; set; }

        public static async Task<JObject> fetchShoppingCategories()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_SHOPPING_CATEGORIES);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }
    }
}
