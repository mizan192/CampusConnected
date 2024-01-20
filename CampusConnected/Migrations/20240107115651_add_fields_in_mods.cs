using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusConnected.Migrations
{
    public partial class add_fields_in_mods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DepartmentId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Students",
                newName: "DId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_DepartmentId",
                table: "Students",
                newName: "IX_Students_DId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DId",
                table: "Students",
                column: "DId",
                principalTable: "Departments",
                principalColumn: "DId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "DId",
                table: "Students",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_DId",
                table: "Students",
                newName: "IX_Students_DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DepartmentId",
                table: "Students",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
