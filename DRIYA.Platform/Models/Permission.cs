using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRIYA.Platform.Models;

public class Permission
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Resource { get; set; } = string.Empty; // e.g., "User", "Tenant", "Billing"
    
    [Required]
    [StringLength(50)]
    public string Action { get; set; } = string.Empty; // e.g., "Create", "Read", "Update", "Delete"
    
    public bool IsSystemPermission { get; set; } = false;
    public bool IsActive { get; set; } = true;
    
    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    
    // Navigation properties
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    
    // Computed property
    [NotMapped]
    public string FullPermission => $"{Resource}.{Action}";
}
