using Microsoft.EntityFrameworkCore.Migrations;

namespace DeveloperCourse.SecondTask.Identity.DataAccess.Migrations
{
    public partial class addRoleValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "AspNetRoles");
        }
    }
}
