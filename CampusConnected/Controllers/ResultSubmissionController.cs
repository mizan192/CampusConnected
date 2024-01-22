using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CampusConnected.Models;
using CampusConnected.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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


        public List<string> getCourseList(String stdCourseList)
        {
            List<string> cList = new List<string>();
            string[] courseIdStrings = stdCourseList.Split(',');
            int[] courseIdArray = courseIdStrings.Select(int.Parse).ToArray();

            var courses = studentDB.Courses.ToList();
            Dictionary<int, string> myDictionary = new Dictionary<int, string>();
            foreach (var course in courses)
            {
                int courseId = course.CId;
                if (courseIdArray.Contains(courseId))
                {
                    string nameofCourse = course.CourseName;
                    myDictionary.Add(courseId, nameofCourse);
                }
            }
            
            foreach (int num in courseIdArray)
            {
                string coursename = myDictionary[num];
                cList.Add(coursename);
            }

            return cList;

            //for showing and storing course with registration serile and for storing marks in this serial
        }


        public ActionResult SubmitResult()
        {

            string stdJson = TempData["ResultSubmissionData"] as string;

            if (stdJson != null)
            {
                // Deserialize the JSON string back to ResultSubmission
                ResultSubmission std = JsonConvert.DeserializeObject<ResultSubmission>(stdJson);

                var s_id = std.StudentId;
                var d_id = std.DepartmentId;
                var semester = std.Semester;
                var stdRec = studentDB.Students.FirstOrDefault(s => s.Id == s_id);
                ViewData["StudentId"] = s_id;
                ViewData["StudentName"] = stdRec.StudentName;
                ViewData["DepartmentId"] = stdRec.DepartmentName;
                ViewData["Semester"] = semester;

                //var stdCourseRec =  studentDB.studentCourses.Where(sc => sc.StudentId == s_id && sc.DepartmentId == d_id).ToList();
                var stdCourseRec = studentDB.studentCourses.FirstOrDefault(sc => sc.StudentId == s_id && sc.DepartmentId == d_id);
                
                ViewData["recData"] = stdCourseRec;

                TempData["s_Id"] = s_id;
                TempData["d_Id"] = d_id;
                


                if (stdCourseRec != null)
                {

                    var stdCourseList = stdCourseRec.CourseidList;

                    List<string> CourseList = getCourseList(stdCourseList);
                    TempData["CourseList"] = CourseList;
                    ViewData["CourseList"] = CourseList;
                    return View();
                }
                else
                {
                    TempData["error"]="Error";
                    return View();
                }


            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitResult(IFormCollection form)
        {
            //var departmentId = ViewData["DepartmentId"] as string;
            //var semester = ViewData["Semester"] as string;
            //var studentId = ViewData["StudentId"] as string;

            var studentId = form["StudentId"];
            var departmentId = form["DepartmentId"];
            var semester = form["Semester"];
            int s_id = Convert.ToInt32(studentId);

           
            var stdCourseRec = studentDB.studentCourses.FirstOrDefault(s=>s.StudentId == s_id);
            var stdCourseList = stdCourseRec.CourseidList;

            List<string> CourseList = getCourseList(stdCourseList);

            foreach (var c in CourseList)
                Console.WriteLine(c);


            int numberOfCourses = CourseList.Count;
            
            // Arrays to store results
            int[] midResultsArray = new int[numberOfCourses];
            int[] finalResultsArray = new int[numberOfCourses];
            int[] classTestResultsArray = new int[numberOfCourses];
            int[] assignmentResultsArray = new int[numberOfCourses];
            int[] attendanceResultsArray = new int[numberOfCourses];

            for (int i = 0; i < numberOfCourses; i++)
            {
                // Parse and store values in arrays
                midResultsArray[i] = Convert.ToInt32(form[$"MidResults[{i}]"]);
                finalResultsArray[i] = Convert.ToInt32(form[$"FinalResults[{i}]"]);
                classTestResultsArray[i] = Convert.ToInt32(form[$"ClassTestResults[{i}]"]);
                assignmentResultsArray[i] = Convert.ToInt32(form[$"AssignmentResults[{i}]"]);
                attendanceResultsArray[i] = Convert.ToInt32(form[$"AttendanceResults[{i}]"]);
            }

            //2351
            

            //foreach (var key in form.Keys)
            //{
            //    Console.WriteLine($"{key}: {form[key]}");
            //}


            //for (int i = 0; i < numberOfCourses; i++)
            //{
            //    Console.WriteLine(midResultsArray[i]);
            //}

            return RedirectToAction("Index", "ResultSubmission");
            //return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResultSubmission std)
        {
           
            if (std.Id != null)
            {
                //TempData["viewModelData"] = std;
                string stdJson = JsonConvert.SerializeObject(std);

                // Store the JSON string in TempData
                TempData["ResultSubmissionData"] = stdJson;
                return RedirectToAction("SubmitResult", "ResultSubmission");
            }

            // Additional logic if needed
            return View(std);
        }


      


    }
}
