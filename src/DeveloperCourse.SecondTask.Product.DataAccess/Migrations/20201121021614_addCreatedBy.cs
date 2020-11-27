using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeveloperCourse.SecondTask.Product.DataAccess.Migrations
{
    public partial class addCreatedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "created_by",
                table: "product",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_by",
                table: "product");
        }
    }
}
