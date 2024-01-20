using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusConnected.Migrations
{
    public partial class db_check : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
