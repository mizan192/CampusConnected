using CampusConnected.Models;

namespace CampusConnected.Repository
{
    public interface IStudent
    {
        //get all student 
        List<Student>getAllStudents();
        
        //get student with specefic id 
        Student getStudentById(int id);



    }
}
