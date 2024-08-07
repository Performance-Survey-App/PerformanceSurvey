using System.ComponentModel.DataAnnotations;

namespace PerformanceSurvey.Models
{
    public class DepartmentDto
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [MaxLength(250)]
        public string DepartmentName { get; set; }

    }
}
