using DRIYA.Platform.Models;

namespace DRIYA.Platform.Services
{
    public interface IApplicationService
    {
        Task<IEnumerable<Application>> GetAllApplicationsAsync();
        Task<Application?> GetApplicationByIdAsync(Guid id);
        Task<Application?> GetApplicationByAppKeyAsync(string appKey);
        Task<Application> CreateApplicationAsync(Application application);
        Task<Application> UpdateApplicationAsync(Application application);
        Task<bool> DeleteApplicationAsync(Guid id);
        Task<bool> IsAppKeyUniqueAsync(string appKey, Guid? excludeId = null);
        Task<bool> IsSubdomainUniqueAsync(string subdomain, Guid? excludeId = null);
    }
}
