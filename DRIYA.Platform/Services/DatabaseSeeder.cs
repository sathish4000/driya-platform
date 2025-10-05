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

        // Create default tenant (for regular users)
        await CreateDefaultTenantAsync();

        // Create admin user (Global Admin doesn't need a tenant)
        await CreateAdminUserAsync();
    }

    private async Task CreateDefaultTenantAsync()
    {
        var existingTenant = _context.Tenants.FirstOrDefault(t => t.TenantId == "default");
        if (existingTenant != null)
            return;

        // Get the default application (should exist from migration seeding)
        var defaultApplication = _context.Applications.FirstOrDefault(a => a.AppKey == "platform");
        if (defaultApplication == null)
        {
            throw new InvalidOperationException("Default application not found. Please ensure database migrations are applied.");
        }

        var tenant = new Tenant
        {
            Id = Guid.NewGuid(),
            TenantId = "default",
            Name = "Default Tenant",
            ContactEmail = "admin@driya.com",
            ApplicationId = defaultApplication.Id, // Set the ApplicationId
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Tenants.Add(tenant);
        await _context.SaveChangesAsync();
    }

    private async Task CreateAdminUserAsync()
    {
        var adminEmail = "admin@driya.com";
        var existingUser = await _userManager.FindByEmailAsync(adminEmail);
        if (existingUser != null)
        {
            // Ensure admin has access to all applications
            await EnsureAdminApplicationAccessAsync(existingUser);
            return;
        }

        var adminUser = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = adminEmail,
            Email = adminEmail,
            FirstName = "Admin",
            LastName = "User",
            TenantId = null, // Global Admin doesn't belong to a specific tenant
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

            // Give admin access to all applications
            await EnsureAdminApplicationAccessAsync(adminUser);
        }
    }

    private async Task EnsureAdminApplicationAccessAsync(ApplicationUser adminUser)
    {
        // Get all applications
        var applications = _context.Applications.ToList();
        
        foreach (var application in applications)
        {
            // Check if admin already has access to this application
            var existingAccess = _context.ApplicationUserAccess
                .FirstOrDefault(aua => aua.UserId == adminUser.Id && aua.ApplicationId == application.Id);
            
            if (existingAccess == null)
            {
                // Create application access for admin
                var appAccess = new ApplicationUserAccess
                {
                    Id = Guid.NewGuid(),
                    ApplicationId = application.Id,
                    UserId = adminUser.Id,
                    Role = "GlobalAdmin", // Admin role for this specific application
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                
                _context.ApplicationUserAccess.Add(appAccess);
            }
        }
        
        await _context.SaveChangesAsync();
    }
}
