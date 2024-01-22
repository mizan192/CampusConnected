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

            foreach (var course in courses)
            {
                int courseId = course.CId;
                if (courseIdArray.Contains(courseId))
                    cList.Add(course.CourseName);
            }
            return cList;
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



                if (stdCourseRec != null)
                {

                    var stdCourseList = stdCourseRec.CourseidList;

                    List<string> CourseList = getCourseList(stdCourseList);
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
            // Retrieve other data from ViewData
            var studentId = ViewData["StudentId"];
            var departmentId = ViewData["DepartmentId"];
            var semester = ViewData["Semester"];

            List<int> midResultsList = new List<int>();
            List<int> finalResultsList = new List<int>();
            List<int> classTestResultsList = new List<int>();
            List<int> attendanceResultsList = new List<int>();
            List<int> assignmentResultsList = new List<int>();

            // Iterate through form keys
            //foreach (var key in form.Keys)
            //{
            //    // Check the prefix of the key and extract the index
            //    var index = int.Parse(key.Substring("MidResults".Length));

            //    // Determine the type of result based on the key
            //    if (key.StartsWith("MidResults"))
            //    {
            //        var midResult = int.Parse(form[key]);
            //        midResultsList.Add(midResult);
            //    }
            //    else if (key.StartsWith("FinalResults"))
            //    {
            //        var finalResult = int.Parse(form[key]);
            //        finalResultsList.Add(finalResult);
            //    }
            //    else if (key.StartsWith("ClassTestResults"))
            //    {
            //        var classTestResult = int.Parse(form[key]);
            //        classTestResultsList.Add(classTestResult);
            //    }
            //    else if (key.StartsWith("AttendanceResults"))
            //    {
            //        var attendanceResult = int.Parse(form[key]);
            //        attendanceResultsList.Add(attendanceResult);
            //    }
            //    else if (key.StartsWith("AssignmentResults"))
            //    {
            //        var assignmentResult = int.Parse(form[key]);
            //        assignmentResultsList.Add(assignmentResult);
            //    }
            //}

            //// Convert lists to arrays if needed
            //int[] midResultsArray = midResultsList.ToArray();
            //int[] finalResultsArray = finalResultsList.ToArray();
            //int[] classTestResultsArray = classTestResultsList.ToArray();
            //int[] attendanceResultsArray = attendanceResultsList.ToArray();
            //int[] assignmentResultsArray = assignmentResultsList.ToArray();

            //for (int i = 0; i < midResultsArray.Length; i++)
            //{
            //    Console.WriteLine($"Mid Result {i + 1}: {midResultsArray[i]}");
            //}

            foreach (var key in form.Keys)
            {
                Console.WriteLine($"{key}: {form[key]}");
            }


            return View();
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
