using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusConnected.Migrations
{
    public partial class marks_fiedls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignmentMarks",
                table: "studentResult",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttendenceMarks",
                table: "studentResult",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClassTestMarks",
                table: "studentResult",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FinalMarks",
                table: "studentResult",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MidMarks",
                table: "studentResult",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignmentMarks",
                table: "studentResult");

            migrationBuilder.DropColumn(
                name: "AttendenceMarks",
                table: "studentResult");

            migrationBuilder.DropColumn(
                name: "ClassTestMarks",
                table: "studentResult");

            migrationBuilder.DropColumn(
                name: "FinalMarks",
                table: "studentResult");

            migrationBuilder.DropColumn(
                name: "MidMarks",
                table: "studentResult");
        }
    }
}
