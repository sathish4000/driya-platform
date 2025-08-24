using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRIYA.Platform.Models;

public class ApiKeyUsage
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    public Guid ApiKeyId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Endpoint { get; set; } = string.Empty;
    
    [StringLength(10)]
    public string Method { get; set; } = "GET";
    
    public int ResponseCode { get; set; }
    public int ResponseTimeMs { get; set; }
    
    [StringLength(50)]
    public string? IpAddress { get; set; }
    
    [StringLength(500)]
    public string? UserAgent { get; set; }
    
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    [ForeignKey(nameof(ApiKeyId))]
    public virtual ApiKey ApiKey { get; set; } = null!;
}
