using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerformanceSurvey.Models
{
    public enum UserType
    {
        User,
        AdminUser
    }

    public class User
    {
        [Key]
        public int UserId { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }
        public string UserEmail { get; set; }
        public int? DepartmentId { get; set; }

        // Navigation properties
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        public UserType UserType { get; set; }

        public string Password { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? DeletedAt { get; set; } = DateTime.UtcNow;

    }
}
