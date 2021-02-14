using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorServer.Migrations
{
    public partial class removeVoteModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vote");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    PostModelId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    DownVote = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UpVote = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vote", x => new { x.PostModelId, x.UserName });
                    table.ForeignKey(
                        name: "FK_Vote_Post_PostModelId",
                        column: x => x.PostModelId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
