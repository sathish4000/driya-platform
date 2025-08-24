using DRIYA.Platform.Data;
using DRIYA.Platform.Models;
using Microsoft.EntityFrameworkCore;

namespace DRIYA.Platform.Services;

public class FeatureService : IFeatureService
{
    private readonly ApplicationDbContext _context;

    public FeatureService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsFeatureEnabledAsync(string featureKey, Guid? tenantId = null, string? userId = null)
    {
        // Check user-specific flag first
        if (!string.IsNullOrEmpty(userId))
        {
            var userFlag = await _context.FeatureFlags
                .Include(ff => ff.Feature)
                .FirstOrDefaultAsync(ff => ff.Feature.FeatureKey == featureKey && 
                                         ff.UserId == userId && 
                                         ff.IsEnabled);

            if (userFlag != null)
                return userFlag.Value.ToLower() == "true";
        }

        // Check tenant-specific flag
        if (tenantId.HasValue)
        {
            var tenantFlag = await _context.FeatureFlags
                .Include(ff => ff.Feature)
                .FirstOrDefaultAsync(ff => ff.Feature.FeatureKey == featureKey && 
                                         ff.TenantId == tenantId && 
                                         ff.IsEnabled);

            if (tenantFlag != null)
                return tenantFlag.Value.ToLower() == "true";
        }

        // Check system-wide flag
        var systemFlag = await _context.FeatureFlags
            .Include(ff => ff.Feature)
            .FirstOrDefaultAsync(ff => ff.Feature.FeatureKey == featureKey && 
                                     ff.TenantId == null && 
                                     ff.UserId == null && 
                                     ff.IsEnabled);

        if (systemFlag != null)
            return systemFlag.Value.ToLower() == "true";

        // Return default value from feature definition
        var feature = await _context.Features
            .FirstOrDefaultAsync(f => f.FeatureKey == featureKey);

        return feature?.DefaultValue?.ToLower() == "true";
    }

    public async Task<FeatureFlag?> GetFeatureFlagAsync(string featureKey, Guid? tenantId = null, string? userId = null)
    {
        return await _context.FeatureFlags
            .Include(ff => ff.Feature)
            .FirstOrDefaultAsync(ff => ff.Feature.FeatureKey == featureKey && 
                                     ff.TenantId == tenantId && 
                                     ff.UserId == userId);
    }

    public async Task<FeatureFlag> SetFeatureFlagAsync(string featureKey, string value, Guid? tenantId = null, string? userId = null)
    {
        var feature = await _context.Features
            .FirstOrDefaultAsync(f => f.FeatureKey == featureKey);

        if (feature == null)
            throw new ArgumentException($"Feature '{featureKey}' not found");

        var existingFlag = await GetFeatureFlagAsync(featureKey, tenantId, userId);

        if (existingFlag != null)
        {
            existingFlag.Value = value;
            existingFlag.UpdatedAt = DateTime.UtcNow;
            _context.FeatureFlags.Update(existingFlag);
        }
        else
        {
            existingFlag = new FeatureFlag
            {
                FeatureId = feature.Id,
                TenantId = tenantId,
                UserId = userId,
                Value = value,
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow
            };
            _context.FeatureFlags.Add(existingFlag);
        }

        await _context.SaveChangesAsync();
        return existingFlag;
    }

    public async Task<IEnumerable<Feature>> GetAllFeaturesAsync()
    {
        return await _context.Features.ToListAsync();
    }

    public async Task<IEnumerable<FeatureFlag>> GetTenantFeatureFlagsAsync(Guid tenantId)
    {
        return await _context.FeatureFlags
            .Include(ff => ff.Feature)
            .Where(ff => ff.TenantId == tenantId)
            .ToListAsync();
    }
}
