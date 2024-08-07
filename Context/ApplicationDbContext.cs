using Microsoft.EntityFrameworkCore;
using PerformanceSurvey.Models;

namespace PerformanceSurvey.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                  : base(options) { }

        public DbSet<Department> departments { get; set; }

        public DbSet<User> users { get; set; }

        public DbSet<DepartmentQuestion> department_questions { get; set; }
        public DbSet<DepartmentQuestionOption> DepartmentQuestionOption { get; set; }   
        public DbSet<DepartmentQuestionnaire> DepartmentQuestionnaires { get; set; }
        public DbSet<DepartmentQuestionnaireQuestion> DepartmentQuestionnaireQuestions { get; set; }

        public DbSet<QuestionStatus> question_Statuses { get; set; }

        public DbSet<Response> responses { get; set; }
    }
}
