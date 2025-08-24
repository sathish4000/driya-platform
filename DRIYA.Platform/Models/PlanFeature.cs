using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRIYA.Platform.Models;

public class PlanFeature
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    public Guid PlanId { get; set; }
    
    [Required]
    public Guid FeatureId { get; set; }
    
    [Required]
    [StringLength(500)]
    public string Value { get; set; } = string.Empty;
    
    public bool IsEnabled { get; set; } = true;
    public int? Limit { get; set; } // For features with limits
    
    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    
    // Navigation properties
    [ForeignKey(nameof(PlanId))]
    public virtual LicensePlan Plan { get; set; } = null!;
    
    [ForeignKey(nameof(FeatureId))]
    public virtual Feature Feature { get; set; } = null!;
}
