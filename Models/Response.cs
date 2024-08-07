using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PerformanceSurvey.Models
{
    public class Response
    {


        [Key]
        public int ResponseId { get; set; }

        [Required]
        public int QuestionId { get; set; }

        // Navigation property to Question
        [ForeignKey("QuestionId")]
        public DepartmentQuestion? Question { get; set; }

        [Required]
        public int? UserId { get; set; }

        // Navigation property to User
        [ForeignKey("UserId")]
        public User? User { get; set; }

        public string TextResponse { get; set; }
        public DepartmentQuestionOption DepartmentQuestionOption { get; set; }

        public float ScorePercentage { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

    }
    public enum MultipleChoiceScore
    {
        Very_Poor,
        Poor,
        Good,
        Very_Good,
        Excellent

    }
}
