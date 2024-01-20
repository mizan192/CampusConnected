using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CampusConnected.Models;
using CampusConnected.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CampusConnected.Controllers
{
    public class ResultSubmissionController : Controller
    {

        private readonly StudentDBContext studentDB;
        public ResultSubmissionController(StudentDBContext studentDB)
        {
            this.studentDB = studentDB;
        }




        public ActionResult Index()
        {
            var departments = studentDB.Departments.ToList();
            var students = studentDB.Students.ToList();
            var obj = new ResultSubmission
            {
                StudentList=students,
                DepartmentList = departments,   
            };

            ViewData["ViewModelData"] = obj;

            return View();
        }




        
    }
}
