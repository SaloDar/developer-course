using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeveloperCourse.SecondLesson.Image.Service.DataAccess.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "image",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    last_saved_date = table.Column<DateTime>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    link = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "image");
        }
    }
}
