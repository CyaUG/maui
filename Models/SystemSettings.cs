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
    public class SystemSettings : BaseModel
    {
        static readonly string TAG = "X:" + typeof(SystemSettings).Name;

        [JsonProperty("whatsapp_number")]
        public String whatsapp_number { get; set; }

        [JsonProperty("company_phone")]
        public String company_phone { get; set; }

        [JsonProperty("company_email")]
        public String company_email { get; set; }

        [JsonProperty("company_address")]
        public String company_address { get; set; }

        [JsonProperty("currency_symbol")]
        public String currency_symbol { get; set; }

        [JsonProperty("date_format")]
        public String date_format { get; set; }

        [JsonProperty("decimal_separator")]
        public String decimal_separator { get; set; }

        [JsonProperty("currency")]
        public String currency { get; set; }

        [JsonProperty("email_sent_from_address")]
        public String email_sent_from_address { get; set; }

        [JsonProperty("email_sent_from_name")]
        public String email_sent_from_name { get; set; }

        [JsonProperty("paypal_transactional_percentage_fee")]
        public String paypal_transactional_percentage_fee { get; set; }

        [JsonProperty("site_logo")]
        public String site_logo { get; set; }

        [JsonProperty("time_format")]
        public String time_format { get; set; }

        [JsonProperty("timezone")]
        public String timezone { get; set; }

        public static async Task<SystemSettings> fetchSystemSettings()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_SYSTEM_SETTINGS);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                JArray settingsArray = (JArray)obj.GetValue("data");

                SystemSettings systemSettings = new SystemSettings();
                foreach (JToken token in settingsArray)
                {
                    JObject jsonObject = (JObject)token;
                    string setting_name = (string)jsonObject.GetValue("setting_name");
                    string setting_value = (string)jsonObject.GetValue("setting_value");

                    switch (setting_name)
                    {
                        case "whatsapp_number":
                            systemSettings.whatsapp_number = setting_value;
                            break;
                        case "company_phone":
                            systemSettings.company_phone = setting_value;
                            break;
                        case "company_email":
                            systemSettings.company_email = setting_value;
                            break;
                        case "company_address":
                            systemSettings.company_address = setting_value;
                            break;
                        case "currency_symbol":
                            systemSettings.currency_symbol = setting_value;
                            break;
                        case "date_format":
                            systemSettings.date_format = setting_value;
                            break;
                        case "decimal_separator":
                            systemSettings.decimal_separator = setting_value;
                            break;
                        case "currency":
                            systemSettings.currency = setting_value;
                            break;
                        case "email_sent_from_address":
                            systemSettings.email_sent_from_address = setting_value;
                            break;
                        case "email_sent_from_name":
                            systemSettings.email_sent_from_name = setting_value;
                            break;
                        case "paypal_transactional_percentage_fee":
                            systemSettings.paypal_transactional_percentage_fee = setting_value;
                            break;
                        case "site_logo":
                            systemSettings.site_logo = setting_value;
                            break;
                        case "time_format":
                            systemSettings.time_format = setting_value;
                            break;
                        case "timezone":
                            systemSettings.timezone = setting_value;
                            break;
                    }
                }
                return systemSettings;
            }
        }
    }
}
