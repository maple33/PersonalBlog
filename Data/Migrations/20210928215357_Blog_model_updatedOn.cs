using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalBlog.Data.Migrations
{
    public partial class Blog_model_updatedOn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Blogs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Blogs");
        }
    }
}
