using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusConnected.Models
{
    public class ResultSubmission
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        public int DepartmentId { get; set; }
        public SemesterList Semester { get; set; }

        public int MidMarks { get; set; }   
        public int FinalMarks { get; set; }
        public int ClassTestMarks { get; set; }
        public int AssignmentMarks { get; set; }
        public int AttendenceMarks { get; set; }

        [NotMapped]
        public List<Department> DepartmentList { get; set; }

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



    }
}
