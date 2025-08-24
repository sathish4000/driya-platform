using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRIYA.Platform.Models;

public class UserPermission
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    public string UserId { get; set; } = string.Empty;
    
    [Required]
    public Guid PermissionId { get; set; }
    
    public Guid? TenantId { get; set; } // For tenant-specific permissions
    
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    public string? AssignedBy { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser User { get; set; } = null!;
    
    [ForeignKey(nameof(PermissionId))]
    public virtual Permission Permission { get; set; } = null!;
    
    [ForeignKey(nameof(TenantId))]
    public virtual Tenant? Tenant { get; set; }
}
