using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusConnected.Migrations
{
    public partial class studet_course_tal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudentCourse_StudentCourses_StudentCoursesId",
                table: "CourseStudentCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Students_StudentId",
                table: "StudentCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentCourses",
                table: "StudentCourses");

            migrationBuilder.RenameTable(
                name: "StudentCourses",
                newName: "CourseRegistration");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourses_StudentId",
                table: "CourseRegistration",
                newName: "IX_CourseRegistration_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseRegistration",
                table: "CourseRegistration",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseRegistration_Students_StudentId",
                table: "CourseRegistration",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudentCourse_CourseRegistration_StudentCoursesId",
                table: "CourseStudentCourse",
                column: "StudentCoursesId",
                principalTable: "CourseRegistration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseRegistration_Students_StudentId",
                table: "CourseRegistration");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudentCourse_CourseRegistration_StudentCoursesId",
                table: "CourseStudentCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseRegistration",
                table: "CourseRegistration");

            migrationBuilder.RenameTable(
                name: "CourseRegistration",
                newName: "StudentCourses");

            migrationBuilder.RenameIndex(
                name: "IX_CourseRegistration_StudentId",
                table: "StudentCourses",
                newName: "IX_StudentCourses_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentCourses",
                table: "StudentCourses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudentCourse_StudentCourses_StudentCoursesId",
                table: "CourseStudentCourse",
                column: "StudentCoursesId",
                principalTable: "StudentCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Students_StudentId",
                table: "StudentCourses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
