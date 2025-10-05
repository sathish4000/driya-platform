using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DRIYA.Platform.Models
{
    public class Application
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        [StringLength(50)]
        public string AppKey { get; set; } = string.Empty; // Unique identifier like "ecommerce", "crm"
        
        [StringLength(200)]
        public string? Domain { get; set; } // Optional custom domain
        
        [StringLength(50)]
        public string? Subdomain { get; set; } // For subdomain routing like "app1.driya.com"
        
        public bool IsActive { get; set; } = true;
        
        [StringLength(20)]
        public string Status { get; set; } = "Active"; // Active, Inactive, Maintenance
        
        [StringLength(100)]
        public string? Icon { get; set; } // Icon class or URL
        
        [StringLength(7)]
        public string? PrimaryColor { get; set; } = "#3b82f6"; // Hex color for theming
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        [JsonIgnore]
        public virtual ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
        [JsonIgnore]
        public virtual ICollection<Feature> Features { get; set; } = new List<Feature>();
        [JsonIgnore]
        public virtual ICollection<ApplicationUserAccess> ApplicationUsers { get; set; } = new List<ApplicationUserAccess>();
    }
}
