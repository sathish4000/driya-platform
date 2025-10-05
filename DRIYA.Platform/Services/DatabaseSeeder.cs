using Microsoft.AspNetCore.Identity;
using DRIYA.Platform.Models;
using DRIYA.Platform.Data;

namespace DRIYA.Platform.Services;

public class DatabaseSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public DatabaseSeeder(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<Role> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        // Ensure database is created
        await _context.Database.EnsureCreatedAsync();

        // Create default tenant
        var defaultTenant = await CreateDefaultTenantAsync();

        // Create admin user
        await CreateAdminUserAsync(defaultTenant.Id);
    }

    private async Task<Tenant> CreateDefaultTenantAsync()
    {
        var existingTenant = _context.Tenants.FirstOrDefault(t => t.TenantId == "default");
        if (existingTenant != null)
            return existingTenant;

        var tenant = new Tenant
        {
            Id = Guid.NewGuid(),
            TenantId = "default",
            Name = "Default Tenant",
            ContactEmail = "admin@driya.com",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Tenants.Add(tenant);
        await _context.SaveChangesAsync();

        return tenant;
    }

    private async Task CreateAdminUserAsync(Guid? tenantId)
    {
        var adminEmail = "admin@driya.com";
        var existingUser = await _userManager.FindByEmailAsync(adminEmail);
        if (existingUser != null)
            return;

        var adminUser = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = adminEmail,
            Email = adminEmail,
            FirstName = "Admin",
            LastName = "User",
            TenantId = tenantId,
            IsActive = true,
            EmailConfirmed = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(adminUser, "Admin123!");
        if (result.Succeeded)
        {
            // Ensure GlobalAdmin role exists
            if (!await _roleManager.RoleExistsAsync("GlobalAdmin"))
            {
                await _roleManager.CreateAsync(new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "GlobalAdmin",
                    NormalizedName = "GLOBALADMIN",
                    Description = "Global system administrator with full access",
                    IsSystemRole = true,
                    HierarchyLevel = 0,
                    CreatedAt = DateTime.UtcNow
                });
            }

            // Assign GlobalAdmin role to admin user
            await _userManager.AddToRoleAsync(adminUser, "GlobalAdmin");
        }
    }
}
