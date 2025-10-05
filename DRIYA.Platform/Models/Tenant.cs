using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DRIYA.Platform.Models;

public class Tenant
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [StringLength(50)]
    public string TenantId { get; set; } = string.Empty; // Unique identifier like "studiodash"
    
    [Required]
    public Guid ApplicationId { get; set; }
    
    [StringLength(200)]
    public string? Description { get; set; }
    
    [Required]
    [StringLength(100)]
    public string ContactEmail { get; set; } = string.Empty;
    
    [StringLength(20)]
    public string? ContactPhone { get; set; }
    
    // White-label branding
    [StringLength(200)]
    public string? LogoUrl { get; set; }
    
    [StringLength(7)]
    public string? PrimaryColor { get; set; } = "#3498db";
    
    [StringLength(7)]
    public string? SecondaryColor { get; set; } = "#2c3e50";
    
    [StringLength(100)]
    public string? CustomDomain { get; set; }
    
    // Subscription and billing
    public Guid? CurrentPlanId { get; set; }
    public DateTime? SubscriptionStartDate { get; set; }
    public DateTime? SubscriptionEndDate { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsTrialActive { get; set; } = false;
    public DateTime? TrialEndDate { get; set; }
    
    // Stripe integration
    [StringLength(100)]
    public string? StripeCustomerId { get; set; }
    
    [StringLength(100)]
    public string? StripeSubscriptionId { get; set; }
    
    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    
    // Navigation properties
    [ForeignKey(nameof(ApplicationId))]
    [JsonIgnore]
    public virtual Application Application { get; set; } = null!;
    
    [JsonIgnore]
    public virtual ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    [JsonIgnore]
    public virtual ICollection<FeatureFlag> FeatureFlags { get; set; } = new List<FeatureFlag>();
    [JsonIgnore]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    [JsonIgnore]
    public virtual ICollection<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();
    [JsonIgnore]
    public virtual ICollection<TenantDatabase> TenantDatabases { get; set; } = new List<TenantDatabase>();
    [JsonIgnore]
    public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();
    
    [ForeignKey(nameof(CurrentPlanId))]
    [JsonIgnore]
    public virtual LicensePlan? CurrentPlan { get; set; }
}
