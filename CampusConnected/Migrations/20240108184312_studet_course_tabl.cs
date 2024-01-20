using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusConnected.Migrations
{
    public partial class studet_course_tabl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_StudentCourses_StudentCourseId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_StudentCourseId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StudentCourseId",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "CourseStudentCourse",
                columns: table => new
                {
                    SelectedCoursesCId = table.Column<int>(type: "int", nullable: false),
                    StudentCoursesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudentCourse", x => new { x.SelectedCoursesCId, x.StudentCoursesId });
                    table.ForeignKey(
                        name: "FK_CourseStudentCourse_Courses_SelectedCoursesCId",
                        column: x => x.SelectedCoursesCId,
                        principalTable: "Courses",
                        principalColumn: "CId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseStudentCourse_StudentCourses_StudentCoursesId",
                        column: x => x.StudentCoursesId,
                        principalTable: "StudentCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudentCourse_StudentCoursesId",
                table: "CourseStudentCourse",
                column: "StudentCoursesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseStudentCourse");

            migrationBuilder.AddColumn<int>(
                name: "StudentCourseId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_StudentCourseId",
                table: "Courses",
                column: "StudentCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_StudentCourses_StudentCourseId",
                table: "Courses",
                column: "StudentCourseId",
                principalTable: "StudentCourses",
                principalColumn: "Id");
        }
    }
}
