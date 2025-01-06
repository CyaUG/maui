using Youth.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mime;

namespace Youth.Models
{
    public class PhoneContact
    {
        static readonly string TAG = "X:" + typeof(PhoneContact).Name;


        [JsonProperty("id")]
        public String id { get; set; }

        [JsonProperty("firstName")]
        public String firstName { get; set; }

        [JsonProperty("lastName")]
        public String lastName { get; set; }

        [JsonProperty("custom_email")]
        public String custom_email { get; set; }

        [JsonProperty("home_email")]
        public String home_email { get; set; }

        [JsonProperty("work_email")]
        public String work_email { get; set; }

        [JsonProperty("other_email")]
        public String other_email { get; set; }

        [JsonProperty("mobile_email")]
        public String mobile_email { get; set; }

        [JsonProperty("custom_phone")]
        public String custom_phone { get; set; }

        [JsonProperty("home_phone")]
        public String home_phone { get; set; }

        [JsonProperty("mobile_phone")]
        public String mobile_phone { get; set; }

        [JsonProperty("work_phone")]
        public String work_phone { get; set; }

        [JsonProperty("fax_work_phone")]
        public String fax_work_phone { get; set; }

        [JsonProperty("fax_home_phone")]
        public String fax_home_phone { get; set; }

        [JsonProperty("pager_phone")]
        public String pager_phone { get; set; }

        [JsonProperty("other_phone")]
        public String other_phone { get; set; }

        [JsonProperty("callback_phone")]
        public String callback_phone { get; set; }

        [JsonProperty("car_phone")]
        public String car_phone { get; set; }

        [JsonProperty("company_main_phone")]
        public String company_main_phone { get; set; }

        [JsonProperty("isdn_phone")]
        public String isdn_phone { get; set; }

        [JsonProperty("main_phone")]
        public String main_phone { get; set; }

        [JsonProperty("other_fax_phone")]
        public String other_fax_phone { get; set; }

        [JsonProperty("radio_phone")]
        public String radio_phone { get; set; }

        [JsonProperty("telex_phone")]
        public String telex_phone { get; set; }

        [JsonProperty("tty_tdd_phone")]
        public String tty_tdd_phone { get; set; }

        [JsonProperty("work_mobile_phone")]
        public String work_mobile_phone { get; set; }

        [JsonProperty("work_pager_phone")]
        public String work_pager_phone { get; set; }

        [JsonProperty("assistant_phone")]
        public String assistant_phone { get; set; }

        [JsonProperty("mms_phone")]
        public String mms_phone { get; set; }

        [JsonProperty("custom_address")]
        public String custom_address { get; set; }

        [JsonProperty("home_address")]
        public String home_address { get; set; }

        [JsonProperty("work_address")]
        public String work_address { get; set; }

        [JsonProperty("other_address")]
        public String other_address { get; set; }

        [JsonProperty("photo_uri")]
        public String photo_uri { get; set; }

        public static async Task<HttpStatusCode> SendContact(PhoneContact phoneContact, string receiverId, String chatId)
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
                content.Add(new StringContent("contact"), "contentType");
                content.Add(new StringContent(receiverId + ""), "recieverId");
                content.Add(new StringContent(chatId + ""), "chatId");
                content.Add(new StringContent(phoneContact.firstName + ""), "firstName");
                content.Add(new StringContent(phoneContact.lastName + ""), "lastName");
                content.Add(new StringContent(phoneContact.custom_email + ""), "custom_email");
                content.Add(new StringContent(phoneContact.home_email + ""), "home_email");
                content.Add(new StringContent(phoneContact.work_email + ""), "work_email");
                content.Add(new StringContent(phoneContact.other_email + ""), "other_email");
                content.Add(new StringContent(phoneContact.mobile_email + ""), "mobile_email");
                content.Add(new StringContent(phoneContact.custom_phone + ""), "custom_phone");
                content.Add(new StringContent(phoneContact.home_phone + ""), "home_phone");
                content.Add(new StringContent(phoneContact.mobile_phone + ""), "mobile_phone");
                content.Add(new StringContent(phoneContact.work_phone + ""), "work_phone");
                content.Add(new StringContent(phoneContact.fax_work_phone + ""), "fax_work_phone");
                content.Add(new StringContent(phoneContact.fax_home_phone + ""), "fax_home_phone");
                content.Add(new StringContent(phoneContact.pager_phone + ""), "pager_phone");
                content.Add(new StringContent(phoneContact.other_phone + ""), "other_phone");
                content.Add(new StringContent(phoneContact.callback_phone + ""), "callback_phone");
                content.Add(new StringContent(phoneContact.car_phone + ""), "car_phone");
                content.Add(new StringContent(phoneContact.company_main_phone + ""), "company_main_phone");
                content.Add(new StringContent(phoneContact.isdn_phone + ""), "isdn_phone");
                content.Add(new StringContent(phoneContact.main_phone + ""), "main_phone");
                content.Add(new StringContent(phoneContact.other_fax_phone + ""), "other_fax_phone");
                content.Add(new StringContent(phoneContact.radio_phone + ""), "radio_phone");
                content.Add(new StringContent(phoneContact.telex_phone + ""), "telex_phone");
                content.Add(new StringContent(phoneContact.tty_tdd_phone + ""), "tty_tdd_phone");
                content.Add(new StringContent(phoneContact.work_mobile_phone + ""), "work_mobile_phone");
                content.Add(new StringContent(phoneContact.work_pager_phone + ""), "work_pager_phone");
                content.Add(new StringContent(phoneContact.assistant_phone + ""), "assistant_phone");
                content.Add(new StringContent(phoneContact.mms_phone + ""), "mms_phone");
                content.Add(new StringContent(phoneContact.custom_address + ""), "custom_address");
                content.Add(new StringContent(phoneContact.home_address + ""), "home_address");
                content.Add(new StringContent(phoneContact.work_address + ""), "work_address");
                content.Add(new StringContent(phoneContact.other_address + ""), "other_address");
                content.Add(new StringContent(phoneContact.photo_uri + ""), "photo_uri");

                HttpResponseMessage response = await client.PostAsync(Constants.API_SEND_CONTACT, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("Conversation: SendContact() : " + result);

                // Return the response content
                return response.StatusCode;
            }
        }
    }
}
