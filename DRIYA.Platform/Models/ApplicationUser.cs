using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DRIYA.Platform.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100)]
        public string? FirstName { get; set; }
        
        [StringLength(100)]
        public string? LastName { get; set; }
        
        public Guid? TenantId { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        [JsonIgnore]
        public virtual Tenant? Tenant { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        [JsonIgnore]
        public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
        [JsonIgnore]
        public virtual ICollection<LoginHistory> LoginHistory { get; set; } = new List<LoginHistory>();
    }
}