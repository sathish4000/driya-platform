using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRIYA.Platform.Models;

public class Role : IdentityRole
{
    [StringLength(500)]
    public string? Description { get; set; }
    
    public bool IsSystemRole { get; set; } = false;
    public bool IsActive { get; set; } = true;
    
    // Role hierarchy
    public string? ParentRoleId { get; set; }
    public int HierarchyLevel { get; set; } = 0;
    
    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    
    // Navigation properties
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    
    [ForeignKey(nameof(ParentRoleId))]
    public virtual Role? ParentRole { get; set; }
    
    public virtual ICollection<Role> ChildRoles { get; set; } = new List<Role>();
}
