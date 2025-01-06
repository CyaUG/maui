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
    public class SafePost : BaseModel
    {
        static readonly string TAG = "X:" + typeof(SafePost).Name;

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("userId")]
        public int userId { get; set; }

        [JsonProperty("message")]
        public String message { get; set; }

        [JsonProperty("fileUrl")]
        public String fileUrl { get; set; }

        [JsonProperty("targetAudience")]
        public int targetAudience { get; set; }

        [JsonProperty("likes")]
        public int likes { get; set; }

        [JsonProperty("comments")]
        public int comments { get; set; }

        [JsonProperty("views")]
        public int views { get; set; }

        [JsonProperty("postContent")]
        public String postContent { get; set; }

        [JsonProperty("videoId")]
        public String videoId { get; set; }

        [JsonProperty("ghostModeEnabled")]
        public bool ghostModeEnabled { get; set; }

        [JsonProperty("isAComment")]
        public bool isAComment { get; set; }

        [JsonProperty("parentId")]
        public int parentId { get; set; }

        [JsonProperty("language")]
        public String language { get; set; }

        [JsonProperty("likedIt")]
        public bool likedIt { get; set; }

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
        public String level { get; set; }

        [JsonProperty("name")]
        public String name { get; set; }

        [JsonProperty("user_name")]
        public String user_name { get; set; }

        [JsonProperty("profile_picture")]
        public String profile_picture { get; set; }

        [JsonProperty("altComment")]
        public SafePost altComment { get; set; }

        [JsonProperty("isShowingImage")]
        public bool isShowingImage { get; set; }

        [JsonProperty("isShowingVideo")]
        public bool isShowingVideo { get; set; }

        [JsonProperty("isUnLiked")]
        public bool isUnLiked { get; set; }

        public static async Task<JObject> fetchSafePosts()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_SAFE_POSTS);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchSafeImagePosts()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_SAFE_IMAGE_POSTS);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchSafeVideoPosts()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_SAFE_VIDEO_POSTS);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> getSelectedSafePost(int postId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_SELECTED_SAFE_POST + postId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchSafePostComments(int postId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_SAFE_POST_COMMENTS + postId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> getLikedSafePost(int likeId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_SELECTED_SAFE_POST + likeId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task submitSafePostLike(int postId)
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
                content.Add(new StringContent(postId + ""), "postId");

                HttpResponseMessage response = await client.PostAsync(Constants.API_SUBMIT_SAFE_POST_LIKE, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("ReferralAccount: createMyReferralAccount() : " + result);
            }
        }

        public static async Task SendSafePost(string postContent, int targetAudience, bool ghostModeEnabled, string message, string videoId)
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
                content.Add(new StringContent(postContent), "postContent");
                content.Add(new StringContent(ghostModeEnabled + ""), "ghostModeEnabled");
                content.Add(new StringContent(targetAudience + ""), "targetAudience");
                content.Add(new StringContent(message), "message");
                content.Add(new StringContent(videoId + ""), "videoId");

                HttpResponseMessage response = await client.PostAsync(Constants.API_SUBMIT_SAFE_POST, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("ReferralAccount: createMyReferralAccount() : " + result);
            }
        }

        public static async Task SendSafePost(string postContent, int targetAudience, bool ghostModeEnabled, string message, FileResult PostImage)
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
                content.Add(new StringContent(postContent), "postContent");
                content.Add(new StringContent(ghostModeEnabled + ""), "ghostModeEnabled");
                content.Add(new StringContent(targetAudience + ""), "targetAudience");
                content.Add(new StringContent(message + ""), "message");

                var PostImageStream = await PostImage.OpenReadAsync();
                var PostImageStreamContent = new StreamContent(PostImageStream);
                PostImageStreamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                content.Add(PostImageStreamContent, "image", PostImage.FileName);

                HttpResponseMessage response = await client.PostAsync(Constants.API_SUBMIT_SAFE_POST, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("ReferralAccount: createMyReferralAccount() : " + result);
            }
        }

        public static async Task SendSafePostComment(string postContent, int parentId, int targetAudience, bool ghostModeEnabled, string message, string videoId)
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
                content.Add(new StringContent(postContent), "postContent");
                content.Add(new StringContent(ghostModeEnabled + ""), "ghostModeEnabled");
                content.Add(new StringContent(targetAudience + ""), "targetAudience");
                content.Add(new StringContent(message), "message");
                content.Add(new StringContent(videoId + ""), "videoId");
                content.Add(new StringContent(parentId + ""), "parentId");

                HttpResponseMessage response = await client.PostAsync(Constants.API_SUBMIT_SAFE_POST_COMMENT, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("ReferralAccount: createMyReferralAccount() : " + result);
            }
        }

        public static async Task SendSafePostComment(string postContent, int parentId, int targetAudience, bool ghostModeEnabled, string message, FileResult PostImage)
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
                content.Add(new StringContent(postContent), "postContent");
                content.Add(new StringContent(ghostModeEnabled + ""), "ghostModeEnabled");
                content.Add(new StringContent(targetAudience + ""), "targetAudience");
                content.Add(new StringContent(message + ""), "message");
                content.Add(new StringContent(parentId + ""), "parentId");

                var PostImageStream = await PostImage.OpenReadAsync();
                var PostImageStreamContent = new StreamContent(PostImageStream);
                PostImageStreamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                content.Add(PostImageStreamContent, "image", PostImage.FileName);

                HttpResponseMessage response = await client.PostAsync(Constants.API_SUBMIT_SAFE_POST_COMMENT, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("ReferralAccount: createMyReferralAccount() : " + result);
            }
        }
    }
}
