using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusConnected.Migrations
{
    public partial class db_checkaftooooppppdddk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseRegistration_Students_StudentId",
                table: "CourseRegistration");

            migrationBuilder.DropTable(
                name: "CourseStudentCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseRegistration",
                table: "CourseRegistration");

            migrationBuilder.RenameTable(
                name: "CourseRegistration",
                newName: "studentCourses");

            migrationBuilder.RenameIndex(
                name: "IX_CourseRegistration_StudentId",
                table: "studentCourses",
                newName: "IX_studentCourses_StudentId");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "studentCourses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CourseCId",
                table: "studentCourses",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_studentCourses",
                table: "studentCourses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_studentCourses_CourseCId",
                table: "studentCourses",
                column: "CourseCId");

            migrationBuilder.AddForeignKey(
                name: "FK_studentCourses_Courses_CourseCId",
                table: "studentCourses",
                column: "CourseCId",
                principalTable: "Courses",
                principalColumn: "CId");

            migrationBuilder.AddForeignKey(
                name: "FK_studentCourses_Students_StudentId",
                table: "studentCourses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_studentCourses_Courses_CourseCId",
                table: "studentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_studentCourses_Students_StudentId",
                table: "studentCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_studentCourses",
                table: "studentCourses");

            migrationBuilder.DropIndex(
                name: "IX_studentCourses_CourseCId",
                table: "studentCourses");

            migrationBuilder.DropColumn(
                name: "CourseCId",
                table: "studentCourses");

            migrationBuilder.RenameTable(
                name: "studentCourses",
                newName: "CourseRegistration");

            migrationBuilder.RenameIndex(
                name: "IX_studentCourses_StudentId",
                table: "CourseRegistration",
                newName: "IX_CourseRegistration_StudentId");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "CourseRegistration",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseRegistration",
                table: "CourseRegistration",
                column: "Id");

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
                        name: "FK_CourseStudentCourse_CourseRegistration_StudentCoursesId",
                        column: x => x.StudentCoursesId,
                        principalTable: "CourseRegistration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseStudentCourse_Courses_SelectedCoursesCId",
                        column: x => x.SelectedCoursesCId,
                        principalTable: "Courses",
                        principalColumn: "CId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudentCourse_StudentCoursesId",
                table: "CourseStudentCourse",
                column: "StudentCoursesId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseRegistration_Students_StudentId",
                table: "CourseRegistration",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
