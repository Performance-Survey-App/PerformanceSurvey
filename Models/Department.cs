using System.ComponentModel.DataAnnotations;

namespace PerformanceSurvey.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [MaxLength(250)]
        public string DepartmentName { get; set; }

        // Navigation properties
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<DepartmentQuestion> Questions { get; set; } = new List<DepartmentQuestion>();
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
