using DRIYA.Platform.Models;

namespace DRIYA.Platform.Services;

public interface ITenantService
{
    Task<Tenant?> GetTenantByIdAsync(Guid id);
    Task<Tenant?> GetTenantByTenantIdAsync(string tenantId);
    Task<IEnumerable<Tenant>> GetAllTenantsAsync();
    Task<Tenant> CreateTenantAsync(Tenant tenant);
    Task<Tenant> UpdateTenantAsync(Tenant tenant);
    Task<bool> DeleteTenantAsync(Guid id);
    Task<bool> IsTenantActiveAsync(string tenantId);
    Task<Tenant?> GetCurrentTenantAsync();
}
