using Youth.Helpers.Converters;
using Youth.Models.Base;
using Youth.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace Youth.Models
{
    public class JobApplication
    {
        static readonly string TAG = "X:" + typeof(JobApplication).Name;

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("userId")]
        public int userId { get; set; }

        [JsonProperty("jobId")]
        public int jobId { get; set; }

        [JsonProperty("applicantName")]
        public String applicantName { get; set; }

        [JsonProperty("applicantCity")]
        public String applicantCity { get; set; }

        [JsonProperty("applicantEmail")]
        public String applicantEmail { get; set; }

        [JsonProperty("applicantPhone")]
        public String applicantPhone { get; set; }

        [JsonProperty("workExperienceFile")]
        public String workExperienceFile { get; set; }

        [JsonProperty("collegeFile")]
        public String collegeFile { get; set; }

        [JsonProperty("highSchoolFile")]
        public String highSchoolFile { get; set; }

        [JsonProperty("ip_address")]
        public String ip_address { get; set; }

        [JsonProperty("deleted")]
        public String deleted { get; set; }

        [JsonProperty("deleteTime")]
        public String deleteTime { get; set; }

        [JsonProperty("deleterId")]
        public String deleterId { get; set; }

        [JsonProperty("level")]
        public String level { get; set; }

        [JsonProperty("name")]
        public String name { get; set; }

        [JsonProperty("user_name")]
        public String user_name { get; set; }

        [JsonProperty("profile_picture")]
        public String profile_picture { get; set; }

        [JsonProperty("authorId")]
        public int authorId { get; set; }

        [JsonProperty("jobTitle")]
        public String jobTitle { get; set; }

        [JsonProperty("businessName")]
        public String businessName { get; set; }

        [JsonProperty("jobCategoryId")]
        public int jobCategoryId { get; set; }

        [JsonProperty("jobDescription")]
        public String jobDescription { get; set; }

        [JsonProperty("minSalary")]
        public double minSalary { get; set; }

        [JsonProperty("maxSalary")]
        public double maxSalary { get; set; }

        [JsonProperty("app_deadline")]
        public String app_deadline { get; set; }

        [JsonProperty("authorIp")]
        public String authorIp { get; set; }

        [JsonProperty("job_address_label")]
        public String job_address_label { get; set; }

        [JsonProperty("job_address_country")]
        public String job_address_country { get; set; }

        [JsonProperty("job_address_placeId")]
        public String job_address_placeId { get; set; }

        [JsonProperty("job_address_latitude")]
        public double job_address_latitude { get; set; }

        [JsonProperty("job_address_longitude")]
        public double job_address_longitude { get; set; }

        [JsonProperty("job_address_image")]
        public String job_address_image { get; set; }

        [JsonProperty("job_type")]
        public String job_type { get; set; }

        [JsonProperty("resumeRequired")]
        public String resumeRequired { get; set; }

        [JsonProperty("imageUrl")]
        public String imageUrl { get; set; }

        [JsonProperty("created_at")]
        public String created_at { get; set; }

        [JsonProperty("updated_at")]
        public String updated_at { get; set; }

        public static async Task<JObject> getMyApplications()
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_MY_JOB_APPLICATIONS);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> getJobApplications(int jobId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_JOB_APPLICATIONS + jobId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task<JObject> fetchJobApplicationDetails(int applicationId)
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
                HttpResponseMessage response = await client.GetAsync(Constants.API_GET_JOB_APPLICATION_DETAILS + applicationId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("JobApplicationDetailsViewModel: " + result);

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }

        public static async Task SubmitJobApplication(String applicantPhone,
                                            String applicantEmail,
                                            String applicantCity,
                                            String applicantName,
                                            int jobId,
                                            FileResult workExperienceFile,
                                            FileResult collegeFile,
                                            FileResult highSchoolFile,
                                            ObservableCollection<JobApplicationQuestion> mJobSugestionQuestions)
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
                content.Add(new StringContent(applicantPhone), "applicantPhone");
                content.Add(new StringContent(applicantEmail), "applicantEmail");
                content.Add(new StringContent(applicantCity), "applicantCity");
                content.Add(new StringContent(applicantName), "applicantName");
                content.Add(new StringContent(jobId + ""), "jobId");

                var json = JsonConvert.SerializeObject(mJobSugestionQuestions);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                content.Add(stringContent, "answers");

                var workExperienceStream = await workExperienceFile.OpenReadAsync();
                var workExperienceStreamContent = new StreamContent(workExperienceStream);
                workExperienceStreamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                content.Add(workExperienceStreamContent, "workExperienceFile", workExperienceFile.FileName);

                var collegeStream = await collegeFile.OpenReadAsync();
                var collegeStreamContent = new StreamContent(collegeStream);
                collegeStreamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                content.Add(collegeStreamContent, "collegeFile", collegeFile.FileName);

                var highSchoolStream = await highSchoolFile.OpenReadAsync();
                var highSchoolStreamContent = new StreamContent(highSchoolStream);
                highSchoolStreamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                content.Add(highSchoolStreamContent, "highSchoolFile", workExperienceFile.FileName);

                HttpResponseMessage response = await client.PostAsync(Constants.API_SUBMIT_JOB_APPLICATION, content);

                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("Quiz: submitQuizResults() : " + result);
            }
        }
    }
}
