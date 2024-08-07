using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PerformanceSurvey.Models
{

    public class DepartmentQuestionOption
    {
        [Key]
        public int OptionId { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [Required]
        public string Text { get; set; }

        public int Score { get; set; }

        // Navigation property
        [ForeignKey("QuestionId")]
        public DepartmentQuestion DepartmentQuestion { get; set; }

    }
}
