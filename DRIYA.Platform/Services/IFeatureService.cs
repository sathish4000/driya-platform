using DRIYA.Platform.Models;

namespace DRIYA.Platform.Services;

public interface IFeatureService
{
    Task<bool> IsFeatureEnabledAsync(string featureKey, Guid? tenantId = null, string? userId = null);
    Task<FeatureFlag?> GetFeatureFlagAsync(string featureKey, Guid? tenantId = null, string? userId = null);
    Task<FeatureFlag> SetFeatureFlagAsync(string featureKey, string value, Guid? tenantId = null, string? userId = null);
    Task<IEnumerable<Feature>> GetAllFeaturesAsync();
    Task<IEnumerable<FeatureFlag>> GetTenantFeatureFlagsAsync(Guid tenantId);
}
