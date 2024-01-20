using CampusConnected.Models;
using CampusConnected.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CampusConnected.Controllers
{
    public class StudentCourseController : Controller
    {

        private readonly StudentDBContext studentDB;
        public StudentCourseController(StudentDBContext studentDB)
        {
            this.studentDB = studentDB;
        }



        public async Task<IActionResult> Index()
        {
            var stdData = await studentDB.studentCourses.ToListAsync();

            return View(stdData);
        }


        private StudentCourse BindModel()
        {
            //fetch department table date 
            var students = studentDB.Students.ToList();
            var courses = studentDB.Courses.ToList();
            
            var studentCourses = new StudentCourse
            {
                StudentList = students,
                CourseList = courses,
            };

            return studentCourses;
        }


        public Boolean isCourseExist(string courseList, int new_course)
        {
            string new_id=new_course.ToString();
            string[] courseArray = courseList.Split(',');
            bool isCoursePresent = courseArray.Contains(new_id);
            return isCoursePresent;
        }

        public List<string> BindAllCourseList()
        {
            List<string> cList = new List<string>();
            var courses = studentDB.Courses.ToList();

            foreach (var course in courses)
            {
                cList.Add(course.CourseName);
            }
            return cList;
        }
        public List<string> getAvailableCourse(String registeredCourse)
        {
            List<string> cList = new List<string>();
            
            var courses = studentDB.Courses.ToList();
            string[] values = registeredCourse.Split(',');
            List<int> idList = values.Select(int.Parse).ToList();
            
            foreach (var course in courses)
            {
                int courseId = course.CId;
                if(!idList.Contains(courseId))
                    cList.Add(course.CourseName);
            }
            return cList;
        }
        public List<string> getRegisteredCourse(String registeredCourse)
        {
            List<string> cList = new List<string>();

            var courses = studentDB.Courses.ToList();
            string[] values = registeredCourse.Split(',');
            List<int> idList = values.Select(int.Parse).ToList();

            foreach (var course in courses)
            {
                int courseId = course.CId;
                if (idList.Contains(courseId))
                    cList.Add(course.CourseName);
            }
            return cList;
        }

       

        //action for open create view page
        public IActionResult Create()
        {
            var stdModel = BindModel();
            ViewData["StudentModel"] = stdModel;

            ViewData["AllCourses"] = BindAllCourseList();

            return View();

            //return View(stdModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentCourse std)
        {
            if (std.Id != null)
            {
                var dep = await studentDB.Courses.FirstOrDefaultAsync(d => d.CId == std.CourseId);
                var cur_studentId = std.StudentId;
                var cur_courseId = std.CourseId;
                var isStudentExists = await studentDB.studentCourses.FirstOrDefaultAsync(m => m.StudentId == cur_studentId);

                if (isStudentExists != null)
                {
                    var courseList = isStudentExists.CourseidList;
                    if (!isCourseExist(courseList, cur_courseId))
                    {
                        isStudentExists.CourseidList = courseList + "," + cur_courseId.ToString();
                        TempData["update_sucess"] = "Course Updated Sucessfully!";
                    }
                    else
                    {
                        TempData["already_reg"] = "Course Already Registered!";
                    }
                    await studentDB.SaveChangesAsync(); 
                }
                else
                {
                    std.CourseidList = cur_courseId.ToString();
                    studentDB.studentCourses.Add(std);
                    await studentDB.SaveChangesAsync(); 
                }

                return RedirectToAction("Index", "StudentCourse");
            }

            return View(std);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || studentDB.studentCourses == null)
            {
                return NotFound();
            }
            var editData = await studentDB.studentCourses.FindAsync(id);
            if (editData == null) { return NotFound(); }

            

            editData.StudentList = studentDB.Students.ToList();
            editData.CourseList = studentDB.Courses.ToList();

            ViewData["StudentModel"] = editData;
           
            var registeredCourse = editData.CourseidList;

            ViewData["AllCourses"] = getAvailableCourse(registeredCourse);
            ViewData["regCourse"] = getRegisteredCourse(registeredCourse);

            return View();
            //return View(editData);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, StudentCourse std)
        {
            if (id != std.Id) { return NotFound(std); }
            if (std.Id != null)
            {
                var cur_studentId = std.StudentId;
                var cur_courseId = std.CourseId;
                var isStudentExists = await studentDB.studentCourses.FirstOrDefaultAsync(m => m.StudentId == cur_studentId);

                if (isStudentExists != null)
                {
                    var courseList = isStudentExists.CourseidList;
                    if (!isCourseExist(courseList, cur_courseId))
                    {
                        isStudentExists.CourseidList = courseList + "," + cur_courseId.ToString();
                        TempData["update_sucess"] = "Course Updated Sucessfully!";
                    }
                    else
                    {
                        TempData["already_reg"] = "Course Already Registered!";
                    }
                    await studentDB.SaveChangesAsync();
                }
                else
                {
                    studentDB.studentCourses.Update(std);
                    await studentDB.SaveChangesAsync();
                }


                return RedirectToAction("Index", "Home");
            }
            return View(std);
        }


        //details method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || studentDB.studentCourses == null)
            {
                return NotFound();
            }
            var detailsData = await studentDB.studentCourses.FirstOrDefaultAsync(x => x.Id == id);
            if (detailsData == null)
            {
                return NotFound();
            }
           

            ViewData["StudentModel"] = detailsData;

            var registeredCourse = detailsData.CourseidList;
            
            ViewData["AllCourses"] = getAvailableCourse(registeredCourse);
            ViewData["regCourse"] = getRegisteredCourse(registeredCourse);

            return View();

            //return View(detailsData);

        }




        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || studentDB.studentCourses == null)
            {
                return NotFound();
            }
            var deletedData = await studentDB.studentCourses.FirstOrDefaultAsync(x => x.Id == id);
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
            var deletedData = await studentDB.studentCourses.FindAsync(id);
            if (deletedData != null)
            {
                studentDB.studentCourses.Remove(deletedData);
            }
            await studentDB.SaveChangesAsync();
            TempData["delete_sucess"] = "Record Deleted Sucessfully!";
            return RedirectToAction("Index");
        }


    }
}
