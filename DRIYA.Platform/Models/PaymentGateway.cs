using System.ComponentModel.DataAnnotations;

namespace DRIYA.Platform.Models;

public class PaymentGateway
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty; // "Stripe", "PayPal", "Square"
    
    [Required]
    [StringLength(50)]
    public string GatewayType { get; set; } = string.Empty; // "Stripe", "PayPal", "Square"
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    // Configuration
    [StringLength(200)]
    public string? PublicKey { get; set; }
    
    [StringLength(500)]
    public string? SecretKey { get; set; }
    
    [StringLength(200)]
    public string? WebhookSecret { get; set; }
    
    [StringLength(200)]
    public string? MerchantId { get; set; }
    
    // Environment settings
    public bool IsTestMode { get; set; } = true;
    public bool IsActive { get; set; } = true;
    
    // Supported features
    public bool SupportsCreditCards { get; set; } = true;
    public bool SupportsBankTransfers { get; set; } = false;
    public bool SupportsDigitalWallets { get; set; } = false;
    public bool SupportsRecurringPayments { get; set; } = true;
    
    // Supported currencies
    [StringLength(1000)]
    public string? SupportedCurrencies { get; set; } // JSON array of currency codes
    
    // Fee structure
    public decimal TransactionFeePercentage { get; set; } = 0.029m; // 2.9%
    public decimal TransactionFeeFixed { get; set; } = 0.30m; // $0.30
    
    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    
    // Navigation properties
    public virtual ICollection<PaymentTransaction> Transactions { get; set; } = new List<PaymentTransaction>();
}
