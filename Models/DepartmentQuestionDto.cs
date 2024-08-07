using System.ComponentModel.DataAnnotations;

namespace PerformanceSurvey.Models
{
    public class DepartmentQuestionDto
    {
        [Key]
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int DepartmentId { get; set; }
        public List<DepartmentQuestionOptionDto> Options { get; set; } = new List<DepartmentQuestionOptionDto>(); // Default to an empty list

    }
}
