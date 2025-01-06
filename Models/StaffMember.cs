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
    public class StaffMember
    {
        static readonly string TAG = "X:" + typeof(Symptom).Name;

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

        [JsonProperty("healthCenterId")]
        public int healthCenterId { get; set; }

        [JsonProperty("hc_member_level")]
        public String hc_member_level { get; set; }

        [JsonProperty("publisherId")]
        public int publisherId { get; set; }

        [JsonProperty("isDefault")]
        public bool isDefault { get; set; }

        [JsonProperty("memberId")]
        public int memberId { get; set; }

        [JsonProperty("dateAdded")]
        public String dateAdded { get; set; }

        public static async Task<JObject> fetchAllHealthCenterStaffMembers(int healthCenterId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_HEALTH_CENTER_STAFF_MEMBERS + healthCenterId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }
    }
}
