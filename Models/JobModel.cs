using Youth.Helpers.Converters;
using Youth.Models.Base;
using Youth.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using ZXing;

namespace Youth.Models
{
    public class JobModel
    {
        static readonly string TAG = "X:" + typeof(JobModel).Name;

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("userId")]
        public int userId { get; set; }

        [JsonProperty("jobTitle")]
        public String jobTitle { get; set; }

        [JsonProperty("businessName")]
        public String businessName { get; set; }

        [JsonProperty("jobDescription")]
        public String jobDescription { get; set; }

        [JsonProperty("minSalary")]
        public double minSalary { get; set; }

        [JsonProperty("maxSalary")]
        public double maxSalary { get; set; }

        [JsonProperty("app_deadline")]
        public String app_deadline { get; set; }

        [JsonProperty("ip_address")]
        public String ip_address { get; set; }

        [JsonProperty("address_label")]
        public String address_label { get; set; }

        [JsonProperty("address_country")]
        public String address_country { get; set; }

        [JsonProperty("address_placeId")]
        public String address_placeId { get; set; }

        [JsonProperty("address_latitude")]
        public double address_latitude { get; set; }

        [JsonProperty("address_longitude")]
        public double address_longitude { get; set; }

        [JsonProperty("address_image")]
        public String address_image { get; set; }

        [JsonProperty("likes")]
        public int likes { get; set; }

        [JsonProperty("totalComments")]
        public int totalComments { get; set; }

        [JsonProperty("likedIt")]
        public bool likedIt { get; set; }

        [JsonProperty("isUnLiked")]
        public bool isUnLiked { get; set; }

        [JsonProperty("job_type")]
        public String job_type { get; set; }

        [JsonProperty("resumeRequired")]
        public bool resumeRequired { get; set; }

        [JsonProperty("jobCategoryId")]
        public int jobCategoryId { get; set; }

        [JsonProperty("imageUrl")]
        public String imageUrl { get; set; }

        [JsonProperty("deleted")]
        public String deleted { get; set; }

        [JsonProperty("deleteTime")]
        public String deleteTime { get; set; }

        [JsonProperty("deleterId")]
        public int deleterId { get; set; }

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

        public static async Task<JObject> fetchJobs()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_JOBS);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchMyListedJobs()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_MY_LISTED_JOB);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchMySavedJobs()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_MY_SAVED_JOBS);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchRelatedJobs(int jobCategoryId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_RELATED_JOBS + jobCategoryId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchJobDetails(int jobId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_JOB_DETAILS + jobId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task saveJob(int jobId)
        {
            try
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
                    content.Add(new StringContent(jobId + ""), "jobId");

                    HttpResponseMessage response = await client.PostAsync(Constants.API_SAVE_JOB, content);

                    var result = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("JobModel: submitJob() : " + result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("JobModel: submitJob() : " + ex);
            }
        }

        public static async Task submitJobLike(int jobId)
        {
            try
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
                    content.Add(new StringContent(jobId + ""), "jobId");

                    HttpResponseMessage response = await client.PostAsync(Constants.API_SUBMIT_JOB_LIKE, content);

                    var result = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("JobModel: submitJob() : " + result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("JobModel: submitJob() : " + ex);
            }
        }

        public static async Task submitJob(JobModel jobModel, FileResult CoverPicture, ObservableCollection<String> jobQuestions)
        {
            try
            {
                var valueTask = Constants.GetAuthTocken();
                var access_token = await valueTask;

                using (var client = new HttpClient())
                {
                    // Set the base address for the API
                    client.BaseAddress = new Uri(Constants.apiUrl);

                    // Add the authorization header
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                    string questions = JsonConvert.SerializeObject(jobQuestions);

                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(jobModel.jobTitle), "jobTitle");
                    content.Add(new StringContent(jobModel.businessName), "businessName");
                    content.Add(new StringContent(jobModel.jobDescription), "jobDescription");
                    content.Add(new StringContent(jobModel.minSalary + ""), "minSalary");
                    content.Add(new StringContent(jobModel.maxSalary + ""), "maxSalary");
                    content.Add(new StringContent(jobModel.app_deadline), "app_deadline");
                    content.Add(new StringContent(jobModel.address_label), "address_label");
                    content.Add(new StringContent(jobModel.address_latitude + ""), "address_latitude");
                    content.Add(new StringContent(jobModel.address_longitude + ""), "address_longitude");
                    content.Add(new StringContent(jobModel.job_type + ""), "job_type");
                    content.Add(new StringContent(jobModel.jobCategoryId + ""), "jobCategoryId");
                    content.Add(new StringContent(jobModel.resumeRequired + ""), "resumeRequired");
                    content.Add(new StringContent(questions), "jobQuestions");

                    var CoverPictureStream = await CoverPicture.OpenReadAsync();
                    var CoverPictureStreamContent = new StreamContent(CoverPictureStream);
                    CoverPictureStreamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                    content.Add(CoverPictureStreamContent, "image", CoverPicture.FileName);

                    HttpResponseMessage response = await client.PostAsync(Constants.API_SUBMIT_JOB, content);

                    var result = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("JobModel: submitJob() : " + result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("JobModel: submitJob() : " + ex);
            }
        }
    }
}
