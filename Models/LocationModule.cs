using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Youth.Models
{
    public class LocationModule
    {
        static readonly string TAG = "X:" + typeof(LocationModule).Name;

        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }

        [JsonProperty("Address")]
        public String Address { get; set; }
    }
}
