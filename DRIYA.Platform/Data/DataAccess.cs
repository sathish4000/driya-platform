using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DRIYA.Platform.Models;
using DRIYA.Platform.Services;

namespace DRIYA.Platform.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Multi-tenant models
    public DbSet<Application> Applications { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<ApplicationUserAccess> ApplicationUserAccess { get; set; }
    
    // RBAC models
    public DbSet<Permission> Permissions { get; set; }
    public new DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }
    
    // Licensing and features
    public DbSet<LicensePlan> LicensePlans { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<FeatureFlag> FeatureFlags { get; set; }
    public DbSet<PlanFeature> PlanFeatures { get; set; }
    
    // Billing
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceItem> InvoiceItems { get; set; }
    
    // API and security
    public DbSet<ApiKey> ApiKeys { get; set; }
    public DbSet<ApiKeyUsage> ApiKeyUsage { get; set; }
    public DbSet<LoginHistory> LoginHistory { get; set; }
    
    // Multi-tenant database configuration
    public DbSet<TenantDatabase> TenantDatabases { get; set; }
    
    // Payment processing
    public DbSet<PaymentGateway> PaymentGateways { get; set; }
    public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
    
    // Legacy models - removed to avoid conflicts with Identity

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.ConfigureWarnings(warnings => 
            warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Configure decimal properties
        ConfigureDecimalProperties(builder);
        
        // Configure indexes
        ConfigureIndexes(builder);
        
        // Configure relationships
        ConfigureRelationships(builder);
        
        // Seed data
        SeedData(builder);
    }

    private void ConfigureDecimalProperties(ModelBuilder builder)
    {
        // Configure Invoice decimal properties
        builder.Entity<Invoice>()
            .Property(i => i.DiscountAmount)
            .HasColumnType("decimal(18,2)");

        builder.Entity<Invoice>()
            .Property(i => i.Subtotal)
            .HasColumnType("decimal(18,2)");

        builder.Entity<Invoice>()
            .Property(i => i.TaxAmount)
            .HasColumnType("decimal(18,2)");

        builder.Entity<Invoice>()
            .Property(i => i.TotalAmount)
            .HasColumnType("decimal(18,2)");

        // Configure InvoiceItem decimal properties
        builder.Entity<InvoiceItem>()
            .Property(ii => ii.UnitPrice)
            .HasColumnType("decimal(18,2)");

        builder.Entity<InvoiceItem>()
            .Property(ii => ii.TotalPrice)
            .HasColumnType("decimal(18,2)");

        // Configure LicensePlan decimal properties
        builder.Entity<LicensePlan>()
            .Property(lp => lp.MonthlyPrice)
            .HasColumnType("decimal(18,2)");

        builder.Entity<LicensePlan>()
            .Property(lp => lp.YearlyPrice)
            .HasColumnType("decimal(18,2)");

        // Configure PaymentGateway decimal properties
        builder.Entity<PaymentGateway>()
            .Property(pg => pg.TransactionFeeFixed)
            .HasColumnType("decimal(18,2)");

        builder.Entity<PaymentGateway>()
            .Property(pg => pg.TransactionFeePercentage)
            .HasColumnType("decimal(5,4)"); // For percentages like 2.5%

        // Configure PaymentTransaction decimal properties
        builder.Entity<PaymentTransaction>()
            .Property(pt => pt.Amount)
            .HasColumnType("decimal(18,2)");

        builder.Entity<PaymentTransaction>()
            .Property(pt => pt.GatewayFee)
            .HasColumnType("decimal(18,2)");

        builder.Entity<PaymentTransaction>()
            .Property(pt => pt.PlatformFee)
            .HasColumnType("decimal(18,2)");
    }

    private void ConfigureIndexes(ModelBuilder builder)
    {
        // Application indexes
        builder.Entity<Application>()
            .HasIndex(a => a.AppKey)
            .IsUnique();

        builder.Entity<Application>()
            .HasIndex(a => a.Subdomain)
            .IsUnique();

        // Tenant indexes
        builder.Entity<Tenant>()
            .HasIndex(t => t.TenantId)
            .IsUnique();

        builder.Entity<Tenant>()
            .HasIndex(t => t.ContactEmail);

        // User indexes
        builder.Entity<ApplicationUser>()
            .HasIndex(u => u.TenantId);

        builder.Entity<ApplicationUser>()
            .HasIndex(u => u.Email);

        // Feature indexes
        builder.Entity<Feature>()
            .HasIndex(f => f.FeatureKey)
            .IsUnique();

        builder.Entity<FeatureFlag>()
            .HasIndex(ff => new { ff.FeatureId, ff.TenantId, ff.UserId })
            .IsUnique();

        // Invoice indexes
        builder.Entity<Invoice>()
            .HasIndex(i => i.InvoiceNumber)
            .IsUnique();

        builder.Entity<Invoice>()
            .HasIndex(i => i.TenantId);

        // API Key indexes
        builder.Entity<ApiKey>()
            .HasIndex(ak => ak.KeyHash)
            .IsUnique();

        builder.Entity<ApiKey>()
            .HasIndex(ak => new { ak.TenantId, ak.UserId });

        // Usage tracking indexes
        builder.Entity<ApiKeyUsage>()
            .HasIndex(aku => aku.ApiKeyId);

        builder.Entity<ApiKeyUsage>()
            .HasIndex(aku => aku.Timestamp);

        builder.Entity<LoginHistory>()
            .HasIndex(lh => lh.UserId);

        builder.Entity<LoginHistory>()
            .HasIndex(lh => lh.Timestamp);
    }

    private void ConfigureRelationships(ModelBuilder builder)
    {
        // Application relationships
        builder.Entity<Application>()
            .HasMany(a => a.Tenants)
            .WithOne(t => t.Application)
            .HasForeignKey(t => t.ApplicationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Application>()
            .HasMany(a => a.Features)
            .WithOne(f => f.Application)
            .HasForeignKey(f => f.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Application>()
            .HasMany(a => a.ApplicationUsers)
            .WithOne(au => au.Application)
            .HasForeignKey(au => au.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        // Tenant relationships
        builder.Entity<Tenant>()
            .HasMany(t => t.Users)
            .WithOne(u => u.Tenant)
            .HasForeignKey(u => u.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Tenant>()
            .HasOne(t => t.CurrentPlan)
            .WithMany(p => p.Tenants)
            .HasForeignKey(t => t.CurrentPlanId)
            .OnDelete(DeleteBehavior.Restrict);

        // User relationships
        builder.Entity<ApplicationUser>()
            .HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ApplicationUser>()
            .HasMany(u => u.UserPermissions)
            .WithOne(up => up.User)
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Role relationships
        builder.Entity<Role>()
            .HasMany(r => r.UserRoles)
            .WithOne(ur => ur.Role)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Role>()
            .HasMany(r => r.RolePermissions)
            .WithOne(rp => rp.Role)
            .HasForeignKey(rp => rp.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        // Permission relationships
        builder.Entity<Permission>()
            .HasMany(p => p.RolePermissions)
            .WithOne(rp => rp.Permission)
            .HasForeignKey(rp => rp.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Permission>()
            .HasMany(p => p.UserPermissions)
            .WithOne(up => up.Permission)
            .HasForeignKey(up => up.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);

        // Feature relationships
        builder.Entity<Feature>()
            .HasMany(f => f.FeatureFlags)
            .WithOne(ff => ff.Feature)
            .HasForeignKey(ff => ff.FeatureId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Feature>()
            .HasMany(f => f.PlanFeatures)
            .WithOne(pf => pf.Feature)
            .HasForeignKey(pf => pf.FeatureId)
            .OnDelete(DeleteBehavior.Cascade);

        // Plan relationships
        builder.Entity<LicensePlan>()
            .HasMany(p => p.PlanFeatures)
            .WithOne(pf => pf.Plan)
            .HasForeignKey(pf => pf.PlanId)
            .OnDelete(DeleteBehavior.Cascade);

        // Invoice relationships
        builder.Entity<Invoice>()
            .HasMany(i => i.InvoiceItems)
            .WithOne(ii => ii.Invoice)
            .HasForeignKey(ii => ii.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        // API Key relationships
        builder.Entity<ApiKey>()
            .HasMany(ak => ak.UsageHistory)
            .WithOne(aku => aku.ApiKey)
            .HasForeignKey(aku => aku.ApiKeyId)
            .OnDelete(DeleteBehavior.Cascade);

        // Login History relationships
        builder.Entity<ApplicationUser>()
            .HasMany(u => u.LoginHistory)
            .WithOne(lh => lh.User)
            .HasForeignKey(lh => lh.UserId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // Tenant Database relationships
        builder.Entity<Tenant>()
            .HasMany(t => t.TenantDatabases)
            .WithOne(td => td.Tenant)
            .HasForeignKey(td => td.TenantId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // Payment relationships
        builder.Entity<PaymentGateway>()
            .HasMany(pg => pg.Transactions)
            .WithOne(pt => pt.Gateway)
            .HasForeignKey(pt => pt.GatewayId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.Entity<Invoice>()
            .HasMany(i => i.PaymentTransactions)
            .WithOne(pt => pt.Invoice)
            .HasForeignKey(pt => pt.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.Entity<Tenant>()
            .HasMany(t => t.PaymentTransactions)
            .WithOne(pt => pt.Tenant)
            .HasForeignKey(pt => pt.TenantId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private void SeedData(ModelBuilder builder)
    {
        // Seed default application
        var defaultApp = new Application
        {
            Id = Guid.NewGuid(),
            Name = "DRIYA Platform",
            Description = "Default platform application",
            AppKey = "platform",
            Subdomain = "app",
            IsActive = true,
            Status = "Active",
            Icon = "pi pi-cog",
            PrimaryColor = "#3b82f6",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        builder.Entity<Application>().HasData(defaultApp);

        // Seed system roles
        var globalAdminRole = new Role
        {
            Id = "1",
            Name = "GlobalAdmin",
            NormalizedName = "GLOBALADMIN",
            Description = "Global system administrator with full access",
            IsSystemRole = true,
            HierarchyLevel = 0,
            CreatedAt = DateTime.UtcNow
        };

        var tenantAdminRole = new Role
        {
            Id = "2",
            Name = "TenantAdmin",
            NormalizedName = "TENANTADMIN",
            Description = "Tenant administrator with full tenant access",
            IsSystemRole = true,
            HierarchyLevel = 1,
            CreatedAt = DateTime.UtcNow
        };

        var managerRole = new Role
        {
            Id = "3",
            Name = "Manager",
            NormalizedName = "MANAGER",
            Description = "Manager with limited administrative access",
            IsSystemRole = true,
            HierarchyLevel = 2,
            CreatedAt = DateTime.UtcNow
        };

        var userRole = new Role
        {
            Id = "4",
            Name = "User",
            NormalizedName = "USER",
            Description = "Standard user with basic access",
            IsSystemRole = true,
            HierarchyLevel = 3,
            CreatedAt = DateTime.UtcNow
        };

        builder.Entity<Role>().HasData(globalAdminRole, tenantAdminRole, managerRole, userRole);

        // Seed system permissions
        var permissions = new List<Permission>
        {
            new() { Id = Guid.NewGuid(), Name = "User Management", Resource = "User", Action = "Create", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "User Management", Resource = "User", Action = "Read", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "User Management", Resource = "User", Action = "Update", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "User Management", Resource = "User", Action = "Delete", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "Tenant Management", Resource = "Tenant", Action = "Create", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "Tenant Management", Resource = "Tenant", Action = "Read", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "Tenant Management", Resource = "Tenant", Action = "Update", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "Tenant Management", Resource = "Tenant", Action = "Delete", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "Billing Management", Resource = "Billing", Action = "Create", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "Billing Management", Resource = "Billing", Action = "Read", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "Billing Management", Resource = "Billing", Action = "Update", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "Feature Management", Resource = "Feature", Action = "Create", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "Feature Management", Resource = "Feature", Action = "Read", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "Feature Management", Resource = "Feature", Action = "Update", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "API Key Management", Resource = "ApiKey", Action = "Create", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "API Key Management", Resource = "ApiKey", Action = "Read", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "API Key Management", Resource = "ApiKey", Action = "Update", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "API Key Management", Resource = "ApiKey", Action = "Delete", IsSystemPermission = true }
        };

        builder.Entity<Permission>().HasData(permissions);

        // Seed license plans
        var basicPlan = new LicensePlan
        {
            Id = Guid.NewGuid(),
            Name = "Basic",
            Description = "Basic plan for small teams",
            PlanCode = "basic",
            MonthlyPrice = 29.99m,
            YearlyPrice = 299.99m,
            MaxUsers = 5,
            MaxStorageGB = 10,
            MaxApiCalls = 1000,
            TrialDays = 14,
            AllowTrial = true,
            IsSystemPlan = true,
            CreatedAt = DateTime.UtcNow
        };

        var proPlan = new LicensePlan
        {
            Id = Guid.NewGuid(),
            Name = "Professional",
            Description = "Professional plan for growing businesses",
            PlanCode = "pro",
            MonthlyPrice = 99.99m,
            YearlyPrice = 999.99m,
            MaxUsers = 25,
            MaxStorageGB = 100,
            MaxApiCalls = 10000,
            TrialDays = 14,
            AllowTrial = true,
            IsSystemPlan = true,
            CreatedAt = DateTime.UtcNow
        };

        var premiumPlan = new LicensePlan
        {
            Id = Guid.NewGuid(),
            Name = "Premium",
            Description = "Premium plan for enterprise customers",
            PlanCode = "premium",
            MonthlyPrice = 299.99m,
            YearlyPrice = 2999.99m,
            MaxUsers = 100,
            MaxStorageGB = 1000,
            MaxApiCalls = 100000,
            TrialDays = 14,
            AllowTrial = true,
            IsSystemPlan = true,
            CreatedAt = DateTime.UtcNow
        };

        builder.Entity<LicensePlan>().HasData(basicPlan, proPlan, premiumPlan);

        // Seed features
        var features = new List<Feature>
        {
            new() { Id = Guid.NewGuid(), ApplicationId = defaultApp.Id, Name = "Basic Analytics", FeatureKey = "basic_analytics", FeatureType = "Boolean", DefaultValue = "true", IsSystemFeature = true },
            new() { Id = Guid.NewGuid(), ApplicationId = defaultApp.Id, Name = "Advanced Analytics", FeatureKey = "advanced_analytics", FeatureType = "Boolean", DefaultValue = "false", IsSystemFeature = true },
            new() { Id = Guid.NewGuid(), ApplicationId = defaultApp.Id, Name = "Custom Branding", FeatureKey = "custom_branding", FeatureType = "Boolean", DefaultValue = "false", IsSystemFeature = true },
            new() { Id = Guid.NewGuid(), ApplicationId = defaultApp.Id, Name = "API Access", FeatureKey = "api_access", FeatureType = "Boolean", DefaultValue = "true", IsSystemFeature = true },
            new() { Id = Guid.NewGuid(), ApplicationId = defaultApp.Id, Name = "White Label", FeatureKey = "white_label", FeatureType = "Boolean", DefaultValue = "false", IsSystemFeature = true },
            new() { Id = Guid.NewGuid(), ApplicationId = defaultApp.Id, Name = "Priority Support", FeatureKey = "priority_support", FeatureType = "Boolean", DefaultValue = "false", IsSystemFeature = true },
            new() { Id = Guid.NewGuid(), ApplicationId = defaultApp.Id, Name = "SSO Integration", FeatureKey = "sso_integration", FeatureType = "Boolean", DefaultValue = "false", IsSystemFeature = true },
            new() { Id = Guid.NewGuid(), ApplicationId = defaultApp.Id, Name = "Advanced Security", FeatureKey = "advanced_security", FeatureType = "Boolean", DefaultValue = "false", IsSystemFeature = true }
        };

        builder.Entity<Feature>().HasData(features);
    }
}
