using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CampusConnected.Models
{
    public class Course
    {
        [Key]
        public int CId { get; set; }

        [Column("Name", TypeName = "varchar(100)")]
        public string CourseName { get; set; } = string.Empty;
        [Column("Course Code", TypeName = "varchar(100)")]
        public string CourseCode { get; set; } = string.Empty;
        public FacultyList Faculty { get; set; }

       
        public ICollection<StudentCourse> StudentCourses { get; set; }
        public bool IsChecked { get; set; }

        public enum FacultyList
        {
            Arts,
            Engineering,
            Business,
            Pharmacy
        }

        public Course()
        {
            IsChecked = false;
        }

    }
}
