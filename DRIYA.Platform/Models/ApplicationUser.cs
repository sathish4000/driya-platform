using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRIYA.Platform.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [StringLength(200)]
    public string? ProfilePictureUrl { get; set; }
    
    // Multi-tenant support
    public Guid? TenantId { get; set; }
    
    // MFA and security
    public bool IsMfaEnabled { get; set; } = false;
    public string? MfaSecretKey { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public string? LastLoginIp { get; set; }
    
    // Account status
    public bool IsActive { get; set; } = true;
    public DateTime? EmailConfirmedAt { get; set; }
    public DateTime? PhoneConfirmedAt { get; set; }
    
    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    
    // Navigation properties
    [ForeignKey(nameof(TenantId))]
    public virtual Tenant? Tenant { get; set; }
    
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    public virtual ICollection<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();
    public virtual ICollection<LoginHistory> LoginHistory { get; set; } = new List<LoginHistory>();
    
    // Computed properties
    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
    
    [NotMapped]
    public bool IsGlobalAdmin => UserRoles.Any(ur => ur.Role.Name == "GlobalAdmin");
    
    [NotMapped]
    public bool IsTenantAdmin => UserRoles.Any(ur => ur.Role.Name == "TenantAdmin");
}
