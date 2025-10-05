using DRIYA.Platform.Data;
using DRIYA.Platform.Models;
using Microsoft.EntityFrameworkCore;

namespace DRIYA.Platform.Services;

public class TenantService : ITenantService
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Tenant?> GetTenantByIdAsync(Guid id)
    {
        return await _context.Tenants.FindAsync(id);
    }

    public async Task<Tenant?> GetTenantByTenantIdAsync(string tenantId)
    {
        return await _context.Tenants.FirstOrDefaultAsync(t => t.TenantId == tenantId);
    }

    public async Task<IEnumerable<Tenant>> GetAllTenantsAsync()
    {
        return await _context.Tenants.ToListAsync();
    }

    public async Task<Tenant> CreateTenantAsync(Tenant tenant)
    {
        tenant.CreatedAt = DateTime.UtcNow;
        _context.Tenants.Add(tenant);
        await _context.SaveChangesAsync();
        return tenant;
    }

    public async Task<Tenant> UpdateTenantAsync(Tenant tenant)
    {
        tenant.UpdatedAt = DateTime.UtcNow;
        _context.Tenants.Update(tenant);
        await _context.SaveChangesAsync();
        return tenant;
    }

    public async Task<bool> DeleteTenantAsync(Guid id)
    {
        var tenant = await _context.Tenants.FindAsync(id);
        if (tenant == null) return false;

        tenant.IsActive = false;
        tenant.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IsTenantActiveAsync(string tenantId)
    {
        var tenant = await GetTenantByTenantIdAsync(tenantId);
        return tenant != null && tenant.IsActive;
    }

    public async Task<Tenant?> GetCurrentTenantAsync()
    {
        var tenantId = _httpContextAccessor.HttpContext?.Request.Headers["X-Tenant-ID"].FirstOrDefault();
        if (string.IsNullOrEmpty(tenantId))
        {
            return null;
        }
        return await GetTenantByTenantIdAsync(tenantId);
    }
}
