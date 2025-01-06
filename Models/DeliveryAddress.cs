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
    public class DeliveryAddress
    {
        static readonly string TAG = "X:" + typeof(DeliveryAddress).Name;

        [JsonProperty("id")]
        public String id { get; set; }

        [JsonProperty("fullname")]
        public String fullname { get; set; }

        [JsonProperty("your_phone_number")]
        public String your_phone_number { get; set; }

        [JsonProperty("phone_number")]
        public String phone_number { get; set; }

        [JsonProperty("country")]
        public String country { get; set; }

        [JsonProperty("city")]
        public String city { get; set; }

        [JsonProperty("user_address")]
        public String user_address { get; set; }

        [JsonProperty("unit_apt")]
        public String unit_apt { get; set; }

        [JsonProperty("userId")]
        public String userId { get; set; }

        [JsonProperty("publishTimeMillis")]
        public long publishTimeMillis { get; set; }

        [JsonProperty("latitude")]
        public double latitude { get; set; }

        [JsonProperty("longitude")]
        public double longitude { get; set; }

        [JsonProperty("place_id")]
        public String place_id { get; set; }

        [JsonProperty("locationAddress")]
        public String locationAddress { get; set; }

        [JsonProperty("CountryCode")]
        public String CountryCode { get; set; }

        [JsonProperty("AdminArea")]
        public String AdminArea { get; set; }

        [JsonProperty("PostalCode")]
        public String PostalCode { get; set; }

        [JsonProperty("SubAdminArea")]
        public String SubAdminArea { get; set; }

        [JsonProperty("SubThoroughfare")]
        public String SubThoroughfare { get; set; }
    }
}
