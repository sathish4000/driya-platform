using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRIYA.Platform.Models;

public class Invoice
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [StringLength(50)]
    public string InvoiceNumber { get; set; } = string.Empty;
    
    [Required]
    public Guid TenantId { get; set; }
    
    public Guid? PlanId { get; set; }
    
    [Required]
    public decimal Subtotal { get; set; }
    
    public decimal TaxAmount { get; set; } = 0;
    public decimal DiscountAmount { get; set; } = 0;
    
    [Required]
    public decimal TotalAmount { get; set; }
    
    [StringLength(3)]
    public string Currency { get; set; } = "USD";
    
    // Invoice status
    [StringLength(20)]
    public string Status { get; set; } = "Draft"; // Draft, Sent, Paid, Overdue, Cancelled
    
    public DateTime? DueDate { get; set; }
    public DateTime? PaidDate { get; set; }
    
    // Stripe integration
    [StringLength(100)]
    public string? StripeInvoiceId { get; set; }
    
    [StringLength(100)]
    public string? StripePaymentIntentId { get; set; }
    
    // Billing period
    public DateTime? BillingPeriodStart { get; set; }
    public DateTime? BillingPeriodEnd { get; set; }
    
    // Notes
    [StringLength(1000)]
    public string? Notes { get; set; }
    
    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    
    // Navigation properties
    [ForeignKey(nameof(TenantId))]
    public virtual Tenant Tenant { get; set; } = null!;
    
    [ForeignKey(nameof(PlanId))]
    public virtual LicensePlan? Plan { get; set; }
    
    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
    public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();
}
