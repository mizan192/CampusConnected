using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusConnected.Models
{
    public class StudentCourse
    {

        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public int DepartmentId { get; set; }
        public SemesterList Semester { get; set; }
       
        public string CourseidList { get; set; }

        [NotMapped]
        public List<Course> CourseList { get; set; }

        [NotMapped]
        public List<String> CourseIds { get; set; }

        [NotMapped]
        public List<Student> StudentList { get; set; }


       


        public enum SemesterList
        {
            First,
            Second,
            Third,
            Fourth,
            Fiveth,
            Sixth,
            Seventh,
            Eighth
        }





        public StudentCourse()
        {
            
        }

    }
}


