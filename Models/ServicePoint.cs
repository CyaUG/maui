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
    public class ServicePoint
    {
        static readonly string TAG = "X:" + typeof(ServicePoint).Name;

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("userId")]
        public int userId { get; set; }

        [JsonProperty("label")]
        public String label { get; set; }

        [JsonProperty("description")]
        public String description { get; set; }

        [JsonProperty("imageUrl")]
        public String imageUrl { get; set; }

        [JsonProperty("address")]
        public String address { get; set; }

        [JsonProperty("latitude")]
        public double latitude { get; set; }

        [JsonProperty("longitude")]
        public double longitude { get; set; }

        [JsonProperty("email")]
        public String email { get; set; }

        [JsonProperty("website")]
        public String website { get; set; }

        [JsonProperty("phone")]
        public String phone { get; set; }

        [JsonProperty("handlingFee")]
        public double handlingFee { get; set; }

        [JsonProperty("deliveryFeePerKM")]
        public double deliveryFeePerKM { get; set; }

        [JsonProperty("minDeliveryFee")]
        public double minDeliveryFee { get; set; }

        [JsonProperty("maxDeliveryFee")]
        public double maxDeliveryFee { get; set; }

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

        [JsonProperty("distance")]
        public double distance { get; set; }

        [JsonProperty("position")]
        public LocationModule position { get; set; }

        public static async Task<JObject> FetchServicePoints(double Latitude, double Longitude)
        {
            var valueTask = Constants.GetAuthTocken();
            var access_token = await valueTask;

            using (var client = new HttpClient())
            {
                // Set the base address for the API
                client.BaseAddress = new Uri(Constants.apiUrl);

                var myData = new
                {
                    latitude = Latitude + "",
                    longitude = Longitude + ""
                };
                string jsonData = JsonConvert.SerializeObject(myData);

                // Add the authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(Constants.API_GET_NEAREST_SERVICE_POINTS, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("ServicePoint: FetchServicePoints() : " + result);

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> FetchServicePointDetails(int servicePointId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_ALL_SERVICE_POINT_DETAILS + servicePointId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }
    }
}
