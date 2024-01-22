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
            //int dep_id = Convert.ToInt32(departmentId);
            
           
            var stdCourseRec = studentDB.studentCourses.FirstOrDefault(s=>s.StudentId == s_id);
            var stdCourseList = stdCourseRec.CourseidList;

            List<string> CourseList = getCourseList(stdCourseList);

            //foreach (var c in CourseList)
            //    Console.WriteLine(c);


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
                if (midResultsArray[i] > 30 || midResultsArray[i] < 0) midResultsArray[i] = 0;
                if (finalResultsArray[i] > 50 || finalResultsArray[i] < 0) finalResultsArray[i] = 0;
                if (classTestResultsArray[i] > 5 || classTestResultsArray[i] < 0) classTestResultsArray[i] = 0;
                if (assignmentResultsArray[i] > 5 || assignmentResultsArray[i] < 0) assignmentResultsArray[i] = 0;
                if (attendanceResultsArray[i] > 10 || attendanceResultsArray[i] < 0) attendanceResultsArray[i] = 0;
            }

            //2351
            //column value is like : 
            //course_id:marks,Course_id:marks

            string midResult = "";
            string finalResult = "";
            string classTestResult = "";
            string assignmentResult = "";
            string attendanceResult = "";
            
            string[] courseIdStrings = stdCourseList.Split(',');
  
            for(int i = 0; i < numberOfCourses; i++)
            {
                midResult += courseIdStrings[i] + ":" + midResultsArray[i].ToString();
                finalResult += courseIdStrings[i] + ":" + finalResultsArray[i].ToString();
                classTestResult += courseIdStrings[i] + ":" + classTestResultsArray[i].ToString();
                assignmentResult += courseIdStrings[i] + ":" + assignmentResultsArray[i].ToString();
                attendanceResult += courseIdStrings[i] + ":" + attendanceResultsArray[i].ToString();
                if (i + 1 < numberOfCourses)
                {
                    midResult += ",";
                    finalResult += ",";
                    classTestResult += ",";
                    assignmentResult += ",";
                    attendanceResult += ",";

                }
            }

            var existingRecord = studentDB.studentResult.FirstOrDefault(sr => sr.StudentId == s_id);

            if (existingRecord != null)
            {
                existingRecord.MidMarks = midResult;
                existingRecord.FinalMarks = finalResult;
                existingRecord.ClassTestMarks =classTestResult ;
                existingRecord.AssignmentMarks = assignmentResult;
                existingRecord.AttendenceMarks = attendanceResult;
            }
            else
            {
                var newRecord = new ResultSubmission
                {
                    StudentId = s_id,
                    //DepartmentId = dep_id,
                    MidMarks = midResult,
                    FinalMarks=finalResult,
                    ClassTestMarks=classTestResult,
                    AssignmentMarks=assignmentResult,
                    AttendenceMarks=attendanceResult,
                };
                if (semester == "First") newRecord.Semester = ResultSubmission.SemesterList.First;
                else if (semester == "Second") newRecord.Semester = ResultSubmission.SemesterList.Second;
                else if (semester == "Third") newRecord.Semester = ResultSubmission.SemesterList.Third;
                else if (semester == "Fourth") newRecord.Semester = ResultSubmission.SemesterList.Fourth;
                else if (semester == "Fiveth") newRecord.Semester = ResultSubmission.SemesterList.Fiveth;
                else if (semester == "Sixth") newRecord.Semester = ResultSubmission.SemesterList.Sixth;
                else if (semester == "Seventh") newRecord.Semester = ResultSubmission.SemesterList.Seventh;
                else if (semester == "Eighth") newRecord.Semester = ResultSubmission.SemesterList.Eighth;
                else newRecord.Semester = ResultSubmission.SemesterList.First;

                studentDB.studentResult.Add(newRecord);
            }

            studentDB.SaveChanges();





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
                var existingRecord = studentDB.studentResult.FirstOrDefault(sr => sr.StudentId == std.StudentId);
                if (existingRecord == null)
                {
                    studentDB.studentResult.Add(std);
                    studentDB.SaveChanges();
                }
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
