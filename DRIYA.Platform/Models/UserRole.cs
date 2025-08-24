using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRIYA.Platform.Models;

public class UserRole
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    public string UserId { get; set; } = string.Empty;
    
    [Required]
    public string RoleId { get; set; } = string.Empty;
    
    public Guid? TenantId { get; set; } // For tenant-specific roles
    
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    public string? AssignedBy { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser User { get; set; } = null!;
    
    [ForeignKey(nameof(RoleId))]
    public virtual Role Role { get; set; } = null!;
    
    [ForeignKey(nameof(TenantId))]
    public virtual Tenant? Tenant { get; set; }
}
