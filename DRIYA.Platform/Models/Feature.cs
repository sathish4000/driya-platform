using System.ComponentModel.DataAnnotations;

namespace DRIYA.Platform.Models;

public class Feature
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
    public string FeatureKey { get; set; } = string.Empty; // e.g., "advanced_analytics", "custom_branding"
    
    public bool IsSystemFeature { get; set; } = false;
    public bool IsActive { get; set; } = true;
    
    // Feature type
    [StringLength(50)]
    public string FeatureType { get; set; } = "Boolean"; // Boolean, Number, String, JSON
    
    [StringLength(500)]
    public string? DefaultValue { get; set; }
    
    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    
    // Navigation properties
    public virtual ICollection<FeatureFlag> FeatureFlags { get; set; } = new List<FeatureFlag>();
    public virtual ICollection<PlanFeature> PlanFeatures { get; set; } = new List<PlanFeature>();
}
