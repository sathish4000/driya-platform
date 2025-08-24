using DRIYA.Platform.Models;

namespace DRIYA.Platform.Services;

public interface IApiKeyService
{
    Task<ApiKey> CreateApiKeyAsync(string name, string description, Guid? tenantId = null, string? userId = null);
    Task<ApiKey?> GetApiKeyByHashAsync(string keyHash);
    Task<IEnumerable<ApiKey>> GetTenantApiKeysAsync(Guid tenantId);
    Task<IEnumerable<ApiKey>> GetUserApiKeysAsync(string userId);
    Task<bool> RevokeApiKeyAsync(Guid id);
    Task<bool> ValidateApiKeyAsync(string apiKey);
    Task LogApiUsageAsync(Guid apiKeyId, string endpoint, string method, int responseCode, int responseTimeMs);
}
