using System.ComponentModel.DataAnnotations;

namespace PerformanceSurvey.Models
{
    public class UserRequest { 

      [Key]
    public int UserId { get; set; }


    [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string UserEmail { get; set; }

        public int? DepartmentId { get; set; }

        public UserType UserType { get; set; }

        public string Password { get; set; }


    }
}
