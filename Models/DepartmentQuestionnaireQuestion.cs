using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PerformanceSurvey.Models
{
    public class DepartmentQuestionnaireQuestion
    {
        [Key]
        public int DepartmentQuestionnaireQuestionID { get; set; }

        [Required]
        public int DepartmentQuestionaireID { get; set; }

        [ForeignKey("QuestionnaireID")]
        public DepartmentQuestionnaire DepartmentQuestionaire { get; set; }

        [Required]
        public int DepartmentQuestionID { get; set; }

        [ForeignKey("QuestionID")]
        public DepartmentQuestion DepartmentQuestion { get; set; }
    }
}
