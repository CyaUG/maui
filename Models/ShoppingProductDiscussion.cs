using Youth.Helpers.Converters;
using Youth.Models.Base;
using Youth.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace Youth.Models
{
    public class ShoppingProductDiscussion
    {
        static readonly string TAG = "X:" + typeof(ShoppingProductDiscussion).Name;

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("productId")]
        public int productId { get; set; }

        [JsonProperty("message")]
        public String message { get; set; }

        [JsonProperty("userId")]
        public int userId { get; set; }

        [JsonProperty("isAComment")]
        public bool isAComment { get; set; }

        [JsonProperty("discussionId")]
        public int discussionId { get; set; }

        [JsonProperty("language")]
        public String language { get; set; }

        [JsonProperty("ip_address")]
        public String ip_address { get; set; }

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

        [JsonProperty("level")]
        public int level { get; set; }

        [JsonProperty("name")]
        public String name { get; set; }

        [JsonProperty("user_name")]
        public String user_name { get; set; }

        [JsonProperty("profile_picture")]
        public String profile_picture { get; set; }

        [JsonProperty("altComment")]
        public ShoppingProductDiscussion altComment { get; set; }

        [JsonProperty("categoryId")]
        public int categoryId { get; set; }

        [JsonProperty("subCategoryId")]
        public int subCategoryId { get; set; }

        [JsonProperty("brandId")]
        public int brandId { get; set; }

        [JsonProperty("brandModel")]
        public int brandModel { get; set; }

        [JsonProperty("label")]
        public String label { get; set; }

        [JsonProperty("description")]
        public String description { get; set; }

        [JsonProperty("imageUrl")]
        public String imageUrl { get; set; }

        [JsonProperty("costPrice")]
        public double costPrice { get; set; }

        [JsonProperty("sellPrice")]
        public double sellPrice { get; set; }

        [JsonProperty("onDiscount")]
        public bool onDiscount { get; set; }

        [JsonProperty("discountPrice")]
        public double discountPrice { get; set; }

        [JsonProperty("hasColorOptions")]
        public bool hasColorOptions { get; set; }

        [JsonProperty("colorOptionsRequired")]
        public bool colorOptionsRequired { get; set; }

        [JsonProperty("hasSizeOptions")]
        public bool hasSizeOptions { get; set; }

        [JsonProperty("sizeOptionsRequired")]
        public bool sizeOptionsRequired { get; set; }

        [JsonProperty("hasFeatureOptions")]
        public bool hasFeatureOptions { get; set; }

        [JsonProperty("featureOptionsRequired")]
        public bool featureOptionsRequired { get; set; }

        [JsonProperty("itemsInStock")]
        public int itemsInStock { get; set; }

        [JsonProperty("maxOrder")]
        public int maxOrder { get; set; }

        [JsonProperty("minOrder")]
        public int minOrder { get; set; }

        [JsonProperty("salesCount")]
        public int salesCount { get; set; }

        [JsonProperty("returnsCount")]
        public int returnsCount { get; set; }

        [JsonProperty("ratting")]
        public String ratting { get; set; }

        [JsonProperty("views")]
        public int views { get; set; }

        [JsonProperty("likes")]
        public int likes { get; set; }

        public static async Task<JObject> fetchShoppingProductDiscussions(int productId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_SHOPPING_PRODUCT_DISCUSSIONS + productId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchShoppingLastProductDiscussion(int productId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_SHOPPING_LAST_PRODUCT_DISCUSSION + productId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchShoppingProductDiscussionDetails(int discussionId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_SHOPPING_PRODUCT_DISCUSSION_DETAILS + discussionId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchShoppingProductDiscussionReplyDetails(String discussionId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_SHOPPING_PRODUCT_DISCUSSION_REPLY_DETAILS + discussionId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchShoppingProductDiscussionReplies(int discussionId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_SHOPPING_PRODUCT_DISCUSSION_REPLIES + discussionId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<HttpStatusCode> submitShoppingProductDiscussion(int productId, String message)
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
                content.Add(new StringContent(message), "message");
                content.Add(new StringContent(productId + ""), "productId");

                HttpResponseMessage response = await client.PostAsync(Constants.API_SUBMIT_SHOPPING_PRODUCT_DISCUSSION, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("UserAccount: ForgotPassword() : " + result);

                // Return the response content
                return response.StatusCode;
            }
        }

        public static async Task<HttpStatusCode> submitShoppingProductDiscussionReply(int discussionId, int productId, String message)
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
                content.Add(new StringContent(message), "message");
                content.Add(new StringContent(productId + ""), "productId");
                content.Add(new StringContent(discussionId + ""), "discussionId");

                HttpResponseMessage response = await client.PostAsync(Constants.API_SUBMIT_SHOPPING_PRODUCT_DISCUSSION_REPLY, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("UserAccount: ForgotPassword() : " + result);

                // Return the response content
                return response.StatusCode;
            }
        }
    }
}
