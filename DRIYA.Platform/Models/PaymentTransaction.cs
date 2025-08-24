using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRIYA.Platform.Models;

public class PaymentTransaction
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [StringLength(100)]
    public string TransactionId { get; set; } = string.Empty; // External transaction ID
    
    public Guid? InvoiceId { get; set; }
    public Guid? TenantId { get; set; }
    public Guid? GatewayId { get; set; }
    
    [Required]
    public decimal Amount { get; set; }
    
    [StringLength(3)]
    public string Currency { get; set; } = "USD";
    
    [Required]
    [StringLength(50)]
    public string PaymentMethod { get; set; } = string.Empty; // "credit_card", "bank_transfer", "paypal"
    
    [StringLength(20)]
    public string Status { get; set; } = "Pending"; // Pending, Processing, Completed, Failed, Refunded
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    // Gateway-specific data
    [StringLength(200)]
    public string? GatewayTransactionId { get; set; }
    
    [StringLength(200)]
    public string? PaymentIntentId { get; set; }
    
    [StringLength(200)]
    public string? CustomerId { get; set; }
    
    // Fee information
    public decimal GatewayFee { get; set; } = 0;
    public decimal PlatformFee { get; set; } = 0;
    
    // Timestamps
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ProcessedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? FailedAt { get; set; }
    
    // Error information
    [StringLength(500)]
    public string? ErrorMessage { get; set; }
    
    [StringLength(100)]
    public string? ErrorCode { get; set; }
    
    // Metadata
    [StringLength(2000)]
    public string? Metadata { get; set; } // JSON object for additional data
    
    // Navigation properties
    [ForeignKey(nameof(InvoiceId))]
    public virtual Invoice? Invoice { get; set; }
    
    [ForeignKey(nameof(TenantId))]
    public virtual Tenant? Tenant { get; set; }
    
    [ForeignKey(nameof(GatewayId))]
    public virtual PaymentGateway? Gateway { get; set; }
}
