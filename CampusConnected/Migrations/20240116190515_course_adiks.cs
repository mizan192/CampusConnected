using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusConnected.Migrations
{
    public partial class course_adiks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseStudentCourse");

            migrationBuilder.DropColumn(
                name: "status",
                table: "studentCourses");

            migrationBuilder.CreateIndex(
                name: "IX_studentCourses_CourseId",
                table: "studentCourses",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_studentCourses_Courses_CourseId",
                table: "studentCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_studentCourses_Courses_CourseId",
                table: "studentCourses");

            migrationBuilder.DropIndex(
                name: "IX_studentCourses_CourseId",
                table: "studentCourses");

            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "studentCourses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CourseStudentCourse",
                columns: table => new
                {
                    CourseListCId = table.Column<int>(type: "int", nullable: false),
                    StudentCoursesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudentCourse", x => new { x.CourseListCId, x.StudentCoursesId });
                    table.ForeignKey(
                        name: "FK_CourseStudentCourse_Courses_CourseListCId",
                        column: x => x.CourseListCId,
                        principalTable: "Courses",
                        principalColumn: "CId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseStudentCourse_studentCourses_StudentCoursesId",
                        column: x => x.StudentCoursesId,
                        principalTable: "studentCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudentCourse_StudentCoursesId",
                table: "CourseStudentCourse",
                column: "StudentCoursesId");
        }
    }
}
