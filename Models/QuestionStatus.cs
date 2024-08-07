using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PerformanceSurvey.Models
{
    public class QuestionStatus
    {

        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }

        public int QuestionId { get; set; } 

        public int PendingCount { get; set; }


    }
}
