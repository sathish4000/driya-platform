using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DRIYA.Platform.Models
{
    public class ApplicationUserAccess
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        public Guid ApplicationId { get; set; }
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Role { get; set; } = "User"; // App-specific role
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        [ForeignKey(nameof(ApplicationId))]
        [JsonIgnore]
        public virtual Application Application { get; set; } = null!;
        
        [ForeignKey(nameof(UserId))]
        [JsonIgnore]
        public virtual ApplicationUser User { get; set; } = null!;
    }
}