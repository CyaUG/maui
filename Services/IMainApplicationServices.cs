using Youth.Models;
using System.Collections.ObjectModel;

namespace Youth.Services
{
    public interface IMainApplicationServices
    {
        Task<HttpResponseMessage> UpdateUserProfileValue(String editSubject, String editValue);
    }
}
