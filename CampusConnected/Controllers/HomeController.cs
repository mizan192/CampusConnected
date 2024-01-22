using CampusConnected.Models;
using CampusConnected.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CampusConnected.Controllers
{
    public class HomeController : Controller
    {
        // private readonly ILogger<HomeController> _logger;
        //private readonly StudentRepository _studentRepository = null;


        //data base table rec update 
        private readonly StudentDBContext studentDB;
        public HomeController(StudentDBContext studentDB)
        {
            this.studentDB = studentDB;
        }



        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //    _studentRepository=new StudentRepository();
        //}


        //public List<Student> getAllStudents()
        //{
        //    return _studentRepository.getAllStudents();
        //}
        //public Student getById(int id)
        //{
        //    return _studentRepository.getStudentById(id);
        //}


        public async Task<IActionResult> Index()
        {
            var stdData = await studentDB.Students.ToListAsync();

            return View(stdData);
        }

        private Student BindModel()
        {
            //fetch department table date 
            var departments = studentDB.Departments.ToList();

            var student = new Student
            {
                DepartmentList = departments
            };
            return student;
        }


        //action for open create view page
        public IActionResult Create()
        {
            var stdModel = BindModel();
            return View(stdModel);

            
        }


        //action for submit create view page data, httppost = submit data
        //get student model data and add to student database
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Student std)
        {
            //var stdModel = BindModel();
         

            
            //if (ModelState.IsValid)
            //{

           

            if (std.Id != null)
            {

                var dep = studentDB.Departments.FirstOrDefault(d => d.DId == std.DepartmentId);
                std.DepartmentName = dep.Name;
                //std.DepartmentName = dep.Name;
                await studentDB.Students.AddAsync(std);
                await studentDB.SaveChangesAsync();
                TempData["insert_sucess"] = "Record Created Sucessfully!";

                return RedirectToAction("Index", "Home");
            }

            //}

          
            return View(std);


        }

        //end of post 



        //edit method view open
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || studentDB.Students == null)
            {
                return NotFound();
            }
            var editData = await studentDB.Students.FindAsync(id);
            if (editData == null) { return NotFound(); }
            //var data = BindModel();
            editData.DepartmentList = studentDB.Departments.ToList();
            return View(editData);
        }

        //edit method save 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Student std)
        {
            if (id != std.Id) { return NotFound(std); }
            if (std.Id!=null)
            {
                var dep = studentDB.Departments.FirstOrDefault(d => d.DId == std.DepartmentId);
                std.DepartmentName = dep.Name;
                studentDB.Students.Update(std);
                await studentDB.SaveChangesAsync();
                TempData["update_sucess"] = "Record Updated Sucessfully!";
                return RedirectToAction("Index", "Home");
            }
            return View(std);
        }

        //details method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || studentDB.Students == null)
            {
                return NotFound();
            }
            var detailsData = await studentDB.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (detailsData == null)
            {
                return NotFound();
            }
            return View(detailsData);
        }



        //delete method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || studentDB.Students == null)
            {
                return NotFound();
            }
            var deletedData = await studentDB.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (deletedData == null)
            {
                return NotFound();
            }
            return View(deletedData);
        }


        //delete button action

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var deletedData = await studentDB.Students.FindAsync(id);
            if (deletedData != null)
            {
                studentDB.Students.Remove(deletedData);
            }
            await studentDB.SaveChangesAsync();
            TempData["delete_sucess"] = "Record Deleted Sucessfully!";
            return RedirectToAction("Index", "Home");
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}