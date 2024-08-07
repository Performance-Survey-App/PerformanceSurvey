using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PerformanceSurvey.Models
{
    public class DepartmentQuestion
    {

        [Key]
        public int QuestionId { get; set; }

        [Required]
        [MaxLength(500)] // Adjust as needed
        public string QuestionText { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        // Navigation properties
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        public ICollection<DepartmentQuestionOption> Options { get; set; } = new List<DepartmentQuestionOption>();

        public DateTime? CreatedAt { get; set; } 

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

    }
}
