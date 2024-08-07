using System.ComponentModel.DataAnnotations;

namespace PerformanceSurvey.Models
{
    public class DepartmentQuestionnaire
    {
        [Key]
        public int QuestionnaireId { get; set; } // Define the primary key

        // Other properties
        public List<Department> Departments { get; set; }
        public List<DepartmentQuestion> DepartmentQuestion { get; set; }
    }
}
