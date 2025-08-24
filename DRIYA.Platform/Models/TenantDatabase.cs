using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRIYA.Platform.Models;

public class TenantDatabase
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    public Guid TenantId { get; set; }
    
    [Required]
    [StringLength(50)]
    public string DatabaseType { get; set; } = string.Empty; // "PostgreSQL", "SQLServer", "MySQL"
    
    [Required]
    [StringLength(200)]
    public string ServerName { get; set; } = string.Empty;
    
    [Required]
    [StringLength(100)]
    public string DatabaseName { get; set; } = string.Empty;
    
    [StringLength(100)]
    public string? Username { get; set; }
    
    [StringLength(500)]
    public string? Password { get; set; }
    
    [StringLength(10)]
    public string? Port { get; set; }
    
    [StringLength(1000)]
    public string? ConnectionString { get; set; }
    
    // Database configuration
    public bool IsShared { get; set; } = true; // true = shared with other tenants, false = dedicated
    public bool IsActive { get; set; } = true;
    
    // Connection settings
    public int MaxConnections { get; set; } = 100;
    public int ConnectionTimeout { get; set; } = 30;
    public bool UseSSL { get; set; } = true;
    
    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    
    // Navigation properties
    [ForeignKey(nameof(TenantId))]
    public virtual Tenant Tenant { get; set; } = null!;
}
