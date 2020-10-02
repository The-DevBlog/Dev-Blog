using Microsoft.EntityFrameworkCore.Migrations;

namespace Dev_Blog.Migrations
{
    public partial class newColumnToNewPostTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UpdateNum",
                table: "NewPost",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateNum",
                table: "NewPost");
        }
    }
}
