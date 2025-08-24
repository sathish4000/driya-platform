using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRIYA.Platform.Models;

public class RolePermission
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    public string RoleId { get; set; } = string.Empty;
    
    [Required]
    public Guid PermissionId { get; set; }
    
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    public string? AssignedBy { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    [ForeignKey(nameof(RoleId))]
    public virtual Role Role { get; set; } = null!;
    
    [ForeignKey(nameof(PermissionId))]
    public virtual Permission Permission { get; set; } = null!;
}
