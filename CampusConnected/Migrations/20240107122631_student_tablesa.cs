using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusConnected.Migrations
{
    public partial class student_tablesa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DepartmentId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_DepartmentId",
                table: "Students");

            migrationBuilder.CreateTable(
                name: "DepartmentStudent",
                columns: table => new
                {
                    DepartmentsDId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentStudent", x => new { x.DepartmentsDId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_DepartmentStudent_Departments_DepartmentsDId",
                        column: x => x.DepartmentsDId,
                        principalTable: "Departments",
                        principalColumn: "DId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentStudent_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentStudent_StudentsId",
                table: "DepartmentStudent",
                column: "StudentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentStudent");

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
    }
}
