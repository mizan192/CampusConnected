using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusConnected.Models
{
    public class Student
    {

        [Key]
        public int Id { get; set; }
        
        [Column("StudentId",TypeName ="varchar(100)")]
        [Required] public string StudentId { get; set; } = string.Empty;
       
        [Required] public string StudentName { get; set; } = string.Empty;

        public string Email { get; set; }
        public string Phone { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public GenderList Gender { get; set; }
        [Required] public FacultyList Faculty { get; set; }

        public SemesterList Semester { get; set; }  



        public enum GenderList
        {
            Male,
            Female,
            Other
        }
        public enum FacultyList
        {
            Arts,
            Engineering,
            Business,
            Pharmacy
        }

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


        //relational field with department model 

        public int DepartmentId { get; set; }

        // public string DepartmentName { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; }




        public Student()
        {
            StudentId = string.Empty;
            StudentName = string.Empty;
            BirthDate = DateTime.Today;
        }









    }
}
