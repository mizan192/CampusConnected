using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusConnected.Migrations
{
    public partial class add_fields_in_models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Students_StudentId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_StudentId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Departments");

            migrationBuilder.CreateIndex(
                name: "IX_Students_DepartmentId",
                table: "Students",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DepartmentId",
                table: "Students",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DepartmentId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_DepartmentId",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_StudentId",
                table: "Departments",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Students_StudentId",
                table: "Departments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
