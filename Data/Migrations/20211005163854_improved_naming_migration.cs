using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalBlog.Data.Migrations
{
    public partial class improved_naming_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Blogs_BlogId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_BlogId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostId",
                table: "Posts",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Blogs_PostId",
                table: "Posts",
                column: "PostId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Blogs_PostId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PostId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BlogId",
                table: "Posts",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Blogs_BlogId",
                table: "Posts",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
