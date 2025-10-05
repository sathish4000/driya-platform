using DRIYA.Platform.Models;

namespace DRIYA.Platform.Services;

public interface IFeatureService
{
    Task<bool> IsFeatureEnabledAsync(string featureKey, Guid? tenantId = null, string? userId = null);
    Task<FeatureFlag?> GetFeatureFlagAsync(string featureKey, Guid? tenantId = null, string? userId = null);
    Task<FeatureFlag> SetFeatureFlagAsync(string featureKey, string value, Guid? tenantId = null, string? userId = null);
    Task<IEnumerable<Feature>> GetAllFeaturesAsync();
    Task<IEnumerable<Feature>> GetFeaturesByApplicationAsync(Guid applicationId);
    Task<Feature?> GetFeatureByIdAsync(Guid id);
    Task<Feature> CreateFeatureAsync(Feature feature);
    Task<Feature> UpdateFeatureAsync(Feature feature);
    Task<bool> DeleteFeatureAsync(Guid id);
    Task<IEnumerable<FeatureFlag>> GetTenantFeatureFlagsAsync(Guid tenantId);
}
