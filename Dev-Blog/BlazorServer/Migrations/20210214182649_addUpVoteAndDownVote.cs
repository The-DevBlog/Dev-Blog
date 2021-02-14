using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorServer.Migrations
{
    public partial class addUpVoteAndDownVote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownVotes",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "UpVotes",
                table: "Post");

            migrationBuilder.CreateTable(
                name: "DownVote",
                columns: table => new
                {
                    PostModelId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownVote", x => new { x.PostModelId, x.UserName });
                    table.ForeignKey(
                        name: "FK_DownVote_Post_PostModelId",
                        column: x => x.PostModelId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UpVote",
                columns: table => new
                {
                    PostModelId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpVote", x => new { x.PostModelId, x.UserName });
                    table.ForeignKey(
                        name: "FK_UpVote_Post_PostModelId",
                        column: x => x.PostModelId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DownVote");

            migrationBuilder.DropTable(
                name: "UpVote");

            migrationBuilder.AddColumn<int>(
                name: "DownVotes",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpVotes",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
