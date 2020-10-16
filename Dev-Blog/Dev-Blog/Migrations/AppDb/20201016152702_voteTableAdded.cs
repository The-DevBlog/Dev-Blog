using Microsoft.EntityFrameworkCore.Migrations;

namespace Dev_Blog.Migrations.AppDb
{
    public partial class voteTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DownVotes",
                table: "Post",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpVotes",
                table: "Post",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    HasVoted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vote", x => new { x.PostId, x.UserId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vote");

            migrationBuilder.DropColumn(
                name: "DownVotes",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "UpVotes",
                table: "Post");
        }
    }
}
