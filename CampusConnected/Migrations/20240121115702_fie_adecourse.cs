using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusConnected.Migrations
{
    public partial class fie_adecourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "studentCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Semester",
                table: "studentCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "studentCourses");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "studentCourses");
        }
    }
}
