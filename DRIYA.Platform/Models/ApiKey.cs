using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRIYA.Platform.Models;

public class ApiKey
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [StringLength(100)]
    public string KeyHash { get; set; } = string.Empty; // Hashed version of the API key
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    public Guid? TenantId { get; set; }
    public string? UserId { get; set; }
    
    // Permissions
    [StringLength(1000)]
    public string? Permissions { get; set; } // JSON array of permissions
    
    public bool IsActive { get; set; } = true;
    public DateTime? ExpiresAt { get; set; }
    public DateTime? LastUsedAt { get; set; }
    
    // Usage tracking
    public int TotalUsage { get; set; } = 0;
    public int MonthlyUsage { get; set; } = 0;
    public DateTime? LastUsageReset { get; set; }
    
    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    
    // Navigation properties
    [ForeignKey(nameof(TenantId))]
    public virtual Tenant? Tenant { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser? User { get; set; }
    
    public virtual ICollection<ApiKeyUsage> UsageHistory { get; set; } = new List<ApiKeyUsage>();
}
