using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeveloperCourse.SecondTask.Product.DataAccess.Migrations
{
    public partial class addLastSavedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "last_saved_by",
                table: "product",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_saved_by",
                table: "product");
        }
    }
}
