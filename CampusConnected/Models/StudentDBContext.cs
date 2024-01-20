using Microsoft.EntityFrameworkCore;

namespace CampusConnected.Models
{
    public class StudentDBContext : DbContext
    {
        public StudentDBContext(DbContextOptions options) : base(options)
        {
            
        }

        //creating db table name='Students'
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<StudentCourse> studentCourses { get; set; }
        public DbSet<ResultSubmission> studentResult { get; set; }


        
       




    }
}
