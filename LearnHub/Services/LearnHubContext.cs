using LearnHub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LearnHub.Services
{
    public class LearnHubContext:IdentityDbContext<ApplicationUser>
    {
        public LearnHubContext(DbContextOptions options) : base(options) { }


        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<CourseResult> CourseResults { get; set; }
    }
}
