using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusConnected.Migrations
{
    public partial class db_checkaftooooppppppppppppk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_studentCourses_Courses_CourseCId",
                table: "studentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_studentCourses_Students_StudentId",
                table: "studentCourses");

            migrationBuilder.DropIndex(
                name: "IX_studentCourses_CourseCId",
                table: "studentCourses");

            migrationBuilder.DropColumn(
                name: "CourseCId",
                table: "studentCourses");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "studentCourses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "studentCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_studentCourses_Students_StudentId",
                table: "studentCourses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_studentCourses_Courses_CourseId",
                table: "studentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_studentCourses_Students_StudentId",
                table: "studentCourses");

            migrationBuilder.DropIndex(
                name: "IX_studentCourses_CourseId",
                table: "studentCourses");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "studentCourses");

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
    }
}
