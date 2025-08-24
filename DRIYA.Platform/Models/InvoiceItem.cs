using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRIYA.Platform.Models;

public class InvoiceItem
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    public Guid InvoiceId { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Description { get; set; } = string.Empty;
    
    public int Quantity { get; set; } = 1;
    
    [Required]
    public decimal UnitPrice { get; set; }
    
    [Required]
    public decimal TotalPrice { get; set; }
    
    [StringLength(50)]
    public string? ItemType { get; set; } // "subscription", "addon", "overage", etc.
    
    [StringLength(100)]
    public string? ItemId { get; set; } // Reference to the actual item
    
    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    [ForeignKey(nameof(InvoiceId))]
    public virtual Invoice Invoice { get; set; } = null!;
}
