using Youth.Models;
using Youth.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using ZXing;

namespace Youth.Services
{
    public class MainApplicationServices : IMainApplicationServices
    {
        static readonly string TAG = "X:" + typeof(MainApplicationServices).Name;
        public async Task<HttpResponseMessage> UpdateUserProfileValue(String editSubject, String editValue)
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
                    content.Add(new StringContent(editSubject + ""), "editSubject");
                    content.Add(new StringContent(editValue + ""), "editValue");

                    HttpResponseMessage response = await client.PostAsync(Constants.API_UPDATE_USER_PROFILE_VALUE, content);

                    return response;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(TAG + ex);
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = HttpStatusCode.FailedDependency;
                return httpResponseMessage;
            }
        }

    }
}
