using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DRIYA.Platform.Models;

public class FeatureFlag
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    public Guid FeatureId { get; set; }
    
    public Guid? TenantId { get; set; } // null for system-wide
    public string? UserId { get; set; } // null for tenant-wide
    
    [Required]
    [StringLength(500)]
    public string Value { get; set; } = string.Empty;
    
    public bool IsEnabled { get; set; } = true;
    public DateTime? ExpiresAt { get; set; }
    
    // Override settings
    public bool IsOverride { get; set; } = false;
    public string? OverrideReason { get; set; }
    public string? OverrideBy { get; set; }
    
    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    
    // Navigation properties
    [ForeignKey(nameof(FeatureId))]
    [JsonIgnore]
    public virtual Feature Feature { get; set; } = null!;
    
    [ForeignKey(nameof(TenantId))]
    [JsonIgnore]
    public virtual Tenant? Tenant { get; set; }
    
    [ForeignKey(nameof(UserId))]
    [JsonIgnore]
    public virtual ApplicationUser? User { get; set; }
}
