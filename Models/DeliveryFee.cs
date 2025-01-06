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
    public class DeliveryFee
    {
        static readonly string TAG = "X:" + typeof(DeliveryFee).Name;

        [JsonProperty("distance")]
        public double distance { get; set; }

        [JsonProperty("servicePointId")]
        public int servicePointId { get; set; }

        [JsonProperty("deliveryFee")]
        public double deliveryFee { get; set; }

        public static async Task<JObject> computeDeliveryFee(DeliveryAddress deliveryAddress)
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
                content.Add(new StringContent(deliveryAddress.fullname + ""), "fullname");
                content.Add(new StringContent(deliveryAddress.phone_number + ""), "phone_number");
                content.Add(new StringContent(deliveryAddress.country + ""), "country");
                content.Add(new StringContent(deliveryAddress.city + ""), "city");
                content.Add(new StringContent(deliveryAddress.user_address + ""), "user_address");
                content.Add(new StringContent(deliveryAddress.unit_apt + ""), "unit_apt");
                content.Add(new StringContent(deliveryAddress.latitude + ""), "latitude");
                content.Add(new StringContent(deliveryAddress.longitude + ""), "longitude");
                content.Add(new StringContent(deliveryAddress.place_id + ""), "place_id");
                content.Add(new StringContent(deliveryAddress.locationAddress + ""), "locationAddress");
                content.Add(new StringContent(deliveryAddress.CountryCode + ""), "CountryCode");
                content.Add(new StringContent(deliveryAddress.AdminArea + ""), "AdminArea");
                content.Add(new StringContent(deliveryAddress.PostalCode + ""), "PostalCode");
                content.Add(new StringContent(deliveryAddress.SubAdminArea + ""), "SubAdminArea");
                content.Add(new StringContent(deliveryAddress.SubThoroughfare + ""), "SubThoroughfare");

                HttpResponseMessage response = await client.PostAsync(Constants.API_COMPUTE_DELIVERY_FEE, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("CheckOutViewModel: computeDeliveryFee()" + result);
                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }
    }
}
