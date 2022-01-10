using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeePayrollRepository.Migrations
{
    public partial class PayrollService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Emp",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    NAME = table.Column<string>(nullable: true),
                    PROFILE = table.Column<string>(nullable: true),
                    GENDER = table.Column<string>(nullable: true),
                    DEPARTMENT = table.Column<string>(nullable: true),
                    SALARY = table.Column<int>(nullable: false),
                    STARTDATE = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emp", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Emp_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emp_UserId",
                table: "Emp",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emp");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
