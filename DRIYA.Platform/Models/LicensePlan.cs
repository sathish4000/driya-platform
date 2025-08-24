using System.ComponentModel.DataAnnotations;

namespace DRIYA.Platform.Models;

public class LicensePlan
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
    public string PlanCode { get; set; } = string.Empty; // e.g., "basic", "pro", "premium"
    
    public decimal MonthlyPrice { get; set; }
    public decimal YearlyPrice { get; set; }
    
    [StringLength(3)]
    public string Currency { get; set; } = "USD";
    
    public int MaxUsers { get; set; } = 1;
    public int MaxStorageGB { get; set; } = 1;
    public int MaxApiCalls { get; set; } = 1000;
    
    public bool IsActive { get; set; } = true;
    public bool IsSystemPlan { get; set; } = false;
    
    // Trial settings
    public int TrialDays { get; set; } = 0;
    public bool AllowTrial { get; set; } = false;
    
    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    
    // Navigation properties
    public virtual ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
    public virtual ICollection<PlanFeature> PlanFeatures { get; set; } = new List<PlanFeature>();
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
