using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerformanceSurvey.Models
{
    public enum UserType
    {
        User,
        AdminUser
    }

    public class User
    {
        [Key]
        public int id { get; set; }

        [MaxLength(250)]
        public string name { get; set; }

        [EmailAddress] // Optionally validate email format
        public string userEmail { get; set; }

        public int? departmentId { get; set; }

        // Navigation properties
        [ForeignKey("departmentId")]
        public Department department { get; set; }

        public UserType userType { get; set; }

        public string password { get; set; }

        public DateTime? createdAt { get; set; } = DateTime.UtcNow;

        public DateTime? updatedAt { get; set; } 

        public DateTime? deletedAt { get; set; } 
    }
}
