using System.ComponentModel.DataAnnotations;

namespace PerformanceSurvey.Models
{
    public class DepartmentQuestionOptionDto
    {
        [Key]
        public int OptionId { get; set; }

        public string Text { get; set; }
        public int Score { get; set; }
    }
}
