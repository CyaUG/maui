using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Youth.Models
{
    public class AccountRequestResponse
    {
        static readonly string TAG = "X:" + typeof(AccountRequestResponse).Name;

        [JsonProperty("access_token")]
        public String access_token { get; set; }

        [JsonProperty("token_type")]
        public String token_type { get; set; }

        [JsonProperty("status")]
        public int status { get; set; }

        [JsonProperty("message")]
        public String message { get; set; }
    }
}
