using CampusConnected.Models;

namespace CampusConnected.Repository
{
    public class StudentRepository : IStudent
    {
        public List<Student> getAllStudents()
        {
            return DataSource();
        }

        public Student getStudentById(int id)
        {
            return DataSource().Where(x=>x.Id ==id ).FirstOrDefault();
        }


        private List<Student> DataSource()
        {
            return new List<Student>
            {
                new Student { Id = 1,StudentId="C1",StudentName="Abul"},
                new Student { Id = 2,StudentId="C2",StudentName="Babul"},
                new Student { Id = 3,StudentId="C3",StudentName="Cabul"}
            };
        }


    }
}
